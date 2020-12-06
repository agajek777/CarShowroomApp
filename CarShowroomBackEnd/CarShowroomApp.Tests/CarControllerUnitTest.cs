using AutoMapper;
using CarShowroom.Application.Services;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using CarShowroom.Domain.Models.Parameters;
using CarShowroom.Infra.Data.Repositories;
using CarShowroom.UI.Configuration;
using CarShowroom.UI.Controllers;
using CarShowroom.UI.Tests.Users;
using CarShowroomApp.Tests.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
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

            var carRepository = new CarRepository(dbContext, mapper, NullLogger<CarRepository>.Instance);

            var clientRepository = new ClientRepository(new CarShowroomMongoSettings(), mapper);

            List<User> _users = new List<User>
                 {
                      new User { Id = "1", UserName = "test1" },
                      new User { Id = "2", UserName = "test2" }
                 };

            var userManager = UserManagerMocker.MockUserManager(_users);

            var carService = new CarService(carRepository, new ClientService(clientRepository, userManager.Object));

            var clientService = new ClientService(clientRepository, userManager.Object);

            // Create API Controller
            var controller = new CarController(carService, clientService, new NullLogger<CarController>());

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

            var carRepository = new CarRepository(dbContext, mapper, NullLogger<CarRepository>.Instance);

            var clientRepository = new ClientRepository(new CarShowroomMongoSettings(), mapper);

            List<User> _users = new List<User>
                 {
                      new User { Id = "1", UserName = "test1" },
                      new User { Id = "2", UserName = "test2" }
                 };

            var userManager = UserManagerMocker.MockUserManager(_users);

            var carService = new CarService(carRepository, new ClientService(clientRepository, userManager.Object));

            var clientService = new ClientService(clientRepository, userManager.Object);

            // Create API Controller
            var controller = new CarController(carService, clientService, new NullLogger<CarController>());

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

            var carRepository = new CarRepository(dbContext, mapper, NullLogger<CarRepository>.Instance);

            var clientRepository = new ClientRepository(new CarShowroomMongoSettings(), mapper);

            List<User> _users = new List<User>
                 {
                      new User { Id = "1", UserName = "test1" },
                      new User { Id = "2", UserName = "test2" }
                 };

            var userManager = UserManagerMocker.MockUserManager(_users);

            var carService = new CarService(carRepository, new ClientService(clientRepository, userManager.Object));

            var clientService = new ClientService(clientRepository, userManager.Object);

            // Create API Controller
            var controller = new CarController(carService, clientService, new NullLogger<CarController>());

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
