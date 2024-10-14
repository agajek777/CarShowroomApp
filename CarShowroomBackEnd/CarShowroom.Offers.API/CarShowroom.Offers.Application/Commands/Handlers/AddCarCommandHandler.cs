using CarShowroom.Offers.Domain;
using CarShowroom.Offers.Domain.Models;
using MediatR;

namespace CarShowroom.Offers.Application.Commands.Handlers;

public class AddCarCommandHandler : IRequestHandler<AddCarCommand, Guid>
{
    private readonly ICarDomainRepository _carRepository;

    public AddCarCommandHandler(ICarDomainRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<Guid> Handle(AddCarCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        var newCar = new Car(
            request.Brand,
            request.Model,
            request.Engine,
            request.Power,
            request.Production,
            request.Price,
            request.ImagePath,
            request.Description,
            request.Mileage,
            request.OwnerId);

        var result = await _carRepository.Add(newCar, cancellationToken);

        if (result is false)
        {
            throw new InvalidOperationException();
        }

        return newCar.Id;
    }
}
