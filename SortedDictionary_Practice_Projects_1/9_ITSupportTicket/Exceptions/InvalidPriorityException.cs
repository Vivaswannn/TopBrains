namespace ITSupportTicket.Exceptions;

public class InvalidPriorityException : TicketException
{
    public int Priority { get; }
    public InvalidPriorityException(int priority) : base($"Invalid priority: {priority}. Must be 1 (Critical) to 5.") => Priority = priority;
}
