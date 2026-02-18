using AirlineBooking.Domain;
using AirlineBooking.Exceptions;
using AirlineBooking.Services;

var service = new FareClassificationService();

Ticket[] tickets =
{
    new EconomyTicket("T1", "FL101", "12A", 200m),
    new BusinessTicket("T2", "FL101", "5B", 200m),
    new FirstClassTicket("T3", "FL101", "1A", 200m)
};

foreach (var t in tickets)
{
    service.AddTicket(t);
    Console.WriteLine($"{t.GetType().Name}: Fare = {t.CalculateFare():C}");
}

try { service.AddTicket(new EconomyTicket("T4", "FL101", "12A", 150m)); }
catch (SeatAlreadyBookedException ex) { Console.WriteLine($"\nExpected: {ex.Message}"); }

Console.WriteLine("Done.");
