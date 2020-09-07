using AutoMapper;
using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using CarShowroom.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public CarRepository(DatabaseContext<User, Role> db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public IEnumerable<CarDto> GetAll()
        {
            IEnumerable<CarDto> result;
            try
            {
                result = _db.Cars.ToListAsync().Result.Select(p => _mapper.Map<CarDto>(p));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result = null;
            }
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
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
                return new BadRequestResult();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                return new BadRequestResult();
            }

            _db.Cars.Remove(carInDb);
            _db.SaveChanges();

            return new OkResult();
        }
    }
}
