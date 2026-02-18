namespace AirlineBooking.Domain;

public class FirstClassTicket : Ticket
{
    private const decimal Multiplier = 5m;
    public FirstClassTicket(string ticketId, string flightId, string seatNumber, decimal baseFare)
        : base(ticketId, flightId, seatNumber, baseFare) { }

    public override decimal CalculateFare() => BaseFare * Multiplier;
}
