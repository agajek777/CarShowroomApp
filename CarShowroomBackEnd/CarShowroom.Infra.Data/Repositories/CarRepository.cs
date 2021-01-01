using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using CarShowroom.Infra.Data.Context;
using CarShowroom.Infra.Data.Repositories.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CarShowroom.Infra.Data.Repositories
{
    public class CarRepository : Repository, ICarRepository<CarDto>
    {
        public CarRepository(DatabaseContext<User, Role> db, IMapper mapper, ILogger<CarRepository> logger) : base(db, mapper, logger)
        {

        }

        public async Task<IQueryable<CarDto>> GetAllAsync()
        {
            if (!CheckConnection())
                throw new DataException("Can't connect to the db.");

            var result = _db.Cars.ProjectTo<CarDto>(_mapper.ConfigurationProvider, p => _mapper.Map<CarDto>(p));

            _logger.LogInformation("GetAll() obtained {Num} Car Models.", await result.CountAsync());

            return result;
        }

        public async Task<CarDto> GetAsync(int id)
        {
            if (!CheckConnection())
                throw new DataException("Can't connect to the db.");

            Car outcome;

            try
            {
                outcome = await _db.Cars.SingleAsync(a => a.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "GetAsync() got exception: {Message}", ex.Message);
                return null;
            }

            return _mapper.Map<CarDto>(outcome);
        }

        public async Task<CarDto> AddAsync(CarDto entity)
        {
            if (!CheckConnection())
                throw new DataException("Can't connect to the db.");

            var model = _mapper.Map<Car>(entity);

            await _db.Cars.AddAsync(model);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning(ex, "AddAsync() got exception: {Message}", ex.Message);
                return null;
            }
            return _mapper.Map<CarDto>(model);
        }

        public async Task<CarDto> UpdateAsync(int id, CarDto entity)
        {
            if (!CheckConnection())
                throw new DataException("Can't connect to the db.");

            var outcome = await _db.Cars.SingleAsync(a => a.Id == id);

            outcome = _mapper.Map<CarDto, Car>(entity, outcome);

            _db.Update(outcome);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning(ex, "UpdateAsync() got exception: {Message}", ex.Message);
                return null;
            }

            return _mapper.Map<CarDto>(outcome);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!CheckConnection())
                throw new DataException("Can't connect to the db.");

            var carInDb = await _db.Cars.SingleAsync(a => a.Id == id);
            

            _db.Cars.Remove(carInDb);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning(ex, "DeleteAsync() got exception: {Message}", ex.Message);
                return false;
            }

            return true;
        }

        public void DeleteAll(ICollection<Offer> offers)
        {
            var tasks = new List<Task>();

            foreach (var offer in offers)
            {
                tasks.Add(DeleteAsync(offer.Id));
            }

            Task.WaitAll(tasks.ToArray());
        }

        public async Task<bool> CarExistsAsync(int id)
        {
            if (!CheckConnection())
                throw new DataException("Can't connect to the db.");

            return await _db.Cars.AnyAsync(p => p.Id == id);
        }
    }
}
