namespace TrafficViolation.Domain;

public abstract class Vehicle
{
    public string PlateNumber { get; }
    public string Type { get; }

    protected Vehicle(string plateNumber, string type)
    {
        PlateNumber = plateNumber ?? throw new ArgumentNullException(nameof(plateNumber));
        Type = type ?? throw new ArgumentNullException(nameof(type));
    }
}
