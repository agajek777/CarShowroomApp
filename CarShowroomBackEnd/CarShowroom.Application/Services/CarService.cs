﻿using CarShowroom.Application.Interfaces;
using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Parameters;
using System.Data;
using System.Threading.Tasks;

namespace CarShowroom.Application.Services
{
    public delegate Task<bool> CarCDHandler(string userId, int? carId);
    public class CarService : ICarService
    {
        private readonly ICarRepository<CarDto> _carRepository;
        public event CarCDHandler OnCarAdded;
        public event CarCDHandler OnCarDeleted;

        public CarService(ICarRepository<CarDto> carRepository, IClientService clientService)
        {
            _carRepository = carRepository;

            OnCarAdded += clientService.AddCarOfferAsync;
            OnCarDeleted += clientService.DeleteCarOfferAsync;
        }
        public async Task<CarDto> AddCarAsync(string id, CarDto carToAdd)
        {
            CarDto addedCar;

            try
            {
                addedCar = await _carRepository.AddAsync(carToAdd);
            }
            catch (DataException)
            {
                throw;
            }

            if (addedCar != null)
            {
                var result = await OnCarAdded?.Invoke(id, addedCar.Id);

                if (!result)
                {
                    await _carRepository.DeleteAsync((int)addedCar.Id);
                }
            }

            return addedCar;
        }

        public async Task<bool> DeleteCarAsync(string userId, int id)
        {
            bool result;
            try
            {
                 result = await _carRepository.DeleteAsync(id);
            }
            catch (DataException)
            {
                throw;
            }

            if (!result)
                return result;

            result = await OnCarDeleted?.Invoke(userId, id);

            return result;
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
        }

        public async Task<bool> CarExistsAsync(int id)
        {
            try
            {
                return await _carRepository.CarExistsAsync(id);
            }
            catch (DataException)
            {
                throw;
            }
        }
    }
}
