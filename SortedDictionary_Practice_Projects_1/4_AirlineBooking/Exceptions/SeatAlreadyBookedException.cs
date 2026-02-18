namespace AirlineBooking.Exceptions;

public class SeatAlreadyBookedException : BookingException
{
    public string SeatNumber { get; }
    public string FlightId { get; }
    public SeatAlreadyBookedException(string flightId, string seatNumber)
        : base($"Seat {seatNumber} on flight {flightId} is already booked.") { FlightId = flightId; SeatNumber = seatNumber; }
}
