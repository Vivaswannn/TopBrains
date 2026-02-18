using AirlineBooking.Domain;
using AirlineBooking.Exceptions;

namespace AirlineBooking.Services;

/// <summary>SortedDictionary&lt;decimal, List&lt;Ticket&gt;&gt; — key = price category (fare).</summary>
public class FareClassificationService
{
    private readonly SortedDictionary<decimal, List<Ticket>> _byFareCategory = new();
    private readonly HashSet<string> _bookedSeats = new(StringComparer.OrdinalIgnoreCase);

    private static string SeatKey(string flightId, string seatNumber) => $"{flightId}|{seatNumber}";

    public void AddTicket(Ticket ticket)
    {
        if (ticket == null) throw new ArgumentNullException(nameof(ticket));
        if (ticket.BaseFare <= 0) throw new InvalidFareException(ticket.BaseFare);
        var key = SeatKey(ticket.FlightId, ticket.SeatNumber);
        if (_bookedSeats.Contains(key))
            throw new SeatAlreadyBookedException(ticket.FlightId, ticket.SeatNumber);

        var fare = ticket.CalculateFare();
        if (!_byFareCategory.ContainsKey(fare)) _byFareCategory[fare] = new List<Ticket>();
        _byFareCategory[fare].Add(ticket);
        _bookedSeats.Add(key);
    }

    public IReadOnlyDictionary<decimal, List<Ticket>> GetTicketsByFareCategory() => _byFareCategory;
}
