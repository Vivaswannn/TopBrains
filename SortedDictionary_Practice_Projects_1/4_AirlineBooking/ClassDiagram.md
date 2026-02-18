# 4. Airline Booking Fare Classification — Class Diagram

```
                    <<abstract>>
                      Ticket
    + TicketId, FlightId, SeatNumber, BaseFare
    + CalculateFare(): decimal  (polymorphic)
           △
           |
    +------+------+-----------+
    |             |           |
 Economy     Business    FirstClass
 (1x)        (2.5x)      (5x)
```

**Exceptions:** `BookingException` → `SeatAlreadyBookedException`, `InvalidFareException`

**Core:** `SortedDictionary<decimal, List<Ticket>>` — key = fare category. Prevent duplicate seat via HashSet.
