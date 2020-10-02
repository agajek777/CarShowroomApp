using AutoMapper;
using CarShowroom.Application.Services;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Parameters;
using CarShowroom.Infra.Data.Repositories;
using CarShowroom.UI.Controllers;
using CarShowroomApp.Data;
using CarShowroomApp.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarShowroom.UI.Tests.Data
{
    public class CarControllerUnitTest
    {
        [Fact]
        public async Task GetAll_ReturnsOkResultAsync()
        {
            /*
             * Arrange
             */

            // Create DbContext
            var dbContext = DbContextMocker.GetDatabaseContext(nameof(GetAll_ReturnsOkResultAsync));

            // Create Mapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = configuration.CreateMapper();

            var carService = new CarService(new CarRepository(dbContext, mapper, NullLogger<CarRepository>.Instance));

            // Create API Controller
            var controller = new CarController(carService, new NullLogger<CarController>());

            /*
             * Act
             */

            var response = await controller.GetAllAsync(new QueryParameters() { PageNumber = 1, PageSize = 1 }) as ObjectResult;

            dbContext.Dispose();


            /*
             * Assert
             */

            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async Task Get_ReturnsOkResult()
        {
            /*
             * Arrange
             */

            // Create DbContext
            var dbContext = DbContextMocker.GetDatabaseContext(nameof(GetAll_ReturnsOkResultAsync));

            // Create Mapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = configuration.CreateMapper();

            var carService = new CarService(new CarRepository(dbContext, mapper, NullLogger<CarRepository>.Instance));

            // Create API Controller
            var controller = new CarController(carService, new NullLogger<CarController>());

            var id = 3;

            /*
             * Act
             */

            var response = await controller.Get(id) as ObjectResult;

            dbContext.Dispose();


            /*
             * Assert
             */

            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async Task Post_ReturnsOkResult()
        {
            /*
             * Arrange
             */

            // Create DbContext
            var dbContext = DbContextMocker.GetDatabaseContext(nameof(GetAll_ReturnsOkResultAsync));

            // Create Mapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = configuration.CreateMapper();

            var carService = new CarService(new CarRepository(dbContext, mapper, NullLogger<CarRepository>.Instance));

            // Create API Controller
            var controller = new CarController(carService, new NullLogger<CarController>());

            var carDto = new CarDto()
            {
                Brand = "Jeep",
                Description = "Nice car",
                Engine = "4.0",
                ImagePath = "www.jeep.com",
                Mileage = 83931,
                Model = "Compass",
                Power = 231,
                Price = 39000,
                Production = new DateTime(2010, 4, 10)
            };

            /*
             * Act
             */

            var response = await controller.Post(carDto) as ObjectResult;

            dbContext.Dispose();


            /*
             * Assert
             */

            Assert.IsType<OkObjectResult>(response);
        }
    }
}
