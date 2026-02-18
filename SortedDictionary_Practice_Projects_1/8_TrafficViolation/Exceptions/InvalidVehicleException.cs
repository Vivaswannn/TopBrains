namespace TrafficViolation.Exceptions;

public class InvalidVehicleException : ViolationException
{
    public string VehicleId { get; }
    public InvalidVehicleException(string vehicleId) : base($"Invalid vehicle: {vehicleId}.") => VehicleId = vehicleId;
}
