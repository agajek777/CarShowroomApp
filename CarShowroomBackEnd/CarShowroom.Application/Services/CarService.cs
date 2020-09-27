using CarShowroom.Application.Interfaces;
using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Parameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<CarDto> AddCarAsync(CarDto carToAdd)
        {
            try
            {
                return await _carRepository.AddAsync(carToAdd);
            }
            catch (DataException)
            {
                throw;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public async Task<bool> DeleteCarAsync(int id)
        {
            try
            {
                return await _carRepository.DeleteAsync(id);
            }
            catch (DataException)
            {
                throw;
            }
            catch (DbUpdateException)
            {
                return false;
            }
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

        public async Task<CarDto> GetCarAsync(int id)
        {
            try
            {
                return await _carRepository.GetAsync(id);
            }
            catch (DataException)
            {
                throw;
            }
        }

        public async Task<CarDto> UpdateCarAsync(int id, CarDto carToUpdate)
        {
            try
            {
                return await _carRepository.UpdateAsync(id, carToUpdate);
            }
            catch (DataException)
            {
                throw;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public async Task<bool> CarExistsAsync(int id)
        {
            return await _carRepository.CarExistsAsync(id);
        }
    }
}
