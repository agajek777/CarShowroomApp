using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using CarShowroom.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

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

            if (!await CheckConnectionAsync())
                throw new DataException("Can't connect to the db.");

            result = _db.Cars.ProjectTo<CarDto>(_mapper.ConfigurationProvider, p => _mapper.Map<CarDto>(p));

            _logger.LogInformation("GetAll() obtained {Num} Car Models.", await result.CountAsync());

            return result;
        }

        public async Task<CarDto> GetAsync(int id)
        {
            Car outcome;

            if (!await CheckConnectionAsync())
                throw new DataException("Can't connect to the db.");

            try
            {
                outcome = await _db.Cars.SingleAsync(a => a.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Get() got exception: {Exception} --- {Message}", typeof(InvalidOperationException).Name, ex.Message);
                throw;
            }

            return _mapper.Map<CarDto>(outcome);
        }

        public async Task<CarDto> AddAsync(CarDto entity)
        {
            if (!await CheckConnectionAsync())
                throw new DataException("Can't connect to the db.");

            Car model = _mapper.Map<Car>(entity);

            await _db.Cars.AddAsync(model);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogWarning("Add() got exception: {Exception} --- {Message}", typeof(DbUpdateConcurrencyException).Name, ex.Message);
                throw;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning("Add() got exception: {Exception} --- {Message}", typeof(DbUpdateException).Name, ex.Message);
                throw;
            }
            return _mapper.Map<CarDto>(model);
        }

        public async Task<CarDto> UpdateAsync(int id, CarDto entity)
        {
            Car outcome;

            if (!await CheckConnectionAsync())
                throw new DataException("Can't connect to the db.");

            try
            {
                outcome = await _db.Cars.SingleAsync(a => a.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Update() got exception: {Exception} --- {Message}", typeof(InvalidOperationException).Name, ex.Message);
                throw;
            }

            outcome = _mapper.Map<CarDto, Car>(entity, outcome);

            _db.Update(outcome);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogWarning("Add() got exception: {Exception} --- {Message}", typeof(DbUpdateConcurrencyException).Name, ex.Message);
                throw;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning("Add() got exception: {Exception} --- {Message}", typeof(DbUpdateException).Name, ex.Message);
                throw;
            }

            return _mapper.Map<CarDto>(outcome);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Car carInDb;

            if (!await CheckConnectionAsync())
                throw new DataException("Can't connect to the db.");

            try
            {
                carInDb = await _db.Cars.SingleAsync(a => a.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Delete() got exception: {Exception} --- {Message}", typeof(InvalidOperationException).Name, ex.Message);
                throw;
            }

            _db.Cars.Remove(carInDb);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogWarning("Delete() got exception: {Exception} --- {Message}", typeof(DbUpdateConcurrencyException).Name, ex.Message);
                throw;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning("Delete() got exception: {Exception} --- {Message}", typeof(DbUpdateException).Name, ex.Message);
                throw;
            }

            return true;
        }

        #region HelperMethods
        private async Task<bool> CheckConnectionAsync()
        {
            try
            {
                await _db.GetService<IRelationalDatabaseCreator>().CanConnectAsync();
            }
            catch (SqlException ex)
            {
                _logger.LogWarning("GetAllAsync() got exception: {Exception} --- {Message}", nameof(SqlException), ex.Message);
                return false;
            }
            return true;
        }
        #endregion
    }
}
