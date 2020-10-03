﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarShowroom.Domain.Models.DTO
{
    public class MessageGetDto
    {
        [Required]
        public string SenderId { get; set; }
        [Required]
        public string ReceiverId { get; set; }
        [Required]
        public DateTime Sent { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
