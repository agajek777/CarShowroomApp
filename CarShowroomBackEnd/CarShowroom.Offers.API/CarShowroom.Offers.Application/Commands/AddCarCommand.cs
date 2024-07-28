using MediatR;

namespace CarShowroom.Offers.Application.Commands;

public class AddCarCommand : IRequest<Guid>
{
    public string Brand { get; private set; } = string.Empty;

    public string Model { get; private set; } = string.Empty;

    public string Engine { get; private set; } = string.Empty;

    public int? Power { get; private set; }

    public DateOnly Production { get; private set; }

    public double Price { get; private set; }

    public string ImagePath { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public double Mileage { get; private set; }

    public Guid OwnerId { get; private set; }
}
