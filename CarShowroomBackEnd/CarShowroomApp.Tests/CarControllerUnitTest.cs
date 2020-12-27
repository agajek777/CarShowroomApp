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
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using CarShowroom.Application.Interfaces;

namespace CarShowroom.UI.Tests.Data
{
    public class CarControllerUnitTest
    {
        public Mock<ICarService> _carService { get; set; }
        public Mock<IClientService> _clientService { get; set; }
        public CarControllerUnitTest()
        {
            _carService = new Mock<ICarService>();
            _clientService = new Mock<IClientService>();

        }
        [Fact]
        public async Task GetAll_ValidCall_OkResultAsync()
        {
            /*
             * Arrange
             */

            // Create DbContext
            var dbContext = DbContextMocker.GetDatabaseContext(nameof(GetAll_ValidCall_OkResultAsync));

            // Create Mapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = configuration.CreateMapper();

            var carRepository = new CarRepository(dbContext, mapper, NullLogger<CarRepository>.Instance);

            var mongoClient = new Mock<IMongoClient>();
            var mongoDb = new Mock<IMongoDatabase>();
            var mongoCollection = new Mock<IMongoCollection<Client>>();

            var clientRepository = new ClientRepository(mongoClient.Object, mongoDb.Object, mongoCollection.Object, mapper, NullLogger<ClientRepository>.Instance);

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

            var fakeIdentity = new GenericIdentity("tester");

            var context = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(fakeIdentity)
                }
            };

            controller.ControllerContext = context;

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
            var dbContext = DbContextMocker.GetDatabaseContext(nameof(GetAll_ValidCall_OkResultAsync));

            // Create Mapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = configuration.CreateMapper();

            var carRepository = new CarRepository(dbContext, mapper, NullLogger<CarRepository>.Instance);

            var clientRepository = new ClientRepository(new CarShowroomMongoSettings(), mapper, NullLogger<ClientRepository>.Instance);

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
        public async Task Post_ValidCarModel_OkResult()
        {
            _carService.Setup(c => c.AddCarAsync(It.IsAny<string>(), It.IsAny<CarDto>())).ReturnsAsync(new CarDto());

            _clientService.Setup(c => c.AddCarOfferAsync(It.IsAny<string>(), It.IsAny<int?>())).ReturnsAsync(true);
            _clientService.Setup(c => c.ClientExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            // Create API Controller
            var controller = new CarController(_carService.Object, _clientService.Object, new NullLogger<CarController>());

            var fakeIdentity = new GenericIdentity("tester");

            var context = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(fakeIdentity)
                }
            };

            controller.ControllerContext = context;

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

            var response = await controller.Post(carDto) as ObjectResult;

            Assert.IsType<OkObjectResult>(response);
        }
    }
}
