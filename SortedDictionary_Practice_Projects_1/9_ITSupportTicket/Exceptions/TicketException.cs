namespace ITSupportTicket.Exceptions;

public abstract class TicketException : Exception
{
    protected TicketException(string message) : base(message) { }
    protected TicketException(string message, Exception inner) : base(message, inner) { }
}
