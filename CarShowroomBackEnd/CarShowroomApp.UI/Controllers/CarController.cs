using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarShowroom.Application.Interfaces;
using CarShowroom.Domain.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CarShowroom.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;

        public CarController(ICarService carService)
        {
            this._carService = carService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var outcome = _carService.GetAllCars();
            return outcome == null ? (IActionResult)BadRequest() : Ok(outcome);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var carInDb = await _carService.GetCar(id);
            if (carInDb == null)
            {
                return BadRequest();
            }
            return Ok(carInDb);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CarDto carDto)
        {
            if (ModelState.IsValid)
            {
                CarDto model = await _carService.AddCar(carDto);
                return Ok(model);
            }
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CarDto carDto)
        {
            if (ModelState.IsValid)
            {
                var outcome = await _carService.UpdateCar(id, carDto);
                return Ok(outcome);
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _carService.DeleteCar(id);
        }
    }
}