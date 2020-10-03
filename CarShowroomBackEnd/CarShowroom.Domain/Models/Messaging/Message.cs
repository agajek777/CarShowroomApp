using CarShowroom.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarShowroom.Domain.Models.Messaging
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Sent { get; set; }
        [ForeignKey("Sender")]
        [Required]
        public string SenderId { get; set; }
        public User Sender { get; set; }
        [ForeignKey("Receiver")]
        [Required]
        public string ReceiverId { get; set; }
        public User Receiver { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
