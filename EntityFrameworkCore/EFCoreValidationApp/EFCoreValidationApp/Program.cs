using CoreValidation.Models.Context;
using EFCoreValidationApp.Models;
using EntityValidation.Models;
using System.ComponentModel.DataAnnotations;        

namespace EFCoreValidationApp
{
    public class Program
    {
        static void Main(string[] args)
        { 
            using var context = new AppDbContext();

            context.Database.EnsureCreated();

            var course = new Course
            {
                CourseName = "Computer Science"
            };

            var student = new Student
            {
                Name = "John Doe",
                Email = "viv@cap.com",
                Age = 25,
                EnrollmentDate = DateTime.Now.AddDays(1), // Future date for validation
                CourseId = 1
            };

        static void ValidateAndSave(AppDbContext context, Student student)
        {
            var validateionResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(student);

            bool isValid = Validator.TryValidateObject(student, validationContext, validateionResults, true);

            if (!isValid)
            {
                Console.WriteLine("Validation Errors:");
                foreach (var error in validateionResults)
                {
                    Console.WriteLine($"- {error.ErrorMessage}");
                }
                return;
            }

            try
            {
                context.Students.Add(student);
                context.SaveChanges();
                Console.WriteLine("Student saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database Error:");
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);

            }
        }
    }
}