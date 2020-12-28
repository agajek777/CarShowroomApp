﻿using AutoMapper;
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
        public async Task GetAll_ValidCallDbWorking_OkResultAsync()
        {
            var cars = new List<CarDto>
            {
                new CarDto(),
                new CarDto()
            };

            _carService
                .Setup(c => c.GetAllCarsAsync(It.IsAny<QueryParameters>()))
                .ReturnsAsync(
                    new PagedList<CarDto>(cars, 1, 1, 2)
                );

            var controller = SetupControllerWithContext();

            var response = await controller.GetAllAsync(new QueryParameters() { PageNumber = 1, PageSize = 1 }) as ObjectResult;

            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async Task Get_ValidIdDbWorking_OkResult()
        {
            _carService.Setup(c => c.CarExistsAsync(It.IsAny<int>())).ReturnsAsync(true);

            _carService.Setup(c => c.GetCarAsync(It.IsAny<int>())).ReturnsAsync(new CarDto());

            var controller = SetupControllerWithContext();

            var id = 3;

            var response = await controller.Get(id) as ObjectResult;

            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async Task Post_ValidCarModelDbWorking_OkResult()
        {
            _carService.Setup(c => c.AddCarAsync(It.IsAny<string>(), It.IsAny<CarDto>())).ReturnsAsync(new CarDto());

            _clientService.Setup(c => c.AddCarOfferAsync(It.IsAny<string>(), It.IsAny<int?>())).ReturnsAsync(true);
            _clientService.Setup(c => c.ClientExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            var controller = SetupControllerWithContext();

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

        [Fact]
        public async Task Put_ValidCarModelDbWorking_OkResult()
        {
            _carService.Setup(c => c.CarExistsAsync(It.IsAny<int>())).ReturnsAsync(true);
            _carService.Setup(c => c.UpdateCarAsync(It.IsAny<int>(), It.IsAny<CarDto>())).ReturnsAsync(new CarDto());

            _clientService.Setup(c => c.CheckIfOwnerAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(true);
            _clientService.Setup(c => c.ClientExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            var controller = SetupControllerWithContext();

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

            var id = 1;

            var response = await controller.Put(id, carDto) as ObjectResult;

            Assert.IsType<OkObjectResult>(response);
        }

        private CarController SetupControllerWithContext()
        {
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

            return controller;
        }
    }
}
