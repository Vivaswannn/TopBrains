using HospitalEmergency.Domain;
using HospitalEmergency.Exceptions;
using HospitalEmergency.Services;

var service = new EmergencyQueueService();

var p1 = new Patient("P001", "Alice", 1, "Cardiac");
var p2 = new Patient("P002", "Bob", 2, "Fracture");
service.AddPatient(p1);
service.AddPatient(p2);

Console.WriteLine("Next patient (highest severity):");
var next = service.GetNextPatient();
if (next != null) Console.WriteLine(next.Treat());

try { new Patient("X", "Y", 10, "Z"); }
catch (InvalidSeverityException ex) { Console.WriteLine($"\nExpected: {ex.Message}"); }

Console.WriteLine("Done.");
