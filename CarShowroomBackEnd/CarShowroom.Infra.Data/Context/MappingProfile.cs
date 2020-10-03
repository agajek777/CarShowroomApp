using AutoMapper;
using CarShowroom.Domain.Models;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using CarShowroom.Domain.Models.Messaging;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShowroomApp.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarDto>().ReverseMap();

            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserForRegisterDto>().ReverseMap();

            CreateMap<MessagePostDto, Message>().ReverseMap();
        }
    }
}
