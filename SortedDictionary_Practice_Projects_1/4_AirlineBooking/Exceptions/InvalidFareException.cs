namespace AirlineBooking.Exceptions;

public class InvalidFareException : BookingException
{
    public decimal Fare { get; }
    public InvalidFareException(decimal fare) : base($"Invalid fare amount: {fare}. Must be positive.") => Fare = fare;
}
