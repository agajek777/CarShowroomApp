using CarShowroom.Application.Interfaces;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Parameters;
using CarShowroom.UI.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarShowroom.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ExceptionHandlingFilter]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IClientService _clientService;
        private readonly ILogger<CarController> _logger;

        public CarController(ICarService carService, IClientService clientService, ILogger<CarController> logger)
        {
            _carService = carService;
            _clientService = clientService;
            _logger = logger;
        }
        [HttpGet]
        [AllowAnonymous]
        [Cached(60)]
        public async Task<IActionResult> GetAllAsync([FromQuery] QueryParameters queryParameters)
        {
            var outcome = await _carService.GetAllCarsAsync(queryParameters);

            _logger.LogInformation("User {User} obtained {Num} Car Models from db", HttpContext.User.Identity.Name, outcome.Count);

            var metadata = new
            {
                outcome.TotalCount,
                outcome.PageSize,
                outcome.CurrentPage,
                outcome.TotalPages,
                outcome.HasNext,
                outcome.HasPrevious,
            };

            Response.Headers.Add("Access-Control-Expose-Headers", "*, Authorization, X-Pagination");
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(outcome);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Cached(60)]
        public async Task<IActionResult> Get(int id)
        {
            if (!await _carService.CarExistsAsync(id))
                return BadRequest(new { Message = $"No car with ID { id } has been found." });

            var carInDb = await _carService.GetCarAsync(id);

            if (carInDb == null)
                return NotFound(new { Error = "Request unsuccessfull." });


            _logger.LogInformation("User {User} obtained Car Model from db", HttpContext.User.Identity.Name);

            return Ok(carInDb);
        }
        [HttpPost]
        [ModelValidationFilter]
        public async Task<IActionResult> Post([FromBody] CarDto carDto)
        {
            if (!await CheckIfClientAsync())
                return StatusCode(StatusCodes.Status403Forbidden, "No client account has been found.");

            var id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = await _carService.AddCarAsync(id, carDto);

            if (model == null)
                return Conflict(new { Error = "Request unsuccessfull." });

            _logger.LogInformation("User {User} added Car Model to db", HttpContext.User.Identity.Name);

            return Ok(model);
        }
        [HttpPut("{id}")]
        [ModelValidationFilter]
        public async Task<IActionResult> Put(int id, [FromBody] CarDto carDto)
        {
            if (!await CheckIfClientAsync())
                return StatusCode(StatusCodes.Status403Forbidden, "No client account has been found.");

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!await _clientService.CheckIfOwnerAsync(userId, id))
                return StatusCode(StatusCodes.Status403Forbidden, "Operation available only for the owner.");

            if (!await _carService.CarExistsAsync(id))
                return NotFound(new { Message = $"No car with ID { id } has been found." });

            var outcome = await _carService.UpdateCarAsync(id, carDto);

            if (outcome == null)
                return Conflict(new { Error = "Request unsuccessfull." });

            _logger.LogInformation("User {User} edited Car Model (id = {Id}) in db", HttpContext.User.Identity.Name, id);

            return Ok(outcome);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await CheckIfClientAsync())
                return StatusCode(StatusCodes.Status403Forbidden, "No client account has been found.");

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!await _clientService.CheckIfOwnerAsync(userId, id))
                return StatusCode(StatusCodes.Status404NotFound, "Operation available only for the owner.");

            if (!await _carService.CarExistsAsync(id))
                return BadRequest(new { Message = $"No car with ID { id } has been found." });

            if (!await _carService.DeleteCarAsync(userId, id))
                return Conflict(new { Error = "Request unsuccessfull." });

            return NoContent();
        }

        [NonAction]
        private async Task<bool> CheckIfClientAsync()
        {
            var id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!await _clientService.ClientExistsAsync(id))
                return false;

            return true;
        }
    }
}