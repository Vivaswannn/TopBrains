# 9. IT Support Ticket Severity — Class Diagram

```
                    <<abstract>>
                       User
    + Id, Name
           △
           |
    +------+------+
    |             |
 Employee      Admin
 + Department
```

**SupportTicket:** TicketId, Subject, Severity, Status, AssignedTo; ResolveTicket() (polymorphic)

**Exceptions:** `TicketException` → `TicketNotFoundException`, `InvalidPriorityException`

**Core:** `SortedDictionary<int, Queue<SupportTicket>>` — key = severity. Process = dequeue from lowest key; Escalate = move to higher severity.
