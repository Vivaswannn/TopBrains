namespace AirlineBooking.Domain;

public class EconomyTicket : Ticket
{
    public EconomyTicket(string ticketId, string flightId, string seatNumber, decimal baseFare)
        : base(ticketId, flightId, seatNumber, baseFare) { }

    public override decimal CalculateFare() => BaseFare;
}
