using CarShowroom.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShowroom.Application.Interfaces
{
    public interface IJwtService
    {
        public object GenerateJSONWebToken(UserForRegisterDto user);
    }
}
