using ITSupportTicket.Domain;
using ITSupportTicket.Exceptions;
using ITSupportTicket.Services;

var service = new TicketSeverityService();
var emp = new Employee("E1", "Alice", "IT");
var ticket = new SupportTicket("T1", "Login issue", 2);
service.AddTicket(ticket);
service.AddTicket(new SupportTicket("T2", "Server down", 1));

var next = service.ProcessTicket();
Console.WriteLine(next != null ? next.ResolveTicket() : "No tickets.");

try { service.EscalateTicket("T99", 1); }
catch (TicketNotFoundException ex) { Console.WriteLine($"\nExpected: {ex.Message}"); }

Console.WriteLine("Done.");
