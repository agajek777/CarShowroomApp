using CarShowroom.Messages.Users;
using CarShowroom.Offers.Consumer.Models;
using CarShowroom.Offers.Domain;
using MediatR;

namespace CarShowroom.Offers.Consumer.Handlers.Users;

public class UserRemovedEventHandler(ICarDomainRepository carRepository) : INotificationHandler<DomainMessage<UserRemovedEvent>>
{
    private readonly ICarDomainRepository _carRepository = carRepository;

    public async Task Handle(DomainMessage<UserRemovedEvent> notification, CancellationToken cancellationToken)
    {
        var message = notification.Message;

        var ownerId = message.Id;

        var cars = await _carRepository.FindByOwner(ownerId, cancellationToken);

        foreach (var car in cars)
        {
            await _carRepository.Remove(car, cancellationToken);
        }
    }
}
