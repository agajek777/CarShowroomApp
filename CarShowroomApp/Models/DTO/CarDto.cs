using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShowroomApp.Models.DTO
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Engine { get; set; }
        public int Power { get; set; }
        public DateTime Production { get; set; }
        public double Price { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public double Mileage { get; set; }
    }
}
