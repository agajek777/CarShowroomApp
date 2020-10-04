using System.ComponentModel.DataAnnotations;

namespace CarShowroom.Domain.Models.DTO
{
    public class MessagePostDto
    {
        [Required]
        public string ReceiverId { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
