using CarShowroom.Offers.Domain;
using CarShowroom.Offers.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarShowroom.Offers.Storage.Repositories;

public class CarDomainRepository : ICarDomainRepository
{
    private readonly CarsContext _context;

    public CarDomainRepository(CarsContext context)
    {
        _context = context;
    }

    public Task<List<Car>> FindByOwner(Guid ownerId, CancellationToken cancellationToken) => _context.Cars
            .Where(c => c.OwnerId == ownerId)
            .ToListAsync(cancellationToken);

    public async Task<bool> Add(Car newCar, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(newCar);

        _context.Cars.Add(newCar);

        var result = await _context.SaveChangesAsync(cancellationToken);

        return result != 0;
    }

    public async Task<bool> Remove(Car car, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(car);

        _context.Cars.Remove(car);

        var result = await _context.SaveChangesAsync(cancellationToken);

        return result != 0;
    }
}
