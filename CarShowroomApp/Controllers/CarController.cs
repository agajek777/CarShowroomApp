using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarShowroomApp.Models;
using CarShowroomApp.Models.DTO;
using CarShowroomApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarShowroomApp.Controllers
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
            return Ok(_carRepository.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var carInDb = _carRepository.Get(id);
            return Ok(_mapper.Map<CarDto>(carInDb));
        }
        [HttpPost]
        public IActionResult Post([FromBody] CarDto carDto)
        {
            if (ModelState.IsValid)
            {
                var car = _mapper.Map<Car>(carDto);
                _carRepository.Add(car);
                return Ok(Json(car));
            }
            return BadRequest();
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CarDto carDto)
        {
            if (ModelState.IsValid)
            {
                var carInDb = _carRepository.Get(id);
                if (carInDb == null)
                {
                    return BadRequest();
                }
                _carRepository.Update(carInDb, carDto);
                return Ok(carInDb);
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var car = _carRepository.Get(id);
            if (id == 0 || car == null)
            {
                return BadRequest();
            }
            _carRepository.Delete(car);
            return Ok();
        }
    }
}