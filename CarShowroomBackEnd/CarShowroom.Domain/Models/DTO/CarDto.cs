using System;
using System.ComponentModel.DataAnnotations;

namespace CarShowroom.Domain.Models.DTO
{
    public class CarDto
    {
        public int? Id { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        public string Engine { get; set; }
        public int? Power { get; set; }
        [Required]
        public DateTime Production { get; set; }
        [Required]
        [Range(0.0, Double.MaxValue)]
        public double Price { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public double Mileage { get; set; }
    }
}
