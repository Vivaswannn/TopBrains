using UniversityCourseRegistration.Domain;
using UniversityCourseRegistration.Exceptions;
using UniversityCourseRegistration.Services;

var service = new CourseRegistrationService(courseCapacity: 3);

var s1 = new Student("S1", "Alice", 3.8);
var s2 = new Student("S2", "Bob", 3.2);
service.RegisterStudent(s1, "CS101");
service.RegisterStudent(s2, "CS101");
Console.WriteLine("Registered by GPA (highest first):");
foreach (var kv in service.GetStudentsByGpa())
    foreach (var s in kv.Value)
        Console.WriteLine($"  GPA {kv.Key}: {s.Name} ({s.Id})");

try { service.RegisterStudent(s1, "CS101"); }
catch (DuplicateEnrollmentException ex) { Console.WriteLine($"\nExpected: {ex.Message}"); }

Console.WriteLine("Done.");
