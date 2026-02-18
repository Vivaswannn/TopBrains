namespace ITSupportTicket.Domain;

public class SupportTicket
{
    public string TicketId { get; }
    public string Subject { get; }
    public int Severity { get; set; } // 1 = highest
    public string Status { get; set; } = "Open";
    public User? AssignedTo { get; set; }

    public SupportTicket(string ticketId, string subject, int severity)
    {
        if (severity < 1 || severity > 5) throw new Exceptions.InvalidPriorityException(severity);
        TicketId = ticketId ?? throw new ArgumentNullException(nameof(ticketId));
        Subject = subject ?? "";
        Severity = severity;
    }

    /// <summary>Override in user types for polymorphic resolve behavior.</summary>
    public virtual string ResolveTicket() => $"Ticket {TicketId} resolved (severity {Severity}).";
}
