using System.ComponentModel.DataAnnotations;

namespace CarShowroom.Domain.Models.DTO
{
    public class RoleDto
    {
        [Required]
        public string Name { get; set; }
    }
}
