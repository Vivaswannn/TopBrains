namespace AirlineBooking.Domain;

public abstract class Ticket
{
    public string TicketId { get; }
    public string FlightId { get; }
    public string SeatNumber { get; }
    public decimal BaseFare { get; }

    protected Ticket(string ticketId, string flightId, string seatNumber, decimal baseFare)
    {
        if (baseFare <= 0) throw new Exceptions.InvalidFareException(baseFare);
        TicketId = ticketId ?? throw new ArgumentNullException(nameof(ticketId));
        FlightId = flightId ?? throw new ArgumentNullException(nameof(flightId));
        SeatNumber = seatNumber ?? throw new ArgumentNullException(nameof(seatNumber));
        BaseFare = baseFare;
    }

    /// <summary>Polymorphic fare calculation.</summary>
    public abstract decimal CalculateFare();
}
