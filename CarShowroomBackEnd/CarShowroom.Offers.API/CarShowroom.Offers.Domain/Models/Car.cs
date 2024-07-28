using CarShowroom.Offers.Domain.Exceptions;

namespace CarShowroom.Offers.Domain.Models;

public class Car
{
    private const int MaxPrice = 10_000_000;
    private const int MinProductionYear = 1950;

    public Car(string brand, string model, string engine, int? power, DateOnly production, double price, string imagePath, string description, double mileage, Guid ownerId)
    {
        Id = Guid.NewGuid();

        Brand = brand;
        Model = model;
        Engine = engine;
        Power = power;
        Production = production;
        Price = price;
        ImagePath = imagePath;
        Description = description;
        Mileage = mileage;
        OwnerId = ownerId;

        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Brand))
        {
            throw new DomainException();
        }

        if (string.IsNullOrWhiteSpace(Model))
        {
            throw new DomainException();
        }

        if (Power.HasValue && Power is < 0)
        {
            throw new DomainException();
        }

        if (Production == DateOnly.MinValue || Production < DateOnly.FromDateTime(new DateTime(MinProductionYear, 1, 1)))
        {
            throw new DomainException();
        }

        if (Mileage is < 0.0)
        {
            throw new DomainException();
        }

        if (Price is < 0.0 or > MaxPrice)
        {
            throw new DomainException();
        }
    }

    public Guid Id { get; private set; }

    public string Brand { get; private set; } = string.Empty;

    public string Model { get; private set; } = string.Empty;

    public string Engine { get; private set; } = string.Empty;

    public int? Power { get; private set; }

    public DateOnly Production { get; private set; }

    public double Price { get; private set; }

    public string ImagePath { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public double Mileage { get; private set; }

    public Owner Owner { get; private set; } = null!;

    public Guid OwnerId { get; private set; }

    public bool UpdatePrice(double newPrice)
    {
        if (newPrice is < 0.0 or > MaxPrice)
        {
            throw new DomainException();
        }

        if (Price == newPrice) 
        {
            return false;
        }

        Price = newPrice;

        return true;
    }
}
