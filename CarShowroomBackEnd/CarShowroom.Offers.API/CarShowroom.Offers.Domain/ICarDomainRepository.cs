using CarShowroom.Offers.Domain.Models;

namespace CarShowroom.Offers.Domain;

public interface ICarDomainRepository
{
    Task<List<Car>> FindByOwner(Guid ownerId, CancellationToken cancellationToken);

    Task<bool> Add(Car newCar, CancellationToken cancellationToken);

    Task<bool> Remove(Car car, CancellationToken cancellationToken);
}
