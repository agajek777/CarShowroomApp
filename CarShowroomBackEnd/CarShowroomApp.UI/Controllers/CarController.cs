using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models;
using CarShowroom.Domain.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CarShowroom.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly ICarRepository<Car, CarDto> _carRepository;
        private readonly IMapper _mapper;

        public CarController(ICarRepository<Car, CarDto> carRepository, IMapper mapper)
        {
            this._carRepository = carRepository;
            this._mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var outcome = _carRepository.GetAll();
            return outcome == null ? (IActionResult)BadRequest() : Ok(outcome);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var carInDb = await _carRepository.Get(id);
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
                CarDto model = await _carRepository.Add(carDto);
                return Ok(model);
            }
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CarDto carDto)
        {
            if (ModelState.IsValid)
            {
                var outcome = await _carRepository.Update(id, carDto);
                return Ok(outcome);
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _carRepository.Delete(id);
        }
    }
}