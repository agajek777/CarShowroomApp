using CarShowroom.Domain.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom.Application.Interfaces
{
    public interface ICarService
    {
        public IEnumerable<CarDto> GetAllCars();
        public Task<CarDto> GetCar(int id);
        public Task<CarDto> AddCar(CarDto carToAdd);
        public Task<CarDto> UpdateCar(int id, CarDto carToUpdate);
        public Task<IActionResult> DeleteCar(int id);
    }
}
