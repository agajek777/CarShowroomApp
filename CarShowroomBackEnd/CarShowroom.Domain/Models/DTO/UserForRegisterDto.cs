using System.ComponentModel.DataAnnotations;

namespace CarShowroom.Domain.Models.DTO
{
    public class UserForRegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
