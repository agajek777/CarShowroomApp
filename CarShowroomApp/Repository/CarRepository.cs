using AutoMapper;
using CarShowroomApp.Data;
using CarShowroomApp.Models;
using CarShowroomApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShowroomApp.Repository
{
    public class CarRepository : ICarRepository<Car, CarDto>
    {
        private readonly DatabaseContext _db;
        private readonly IMapper _mapper;

        public CarRepository(DatabaseContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public void Add(Car entity)
        {
            _db.Cars.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(Car entity)
        {
            _db.Cars.Remove(entity);
            _db.SaveChanges();
        }

        public Car Get(int id)
        {
            return _db.Cars.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Car> GetAll()
        {
            return _db.Cars.ToList();
        }
        public void Update(Car dbEntity, CarDto entity)
        {
            dbEntity = _mapper.Map<Car>(entity);
            _db.SaveChanges();

        }
    }
}
