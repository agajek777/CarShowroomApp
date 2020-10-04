using AutoMapper;
using CarShowroom.Domain.Models;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using CarShowroom.Domain.Models.Messaging;

namespace CarShowroom.UI.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarDto>().ReverseMap();

            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserForRegisterDto>().ReverseMap();

            CreateMap<Message, MessagePostDto>().ReverseMap();
            CreateMap<Message, MessageGetDto>().ReverseMap();
        }
    }
}
