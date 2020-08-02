﻿using AutoMapper;
using CarShowroomApp.Controllers;
using CarShowroomApp.Data;
using CarShowroomApp.Models;
using CarShowroomApp.Models.DTO;
using CarShowroomApp.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarShowroomApp.Tests.Data
{
    public class CarControllerUnitTest
    {
        [Fact]
        public void GetAll_ReturnsOkResult()
        {
            /*
             * Arrange
             */

            // Create DbContext
            var dbContext = DbContextMocker.GetDatabaseContext(nameof(GetAll_ReturnsOkResult));

            // Create Mapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            });
            var mapper = configuration.CreateMapper();

            // Create Repository
            var carRepository = new CarRepository(dbContext, mapper);

            // Create API Controller
            var controller = new CarController(carRepository, mapper);

            /*
             * Act
             */

            var response = controller.GetAll() as ObjectResult;

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
            var dbContext = DbContextMocker.GetDatabaseContext(nameof(GetAll_ReturnsOkResult));

            // Create Mapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            });
            var mapper = configuration.CreateMapper();

            // Create Repository
            var carRepository = new CarRepository(dbContext, mapper);

            // Create API Controller
            var controller = new CarController(carRepository, mapper);

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
    }
}