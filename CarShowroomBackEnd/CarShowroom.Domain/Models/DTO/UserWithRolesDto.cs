using System.Collections.Generic;

namespace CarShowroom.Domain.Models.DTO
{
    public class UserWithRolesDto
    {
        public UsernameDto User { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; }
    }
}
