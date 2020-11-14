using System;
using System.ComponentModel.DataAnnotations;

namespace CarShowroom.Domain.Models.DTO
{
    public class MessageGetDto
    {
        [Required]
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        [Required]
        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        [Required]
        public DateTime Sent { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
