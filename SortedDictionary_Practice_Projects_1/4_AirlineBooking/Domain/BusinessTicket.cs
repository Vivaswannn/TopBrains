namespace AirlineBooking.Domain;

public class BusinessTicket : Ticket
{
    private const decimal Multiplier = 2.5m;
    public BusinessTicket(string ticketId, string flightId, string seatNumber, decimal baseFare)
        : base(ticketId, flightId, seatNumber, baseFare) { }

    public override decimal CalculateFare() => BaseFare * Multiplier;
}
