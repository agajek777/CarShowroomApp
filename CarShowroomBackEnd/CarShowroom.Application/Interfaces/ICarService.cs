using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Parameters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom.Application.Interfaces
{
    public interface ICarService
    {
        public Task<PagedList<CarDto>> GetAllCarsAsync(QueryParameters queryParameters);
        public Task<CarDto> GetCarAsync(int id);
        public Task<CarDto> AddCarAsync(CarDto carToAdd);
        public Task<CarDto> UpdateCarAsync(int id, CarDto carToUpdate);
        public Task<bool> DeleteCarAsync(int id);
        public Task<bool> CarExists(int id);
    }
}
