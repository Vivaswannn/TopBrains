using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Course_Registration_System
{
    // =========================
    // University System Class
    // =========================
    public class UniversitySystem
    {
        public Dictionary<string, Course> AvailableCourses { get; private set; }
        public Dictionary<string, Student> Students { get; private set; }

        public UniversitySystem()
        {
            AvailableCourses = new Dictionary<string, Course>();
            Students = new Dictionary<string, Student>();
        }

        public void AddCourse(string code, string name, int credits, int maxCapacity = 50, List<string> prerequisites = null)
        {
            // TODO:
            // 1. Throw ArgumentException if course code exists
            // 2. Create Course object
            // 3. Add to AvailableCourses
            if(courseCodeExists(code))
                throw new ArgumentException("Course code already exists.");

            Course newCourse = new Course(code, name, credits, maxCapacity, prerequisites);
            AvailableCourses.Add(code, newCourse);
        }

        public void AddStudent(string id, string name, string major, int maxCredits = 18, List<string> completedCourses = null)
        {
            // TODO:
            // 1. Throw ArgumentException if student ID exists
            // 2. Create Student object
            // 3. Add to Students dictionary
            if(studentIdExists(id))
                throw new ArgumentException("Student ID already exists.");

            Student newStudent = new Student(id, name, major, maxCredits, completedCourses);
            Students.Add(id, newStudent);

        }

        public bool RegisterStudentForCourse(string studentId, string courseCode)
        {
            // TODO:
            // 1. Validate student and course existence
            // 2. Call student.AddCourse(course)
            // 3. Display meaningful messages
            if (!Students.ContainsKey(studentId))
            {
                Console.WriteLine("Student not found.");
                return false;
            }

            if (!AvailableCourses.ContainsKey(courseCode))
            {
                Console.WriteLine("Course not found.");
                return false;
            }

            Student student = Students[studentId];
            Course course = AvailableCourses[courseCode];

            bool result = student.AddCourse(course);

            if (!result)
            {
                Console.WriteLine("Could not register student for course. Check prerequisites, credits, or capacity.");
            }

            return result;
        }

        public bool DropStudentFromCourse(string studentId, string courseCode)
        {
            // TODO:
            // 1. Validate student existence
            // 2. Call student.DropCourse(courseCode)
            if (!Students.ContainsKey(studentId))
            {
                Console.WriteLine("Student not found.");
                return false;
            }

            Student student = Students[studentId];
            bool result = student.DropCourse(courseCode);

            if (!result)
            {
                Console.WriteLine("Student is not registered in the specified course.");
            }

            return result;
        }

        public void DisplayAllCourses()
        {
            // TODO:
            // Display course code, name, credits, enrollment info
            if (AvailableCourses.Count == 0)
            {
                Console.WriteLine("No courses available.");
                return;
            }

            Console.WriteLine("Available Courses:");
            foreach (var course in AvailableCourses.Values)
            {
                Console.WriteLine($"{course.CourseCode} - {course.CourseName} ({course.Credits} credits), Enrollment: {course.GetEnrollmentInfo()}");
            }
        }

        public void DisplayStudentSchedule(string studentId)
        {
            // TODO:
            // Validate student existence
            // Call student.DisplaySchedule()
            if (!Students.ContainsKey(studentId))
            {
                Console.WriteLine("Student not found.");
                return;
            }

            Student student = Students[studentId];
            student.DisplaySchedule();
        }

        public void DisplaySystemSummary()
        {
            // TODO:
            // Display total students, total courses, average enrollment
            int totalStudents = Students.Count;
            int totalCourses = AvailableCourses.Count;

            double averageEnrollment = 0.0;
            if (totalCourses > 0)
            {
                int totalEnrollment = 0;
                foreach (var course in AvailableCourses.Values)
                {
                    string[] parts = course.GetEnrollmentInfo().Split('/');
                    int current;
                    if (parts.Length > 0 && int.TryParse(parts[0], out current))
                    {
                        totalEnrollment += current;
                    }
                }

                averageEnrollment = (double)totalEnrollment / totalCourses;
            }

            Console.WriteLine("System Summary:");
            Console.WriteLine($"- Total Students: {totalStudents}");
            Console.WriteLine($"- Total Courses: {totalCourses}");
            Console.WriteLine($"- Average Enrollment: {averageEnrollment:F1}");
        }

        // Simple helper methods to check existence
        private bool courseCodeExists(string code)
        {
            return AvailableCourses.ContainsKey(code);
        }

        private bool studentIdExists(string id)
        {
            return Students.ContainsKey(id);
        }
    }
}
