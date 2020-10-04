using CarShowroom.Domain.Models.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarShowroom.Application.Interfaces
{
    public interface IJwtService
    {
        public Task<object> GenerateJSONWebToken(User user);
        public Task<List<Claim>> GetValidClaims(User user);
    }
}
