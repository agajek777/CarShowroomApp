using CarShowroom.Application.Interfaces;
using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Parameters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CarShowroom.Application.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository<Car, CarDto> _carRepository;

        public CarService(ICarRepository<Car, CarDto> carRepository)
        {
            _carRepository = carRepository;
        }
        public async Task<CarDto> AddCar(CarDto carToAdd)
        {
            return await _carRepository.Add(carToAdd);
        }

        public async Task<IActionResult> DeleteCar(int id)
        {
            return await _carRepository.Delete(id);
        }

        public async Task<PagedList<CarDto>> GetAllCarsAsync(QueryParameters queryParameters)
        {
            try
            {
                return PagedList<CarDto>.ToPagedList(await _carRepository.GetAllAsync(),
                                                queryParameters.PageNumber,
                                                queryParameters.PageSize);
            }
            catch (DataException)
            {
                throw;
            }
        }

        public async Task<CarDto> GetCar(int id)
        {
            return await _carRepository.Get(id);
        }

        public async Task<CarDto> UpdateCar(int id, CarDto carToUpdate)
        {
            return await _carRepository.Update(id, carToUpdate);
        }
    }
}
