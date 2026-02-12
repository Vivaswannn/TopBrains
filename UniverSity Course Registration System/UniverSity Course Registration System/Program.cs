using System;
using System.Collections.Generic;
using System.Linq;

namespace University_Course_Registration_System
{
     // =========================
    // Program (Menu-Driven)
    // =========================
    class Program
    {
        static void Main()
        {
            UniversitySystem system = new UniversitySystem();
            bool exit = false;

            Console.WriteLine("Welcome to University Course Registration System");

            while (!exit)
            {
                Console.WriteLine("\n1. Add Course");
                Console.WriteLine("2. Add Student");
                Console.WriteLine("3. Register Student for Course");
                Console.WriteLine("4. Drop Student from Course");
                Console.WriteLine("5. Display All Courses");
                Console.WriteLine("6. Display Student Schedule");
                Console.WriteLine("7. Display System Summary");
                Console.WriteLine("8. Exit");

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                try
                {
                    // TODO:
                    // Implement menu handling logic using switch-case
                    // Prompt user inputs
                    // Call appropriate UniversitySystem methods

                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine("Enter Course Code:");
                            string code = Console.ReadLine();

                            Console.WriteLine("Enter Course Name:");
                            string name = Console.ReadLine();

                            Console.WriteLine("Enter Credits:");
                            int credits = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Enter Max Capacity (default 50):");
                            string capacityInput = Console.ReadLine();
                            int maxCapacity = string.IsNullOrWhiteSpace(capacityInput)
                                ? 50
                                : Convert.ToInt32(capacityInput);

                            Console.WriteLine("Enter Prerequisites (comma-separated, or Enter for none):");
                            string prereqInput = Console.ReadLine();
                            List<string> prerequisites = string.IsNullOrWhiteSpace(prereqInput)
                                ? null
                                : prereqInput.Split(',').ToList();

                            system.AddCourse(code, name, credits, maxCapacity, prerequisites);
                            Console.WriteLine("Course added successfully.");
                            break;

                        case "2":
                            Console.WriteLine("Enter Student ID:");
                            string studentId = Console.ReadLine();

                            Console.WriteLine("Enter Name:");
                            string studentName = Console.ReadLine();

                            Console.WriteLine("Enter Major:");
                            string major = Console.ReadLine();

                            Console.WriteLine("Enter Max Credits (default 18):");
                            string maxCreditsInput = Console.ReadLine();
                            int maxCredits = string.IsNullOrWhiteSpace(maxCreditsInput)
                                ? 18
                                : Convert.ToInt32(maxCreditsInput);

                            Console.WriteLine("Enter Completed Courses (comma-separated, or Enter for none):");
                            string completedInput = Console.ReadLine();
                            List<string> completedCourses = string.IsNullOrWhiteSpace(completedInput)
                                ? null
                                : completedInput.Split(',').ToList();

                            system.AddStudent(studentId, studentName, major, maxCredits, completedCourses);
                            Console.WriteLine("Student added successfully.");
                            break;

                        case "3":
                            Console.WriteLine("Enter Student ID:");
                            string regStudentId = Console.ReadLine();

                            Console.WriteLine("Enter Course Code:");
                            string regCourseCode = Console.ReadLine();

                            if (system.RegisterStudentForCourse(regStudentId, regCourseCode))
                            {
                                Console.WriteLine("Registration successful.");
                            }
                            else
                            {
                                Console.WriteLine("Registration failed.");
                            }
                            break;

                        case "4":
                            Console.WriteLine("Enter Student ID:");
                            string dropStudentId = Console.ReadLine();

                            Console.WriteLine("Enter Course Code:");
                            string dropCourseCode = Console.ReadLine();

                            if (system.DropStudentFromCourse(dropStudentId, dropCourseCode))
                            {
                                Console.WriteLine("Course dropped successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Drop failed.");
                            }
                            break;

                        case "5":
                            system.DisplayAllCourses();
                            break;

                        case "6":
                            Console.WriteLine("Enter Student ID:");
                            string scheduleId = Console.ReadLine();
                            system.DisplayStudentSchedule(scheduleId);
                            break;

                        case "7":
                            system.DisplaySystemSummary();
                            break;

                        case "8":
                            exit = true;
                            Console.WriteLine("Exiting...");
                            break;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}

