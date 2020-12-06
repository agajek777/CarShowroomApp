using AutoMapper;
using CarShowroom.Application.Interfaces;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using CarShowroom.Domain.Models.Parameters;
using CarShowroom.UI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShowroom.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ICarService _carService;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ClientController> _logger;
        private readonly IMapper _mapper;

        public ClientController(IClientService clientService, ICarService carService, UserManager<User> userManager, ILogger<ClientController> logger, IMapper mapper)
        {
            _clientService = clientService;
            _carService = carService;
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        [Cached(600)]
        public IActionResult GetAllAsync([FromQuery] QueryParameters queryParameters)
        {
            var outcome = _clientService.GetAllClients(queryParameters);

            _logger.LogInformation("User {User} obtained {Num} Clients Models from db", HttpContext.User.Identity.Name, outcome.Count);

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
        public async Task<IActionResult> Get(string id)
        {
            if (!await _clientService.ClientExistsAsync(id))
                return BadRequest(new { Message = $"No client with ID { id } has been found." });

            var clientInDb = await _clientService.GetClientAsync(id);

            if (clientInDb == null)
                return Conflict(new { Error = "Request unsuccessfull." });


            _logger.LogInformation("User {User} obtained Client Model from db", HttpContext.User.Identity.Name);

            return Ok(clientInDb);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClientDto clientToAdd)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid client model. Please provide Identity Id.");

            if ((await _userManager.FindByIdAsync(clientToAdd.IdentityId)) == null)
                return BadRequest($"No user with prvided user Id {clientToAdd.IdentityId} has been found.");

            if (clientToAdd.Offers != null)
            {
                if (clientToAdd.Offers?.Count != 0)
                {
                    foreach (var offer in clientToAdd.Offers)
                    {
                        if (!await _carService.CarExistsAsync(offer.Id))
                            return BadRequest($"No car with provided Id {offer.Id} has been found.");
                    }
                }
            }

            var clientInDb = await _clientService.AddClientAsync(clientToAdd);

            if (clientInDb == null)
                return Conflict(new { Error = "Request unsuccessfull." });

            return Ok(clientInDb);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!await _clientService.ClientExistsAsync(id))
                return BadRequest($"No user with provided Id {id} has been found.");

            var result = await _clientService.DeleteClientAsync(id);

            if (!result)
                return Conflict(new { Error = "Request unsuccessfull." });

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] ClientDto clientDto)
        {
            if (!await _clientService.ClientExistsAsync(id))
                return BadRequest($"No user with provided Id {id} has been found.");

            var outcome = await _clientService.UpdateClientAsync(id, clientDto);

            if (outcome == null)
                return Conflict(new { Error = "Request unsuccessfull." });

            _logger.LogInformation("User {User} edited Car Model (id = {Id}) in db", HttpContext.User.Identity.Name, id);

            return Ok(outcome);
        }
    }
}