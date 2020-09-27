using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarShowroom.Application.Interfaces;
using CarShowroom.Domain.Models;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Parameters;
using CarShowroom.UI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;

namespace CarShowroom.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ExceptionHandlingFilter]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly ILogger<CarController> _logger;

        public CarController(ICarService carService, ILogger<CarController> logger)
        {
            _carService = carService;
            _logger = logger;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] QueryParameters queryParameters)
        {
            PagedList<CarDto> outcome;

            outcome = await _carService.GetAllCarsAsync(queryParameters);

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

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


            return Ok(outcome);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            CarDto carInDb;

            if (!await _carService.CarExistsAsync(id))
                return BadRequest(new { Message = $"No car with ID { id } has been found." });

            carInDb = await _carService.GetCarAsync(id);

            _logger.LogInformation("User {User} obtained Car Model from db", HttpContext.User.Identity.Name);

            return Ok(carInDb);
        }
        [HttpPost]
        [ModelValidationFilter]
        public async Task<IActionResult> Post([FromBody] CarDto carDto)
        {
            CarDto model;

            model = await _carService.AddCarAsync(carDto);

            _logger.LogInformation("User {User} added Car Model to db", HttpContext.User.Identity.Name);

            return Ok(model);
        }
        [HttpPut("{id}")]
        [ModelValidationFilter]
        public async Task<IActionResult> Put(int id, [FromBody] CarDto carDto)
        {
            CarDto outcome;

            if (!await _carService.CarExistsAsync(id))
                return BadRequest(new { Message = $"No car with ID { id } has been found." });

            outcome = await _carService.UpdateCarAsync(id, carDto);

            _logger.LogInformation("User {User} edited Car Model (id = {Id}) in db", HttpContext.User.Identity.Name, id);

            return Ok(outcome);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _carService.CarExistsAsync(id))
                return BadRequest(new { Message = $"No car with ID { id } has been found." });

            if (!await _carService.DeleteCarAsync(id))
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Request unsuccessfull." });

            return NoContent();
        }
    }
}