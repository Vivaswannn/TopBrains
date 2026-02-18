using LibraryFine.Domain;
using LibraryFine.Exceptions;
using LibraryFine.Services;

var service = new FineService();
var student = new StudentMember("M1", "Alice");
var faculty = new FacultyMember("M2", "Bob");
service.RegisterMember(new Member(student, 0));
service.RegisterMember(new Member(faculty, 5m));

Console.WriteLine($"Student fine (10 days): {student.CalculateFine(10):C}");
Console.WriteLine($"Faculty fine (10 days): {faculty.CalculateFine(10):C}");

service.AddFine("M1", 2.50m);
service.PayFine("M2", 5m);

try { service.PayFine("M99", 1m); }
catch (FineNotFoundException ex) { Console.WriteLine($"\nExpected: {ex.Message}"); }

Console.WriteLine("Done.");
