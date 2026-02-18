using ITSupportTicket.Domain;
using ITSupportTicket.Exceptions;

namespace ITSupportTicket.Services;

/// <summary>SortedDictionary&lt;int, Queue&lt;SupportTicket&gt;&gt; — key = severity (1 = highest).</summary>
public class TicketSeverityService
{
    private readonly SortedDictionary<int, Queue<SupportTicket>> _bySeverity = new();
    private readonly Dictionary<string, (int severity, Queue<SupportTicket> queue)> _ticketIndex = new();

    public void AddTicket(SupportTicket ticket)
    {
        if (ticket == null) throw new ArgumentNullException(nameof(ticket));
        if (ticket.Severity < 1 || ticket.Severity > 5) throw new InvalidPriorityException(ticket.Severity);
        if (_ticketIndex.ContainsKey(ticket.TicketId)) return;

        if (!_bySeverity.ContainsKey(ticket.Severity))
            _bySeverity[ticket.Severity] = new Queue<SupportTicket>();
        var q = _bySeverity[ticket.Severity];
        q.Enqueue(ticket);
        _ticketIndex[ticket.TicketId] = (ticket.Severity, q);
    }

    public SupportTicket? ProcessTicket()
    {
        foreach (var kv in _bySeverity)
        {
            if (kv.Value.Count > 0)
            {
                var t = kv.Value.Dequeue();
                _ticketIndex.Remove(t.TicketId);
                return t;
            }
        }
        return null;
    }

    public void EscalateTicket(string ticketId, int newSeverity)
    {
        if (!_ticketIndex.TryGetValue(ticketId, out var pair))
            throw new TicketNotFoundException(ticketId);
        if (newSeverity < 1 || newSeverity > 5) throw new InvalidPriorityException(newSeverity);

        var q = pair.queue;
        var temp = new Queue<SupportTicket>();
        SupportTicket? target = null;
        while (q.Count > 0)
        {
            var t = q.Dequeue();
            if (t.TicketId == ticketId) { t.Severity = newSeverity; target = t; }
            else temp.Enqueue(t);
        }
        while (temp.Count > 0) q.Enqueue(temp.Dequeue());
        _ticketIndex.Remove(ticketId);
        if (target != null) AddTicket(target);
    }

    public IReadOnlyDictionary<int, Queue<SupportTicket>> GetTicketsBySeverity() => _bySeverity;
}
