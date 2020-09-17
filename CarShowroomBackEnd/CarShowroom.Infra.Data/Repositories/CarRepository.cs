﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using CarShowroom.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom.Infra.Data.Repositories
{
    public class CarRepository : ICarRepository<Car, CarDto>
    {
        private readonly DatabaseContext<User, Role> _db;
        private readonly IMapper _mapper;
        private readonly ILogger<CarRepository> _logger;

        public CarRepository(DatabaseContext<User, Role> db, IMapper mapper, ILogger<CarRepository> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IQueryable<CarDto>> GetAllAsync()
        {
            IQueryable<CarDto> result;

            try
            {
                result = _db.Cars.ProjectTo<CarDto>(_mapper.ConfigurationProvider, p => _mapper.Map<CarDto>(p));
            }
            catch (Exception ex)
            {
                _logger.LogWarning("GetAll() got exception: {Exception}", ex.Message);
                return null;
            }

            _logger.LogInformation("GetAll() obtained {Num} Car Models.", await result.CountAsync());

            return result;
        }

        public async Task<CarDto> Get(int id)
        {
            Car outcome;

            try
            {
                outcome = await _db.Cars.SingleAsync(a => a.Id == id);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning("Get() got exception: {Exception}", typeof(InvalidOperationException).Name, ex.Message);
                return null;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Get() got exception: {Exception}", typeof(InvalidOperationException).Name, ex.Message);
                return null;
            }

            return _mapper.Map<CarDto>(outcome);
        }

        public async Task<CarDto> Add(CarDto entity)
        {
            Car model = _mapper.Map<Car>(entity);
            await _db.Cars.AddAsync(model);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Add() got exception: {Exception}", ex.Message);
            }
            return _mapper.Map<CarDto>(model);
        }

        public async Task<CarDto> Update(int id, CarDto entity)
        {
            Car outcome;

            try
            {
                outcome = await _db.Cars.SingleAsync(a => a.Id == id);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning("Update() got exception: {Exception} - {Message}", typeof(ArgumentNullException).Name, ex.Message);
                return null;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Update() got exception: {Exception}", typeof(InvalidOperationException).Name, ex.Message);
                return null;
            }

            outcome = _mapper.Map<CarDto, Car>(entity, outcome);

            _db.Update(outcome);
            await _db.SaveChangesAsync();

            return _mapper.Map<CarDto>(outcome);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Car carInDb;

            try
            {
                carInDb = await _db.Cars.SingleAsync(a => a.Id == id);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning("Delete() got exception: {Exception} - {Message}", typeof(ArgumentNullException).Name, ex.Message);
                return new BadRequestResult();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Delete() got exception: {Exception}", typeof(InvalidOperationException).Name, ex.Message);
                return new BadRequestResult();
            }

            _db.Cars.Remove(carInDb);
            _db.SaveChanges();

            return new OkResult();
        }
    }
}
