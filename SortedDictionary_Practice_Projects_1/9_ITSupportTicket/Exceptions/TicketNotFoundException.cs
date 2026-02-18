namespace ITSupportTicket.Exceptions;

public class TicketNotFoundException : TicketException
{
    public string TicketId { get; }
    public TicketNotFoundException(string ticketId) : base($"Ticket not found: {ticketId}.") => TicketId = ticketId;
}
