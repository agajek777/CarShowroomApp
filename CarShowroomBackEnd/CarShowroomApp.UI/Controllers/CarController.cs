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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CarShowroom.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            try
            {
                outcome = await _carService.GetAllCarsAsync(queryParameters);
            }
            catch (DataException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }

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

            try
            {
                carInDb = await _carService.GetCarAsync(id);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
            catch (DataException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }

            _logger.LogInformation("User {User} obtained Car Model from db", HttpContext.User.Identity.Name);

            return Ok(carInDb);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CarDto carDto)
        {
            if (ModelState.IsValid)
            {
                CarDto model;

                try
                {
                    model = await _carService.AddCarAsync(carDto);
                }
                catch (DataException ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
                }

                _logger.LogInformation("User {User} added Car Model to db", HttpContext.User.Identity.Name);

                return Ok(model);
            }
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CarDto carDto)
        {
            if (ModelState.IsValid)
            {
                CarDto outcome;

                try
                {
                    outcome = await _carService.UpdateCarAsync(id, carDto);
                }
                catch (DataException ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
                }

                _logger.LogInformation("User {User} edited Car Model in db", HttpContext.User.Identity.Name);

                return Ok(outcome);
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await _carService.DeleteCarAsync(id);
            }
            catch (DataException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
    }
}