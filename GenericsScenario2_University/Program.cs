using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericsScenario2_University
{
    public interface IStudent
    {
        int StudentId { get; }
        string Name { get; }
        int Semester { get; }
    }

    public interface ICourse
    {
        string CourseCode { get; }
        string Title { get; }
        int MaxCapacity { get; }
        int Credits { get; }
    }

    public class EnrollmentSystem<TStudent, TCourse>
        where TStudent : IStudent
        where TCourse : ICourse
    {
        private readonly Dictionary<TCourse, List<TStudent>> _enrollments = new();

        public bool EnrollStudent(TStudent student, TCourse course)
        {
            if (student == null || course == null)
                return false;
            if (!_enrollments.ContainsKey(course))
                _enrollments[course] = new List<TStudent>();
            var list = _enrollments[course];
            if (list.Count >= course.MaxCapacity)
                return false;
            if (list.Any(s => s.StudentId == student.StudentId))
                return false;
            if (course is LabCourse lc && student.Semester < lc.RequiredSemester)
                return false;
            list.Add(student);
            return true;
        }

        public IReadOnlyList<TStudent> GetEnrolledStudents(TCourse course)
        {
            if (course == null || !_enrollments.ContainsKey(course))
                return new List<TStudent>();
            return _enrollments[course].AsReadOnly();
        }

        public IEnumerable<TCourse> GetStudentCourses(TStudent student)
        {
            if (student == null) yield break;
            foreach (var kv in _enrollments)
                if (kv.Value.Any(s => s.StudentId == student.StudentId))
                    yield return kv.Key;
        }

        public int CalculateStudentWorkload(TStudent student)
        {
            return GetStudentCourses(student).Sum(c => c.Credits);
        }

        public bool IsEnrolled(TStudent student, TCourse course)
        {
            return _enrollments.ContainsKey(course) && _enrollments[course].Any(s => s.StudentId == student.StudentId);
        }
    }

    public class EngineeringStudent : IStudent
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int Semester { get; set; }
        public string Specialization { get; set; }
    }

    public class LabCourse : ICourse
    {
        public string CourseCode { get; set; }
        public string Title { get; set; }
        public int MaxCapacity { get; set; }
        public int Credits { get; set; }
        public string LabEquipment { get; set; }
        public int RequiredSemester { get; set; }
    }

    public class GradeBook<TStudent, TCourse>
        where TStudent : IStudent
        where TCourse : ICourse
    {
        private readonly Dictionary<(int studentId, string courseCode), double> _grades = new();
        private readonly EnrollmentSystem<TStudent, TCourse> _enrollment;

        public GradeBook(EnrollmentSystem<TStudent, TCourse> enrollment)
        {
            _enrollment = enrollment;
        }

        public void AddGrade(TStudent student, TCourse course, double grade)
        {
            if (grade < 0 || grade > 100)
                throw new ArgumentOutOfRangeException(nameof(grade), "Grade must be between 0 and 100.");
            if (!_enrollment.IsEnrolled(student, course))
                throw new InvalidOperationException("Student must be enrolled in course to add grade.");
            _grades[(student.StudentId, course.CourseCode)] = grade;
        }

        public double? CalculateGPA(TStudent student)
        {
            var courses = _enrollment.GetStudentCourses(student).ToList();
            if (courses.Count == 0) return null;
            double sum = 0;
            int totalCredits = 0;
            foreach (var c in courses)
            {
                var key = (student.StudentId, c.CourseCode);
                if (!_grades.TryGetValue(key, out var g)) continue;
                sum += g * c.Credits;
                totalCredits += c.Credits;
            }
            return totalCredits == 0 ? null : (double?)(sum / totalCredits);
        }

        public (TStudent student, double grade)? GetTopStudent(TCourse course)
        {
            var students = _enrollment.GetEnrolledStudents(course);
            TStudent topStudent = default;
            double topGrade = -1;
            foreach (var s in students)
            {
                if (_grades.TryGetValue((s.StudentId, course.CourseCode), out var g) && g > topGrade)
                {
                    topGrade = g;
                    topStudent = s;
                }
            }
            return topStudent != null ? (topStudent, topGrade) : null;
        }
    }

    class Program
    {
        static void Main()
        {
            var enrollment = new EnrollmentSystem<EngineeringStudent, LabCourse>();
            var gradeBook = new GradeBook<EngineeringStudent, LabCourse>(enrollment);

            var s1 = new EngineeringStudent { StudentId = 1, Name = "Alice", Semester = 3, Specialization = "CS" };
            var s2 = new EngineeringStudent { StudentId = 2, Name = "Bob", Semester = 2, Specialization = "EE" };
            var s3 = new EngineeringStudent { StudentId = 3, Name = "Carol", Semester = 4, Specialization = "CS" };

            var c1 = new LabCourse { CourseCode = "CS101", Title = "Programming", MaxCapacity = 2, Credits = 4, RequiredSemester = 2 };
            var c2 = new LabCourse { CourseCode = "EE201", Title = "Circuits", MaxCapacity = 3, Credits = 3, RequiredSemester = 3 };

            Console.WriteLine("Enroll Alice in CS101: " + enrollment.EnrollStudent(s1, c1));
            Console.WriteLine("Enroll Bob in CS101: " + enrollment.EnrollStudent(s2, c1));
            Console.WriteLine("Enroll Carol in CS101 (capacity 2): " + enrollment.EnrollStudent(s3, c1));
            Console.WriteLine("Enroll Bob in EE201 (semester 2 < required 3): " + enrollment.EnrollStudent(s2, c2));
            Console.WriteLine("Enroll Alice in EE201: " + enrollment.EnrollStudent(s1, c2));
            Console.WriteLine("Enroll Carol in EE201: " + enrollment.EnrollStudent(s3, c2));

            gradeBook.AddGrade(s1, c1, 85);
            gradeBook.AddGrade(s2, c1, 92);
            gradeBook.AddGrade(s1, c2, 78);
            gradeBook.AddGrade(s3, c2, 88);

            Console.WriteLine("Alice GPA: " + gradeBook.CalculateGPA(s1)?.ToString("F2"));
            Console.WriteLine("Bob GPA: " + gradeBook.CalculateGPA(s2)?.ToString("F2"));
            var top = gradeBook.GetTopStudent(c1);
            if (top.HasValue) Console.WriteLine($"Top in CS101: {top.Value.student.Name} - {top.Value.grade}");
        }
    }
}
