using CarShowroom.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShowroom.Infra.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); modelBuilder.Entity<Car>().HasData(
                 new Car
                 {
                     Id = 1,
                     Brand = "Audi",
                     Model = "RS6",
                     Engine = "V10 5.2",
                     Power = 580,
                     Production = new System.DateTime(2012, 1, 2),
                     Price = 120000,
                     ImagePath = "https://apollo-ireland.akamaized.net/v1/files/eyJmbiI6IjV1Z3FkOTBqbzBqNzEtT1RPTU9UT1BMIiwidyI6W3siZm4iOiJ3ZzRnbnFwNnkxZi1PVE9NT1RPUEwiLCJzIjoiMTYiLCJwIjoiMTAsLTEwIiwiYSI6IjAifV19.RWMwGPIAdM3kRNxqXNsJhlDtpAK4npaZ2LUk4TheVJE/image;s=1080x720;cars_;/937787999_;slot=10;filename=eyJmbiI6IjV1Z3FkOTBqbzBqNzEtT1RPTU9UT1BMIiwidyI6W3siZm4iOiJ3ZzRnbnFwNnkxZi1PVE9NT1RPUEwiLCJzIjoiMTYiLCJwIjoiMTAsLTEwIiwiYSI6IjAifV19.RWMwGPIAdM3kRNxqXNsJhlDtpAK4npaZ2LUk4TheVJE_rev001.jpg",
                     Mileage = 130000,
                     Description = "Autko w super stanie! Garażowane! Od fanatyka!"
                 },
                 new Car
                 {
                     Id = 2,
                     Brand = "Audi",
                     Model = "R8",
                     Engine = "V10 5.2",
                     Power = 525,
                     Production = new System.DateTime(2010, 3, 24),
                     Price = 320000,
                     ImagePath = "http://blog.ozonee.pl/wp-content/uploads/2018/07/Audi-R8-V10.jpg",
                     Mileage = 54000,
                     Description = "Niestety koszty utrzymania samochodu to fanaberia!"
                 });
        }
    }
}
