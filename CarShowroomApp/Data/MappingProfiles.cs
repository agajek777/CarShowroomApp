using AutoMapper;
using CarShowroomApp.Models;
using CarShowroomApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShowroomApp.Data
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Car, CarDto>();
            CreateMap<CarDto, Car>();
        }
    }
}
