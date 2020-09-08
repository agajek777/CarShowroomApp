using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom.Application.Interfaces
{
    public interface IJwtService
    {
        public Task<object> GenerateJSONWebToken(User user);
        public Task<List<Claim>> GetValidClaims(User user);
    }
}
