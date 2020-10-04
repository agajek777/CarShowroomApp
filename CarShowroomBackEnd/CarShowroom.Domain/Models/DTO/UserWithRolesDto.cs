using System.Collections.Generic;

namespace CarShowroom.Domain.Models.DTO
{
    public class UserWithRolesDto
    {
        public UserDto User { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; }
    }
}
