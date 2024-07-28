using CarShowroom.Offers.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarShowroom.Offers.Storage;

public class CarsContext : DbContext
{
    public DbSet<Car> Cars { get; set; }

    public DbSet<Owner> Owners { get; set; }
}
