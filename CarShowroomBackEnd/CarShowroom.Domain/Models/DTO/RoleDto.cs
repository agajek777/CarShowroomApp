using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarShowroom.Domain.Models.DTO
{
    public class RoleDto
    {
        [Required]
        public string Name { get; set; }
    }
}
