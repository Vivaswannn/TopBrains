using TrafficViolation.Domain;
using TrafficViolation.Exceptions;
using TrafficViolation.Services;

var service = new ViolationMonitoringService();
var car = new Car("ABC-1234");
var truck = new Truck("XYZ-9999");

service.AddViolation(new Violation("V1", car, 150m, "Speeding"));
service.AddViolation(new Violation("V2", car, 200m, "Red light"));
service.AddViolation(new Violation("V3", truck, 75m, "Parking"));

Console.WriteLine("Repeat offenders (2+):");
foreach (var plate in service.GetRepeatOffenders(2))
    Console.WriteLine($"  {plate}");

try { service.AddViolation(new Violation("V4", null!, 50m, "Test")); }
catch (InvalidVehicleException) { Console.WriteLine("\nExpected: Invalid vehicle."); }

Console.WriteLine("Done.");
