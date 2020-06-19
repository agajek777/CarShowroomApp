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
    }
}