namespace AirlineBooking.Exceptions;

public abstract class BookingException : Exception
{
    protected BookingException(string message) : base(message) { }
    protected BookingException(string message, Exception inner) : base(message, inner) { }
}
