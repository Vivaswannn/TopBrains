using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineLearningPlatform
{
    public class EnrollmentException : Exception
    {
        public EnrollmentException(string message) : base(message) { }
    }

    public class CapacityExceededException : Exception
    {
        public CapacityExceededException(string message) : base(message) { }
    }

    public class AssignmentDeadlineException : Exception
    {
        public AssignmentDeadlineException(string message) : base(message) { }
    }

    public class Instructor
    {
        public int Id { get; }
        public string Name { get; }

        public Instructor(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id} - {Name}";
        }
    }

    public class Course : IComparable<Course>
    {
        public int Id { get; }
        public string Title { get; }
        public int Capacity { get; }
        public double Rating { get; private set; }
        public int RatingCount { get; private set; }
        public Instructor Instructor { get; }

        public Course(int id, string title, int capacity, Instructor instructor)
        {
            Id = id;
            Title = title;
            Capacity = capacity;
            Instructor = instructor;
        }

        public void AddRating(double rating)
        {
            if (rating < 0 || rating > 5)
            {
                return;
            }

            Rating = (Rating * RatingCount + rating) / (RatingCount + 1);
            RatingCount++;
        }

        public int CompareTo(Course other)
        {
            if (other == null) return 1;
            int byRating = other.Rating.CompareTo(Rating);
            if (byRating != 0) return byRating;
            return string.Compare(Title, other.Title, StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return $"{Id} - {Title} - Capacity: {Capacity} - Rating: {Rating:F1} - Instructor: {Instructor.Name}";
        }
    }

    public class Student
    {
        public int Id { get; }
        public string Name { get; }

        public Student(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id} - {Name}";
        }
    }

    public class Enrollment
    {
        public int Id { get; }
        public Student Student { get; }
        public Course Course { get; }
        public DateTime EnrolledOn { get; }

        public Enrollment(int id, Student student, Course course)
        {
            Id = id;
            Student = student;
            Course = course;
            EnrolledOn = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Id} - {Student.Name} -> {Course.Title} ({EnrolledOn:d})";
        }
    }

    public class Assignment
    {
        public int Id { get; }
        public Course Course { get; }
        public string Title { get; }
        public DateTime DueDate { get; }

        public Assignment(int id, Course course, string title, DateTime dueDate)
        {
            Id = id;
            Course = course;
            Title = title;
            DueDate = dueDate;
        }

        public void Submit(Student student, DateTime submittedOn)
        {
            if (submittedOn > DueDate)
            {
                throw new AssignmentDeadlineException($"Assignment {Title} submitted late by {student.Name}.");
            }
        }

        public override string ToString()
        {
            return $"{Id} - {Title} - Course: {Course.Title} - Due: {DueDate:g}";
        }
    }

    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T item);
    }

    public class InMemoryRepository<T> : IRepository<T>
    {
        private readonly List<T> _items = new List<T>();
        private readonly Func<T, int> _idSelector;

        public InMemoryRepository(Func<T, int> idSelector)
        {
            _idSelector = idSelector;
        }

        public IEnumerable<T> GetAll()
        {
            return _items;
        }

        public T GetById(int id)
        {
            return _items.FirstOrDefault(i => _idSelector(i) == id);
        }

        public void Add(T item)
        {
            _items.Add(item);
        }
    }

    public static class Platform
    {
        public static InMemoryRepository<Course> CoursesRepo { get; } = new InMemoryRepository<Course>(c => c.Id);
        public static InMemoryRepository<Student> StudentsRepo { get; } = new InMemoryRepository<Student>(s => s.Id);
        public static InMemoryRepository<Instructor> InstructorsRepo { get; } = new InMemoryRepository<Instructor>(i => i.Id);
        public static InMemoryRepository<Enrollment> EnrollmentsRepo { get; } = new InMemoryRepository<Enrollment>(e => e.Id);
        public static InMemoryRepository<Assignment> AssignmentsRepo { get; } = new InMemoryRepository<Assignment>(a => a.Id);

        private static int _nextEnrollmentId = 1;
        private static int _nextAssignmentId = 1;

        public static IEnumerable<Course> Courses => CoursesRepo.GetAll();
        public static IEnumerable<Student> Students => StudentsRepo.GetAll();
        public static IEnumerable<Instructor> Instructors => InstructorsRepo.GetAll();
        public static IEnumerable<Enrollment> Enrollments => EnrollmentsRepo.GetAll();
        public static IEnumerable<Assignment> Assignments => AssignmentsRepo.GetAll();

        public static void Seed()
        {
            if (Courses.Any())
            {
                return;
            }

            var i1 = new Instructor(1, "Rahul");
            var i2 = new Instructor(2, "Riya");
            var i3 = new Instructor(3, "John");

            InstructorsRepo.Add(i1);
            InstructorsRepo.Add(i2);
            InstructorsRepo.Add(i3);

            var c1 = new Course(1, "C# Basics", 100, i1);
            var c2 = new Course(2, "LINQ", 60, i1);
            var c3 = new Course(3, "ASP.NET", 80, i2);
            var c4 = new Course(4, "SQL", 120, i3);

            CoursesRepo.Add(c1);
            CoursesRepo.Add(c2);
            CoursesRepo.Add(c3);
            CoursesRepo.Add(c4);

            c1.AddRating(4.5);
            c1.AddRating(5);
            c2.AddRating(4);
            c3.AddRating(4.8);
            c4.AddRating(3.9);

            var s1 = new Student(1, "Ramesh");
            var s2 = new Student(2, "Sneha");
            var s3 = new Student(3, "Vikas");
            var s4 = new Student(4, "Rohan");
            var s5 = new Student(5, "Sita");

            StudentsRepo.Add(s1);
            StudentsRepo.Add(s2);
            StudentsRepo.Add(s3);
            StudentsRepo.Add(s4);
            StudentsRepo.Add(s5);

            EnrollStudent(1, 1);
            EnrollStudent(2, 1);
            EnrollStudent(3, 1);
            EnrollStudent(4, 1);

            EnrollStudent(1, 2);
            EnrollStudent(2, 2);
            EnrollStudent(3, 2);

            EnrollStudent(1, 3);
            EnrollStudent(2, 3);
            EnrollStudent(3, 3);
            EnrollStudent(4, 3);
            EnrollStudent(5, 3);

            EnrollStudent(1, 4);
            EnrollStudent(2, 4);
            EnrollStudent(3, 4);
            EnrollStudent(4, 4);
            EnrollStudent(5, 4);

            CreateAssignment(1, "C# Assignment", DateTime.Now.AddDays(3));
            CreateAssignment(2, "LINQ Assignment", DateTime.Now.AddDays(5));
        }

        public static Course GetCourse(int id)
        {
            var c = CoursesRepo.GetById(id);
            if (c == null)
            {
                throw new ArgumentException("Course not found.");
            }
            return c;
        }

        public static Student GetStudent(int id)
        {
            var s = StudentsRepo.GetById(id);
            if (s == null)
            {
                throw new ArgumentException("Student not found.");
            }
            return s;
        }

        public static Enrollment EnrollStudent(int studentId, int courseId)
        {
            var student = GetStudent(studentId);
            var course = GetCourse(courseId);

            bool already = Enrollments.Any(e => e.Student.Id == studentId && e.Course.Id == courseId);
            if (already)
            {
                throw new EnrollmentException("Student already enrolled.");
            }

            int count = Enrollments.Count(e => e.Course.Id == courseId);
            if (count >= course.Capacity)
            {
                throw new CapacityExceededException("Course is full.");
            }

            var enrollment = new Enrollment(_nextEnrollmentId++, student, course);
            EnrollmentsRepo.Add(enrollment);
            return enrollment;
        }

        public static Assignment CreateAssignment(int courseId, string title, DateTime due)
        {
            var course = GetCourse(courseId);
            var a = new Assignment(_nextAssignmentId++, course, title, due);
            AssignmentsRepo.Add(a);
            return a;
        }

        public static IEnumerable<Course> CoursesWithMoreThanStudents(int count)
        {
            return Enrollments
                .GroupBy(e => e.Course)
                .Where(g => g.Count() > count)
                .Select(g => g.Key);
        }

        public static IEnumerable<Student> StudentsWithMoreThanCourses(int count)
        {
            return Enrollments
                .GroupBy(e => e.Student)
                .Where(g => g.Count() > count)
                .Select(g => g.Key);
        }

        public static Course MostPopularCourse()
        {
            var item = Enrollments
                .GroupBy(e => e.Course)
                .Select(g => new { Course = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .FirstOrDefault();

            return item?.Course;
        }

        public static double AverageCourseRating()
        {
            var rated = Courses.Where(c => c.RatingCount > 0);
            if (!rated.Any())
            {
                return 0;
            }
            return rated.Average(c => c.Rating);
        }

        public static IEnumerable<(Instructor Instructor, int Count)> InstructorsByEnrollments()
        {
            var query = Enrollments
                .Join(Courses,
                    e => e.Course.Id,
                    c => c.Id,
                    (e, c) => new { e.Student, Course = c, c.Instructor })
                .GroupBy(x => x.Instructor)
                .Select(g => new { Instructor = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count);

            return query.Select(x => (x.Instructor, x.Count));
        }

        public static IEnumerable<Course> SortedCourses()
        {
            var list = Courses.ToList();
            list.Sort();
            return list;
        }
    }

    public class Program
    {
        public static void Main()
        {
            Platform.Seed();
            RunMenu();
        }

        private static void RunMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=== Online Learning Platform ===");
                Console.WriteLine("1. List courses (sorted)");
                Console.WriteLine("2. List students");
                Console.WriteLine("3. List instructors");
                Console.WriteLine("4. Enroll student in course");
                Console.WriteLine("5. Create assignment");
                Console.WriteLine("6. Submit assignment");
                Console.WriteLine("7. Courses with >50 students");
                Console.WriteLine("8. Students with >3 courses");
                Console.WriteLine("9. Most popular course");
                Console.WriteLine("10. Average course rating");
                Console.WriteLine("11. Instructors by enrollments");
                Console.WriteLine("0. Exit");
                Console.Write("Choose: ");

                var choice = Console.ReadLine();
                Console.WriteLine();

                if (choice == "0")
                {
                    break;
                }

                try
                {
                    switch (choice)
                    {
                        case "1":
                            ListCourses();
                            break;
                        case "2":
                            ListStudents();
                            break;
                        case "3":
                            ListInstructors();
                            break;
                        case "4":
                            EnrollFromUser();
                            break;
                        case "5":
                            CreateAssignmentFromUser();
                            break;
                        case "6":
                            SubmitAssignmentFromUser();
                            break;
                        case "7":
                            ShowCoursesWithManyStudents();
                            break;
                        case "8":
                            ShowStudentsWithManyCourses();
                            break;
                        case "9":
                            ShowMostPopularCourse();
                            break;
                        case "10":
                            ShowAverageRating();
                            break;
                        case "11":
                            ShowInstructorsByEnrollments();
                            break;
                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private static int ReadInt(string msg)
        {
            Console.Write(msg);
            return int.Parse(Console.ReadLine() ?? "0");
        }

        private static double ReadDouble(string msg)
        {
            Console.Write(msg);
            return double.Parse(Console.ReadLine() ?? "0");
        }

        private static DateTime ReadDateTime(string msg)
        {
            Console.Write(msg);
            return DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString("g"));
        }

        private static void ListCourses()
        {
            foreach (var c in Platform.SortedCourses())
            {
                Console.WriteLine(c);
            }
        }

        private static void ListStudents()
        {
            foreach (var s in Platform.Students)
            {
                Console.WriteLine(s);
            }
        }

        private static void ListInstructors()
        {
            foreach (var i in Platform.Instructors)
            {
                Console.WriteLine(i);
            }
        }

        private static void EnrollFromUser()
        {
            ListStudents();
            int sid = ReadInt("Student id: ");
            ListCourses();
            int cid = ReadInt("Course id: ");

            var e = Platform.EnrollStudent(sid, cid);
            Console.WriteLine("Enrolled: " + e);
        }

        private static void CreateAssignmentFromUser()
        {
            ListCourses();
            int cid = ReadInt("Course id: ");
            Console.Write("Assignment title: ");
            string title = Console.ReadLine() ?? "";
            DateTime due = ReadDateTime("Due date (e.g. 2026-02-17 23:59): ");

            var a = Platform.CreateAssignment(cid, title, due);
            Console.WriteLine("Created: " + a);
        }

        private static void SubmitAssignmentFromUser()
        {
            foreach (var a in Platform.Assignments)
            {
                Console.WriteLine(a);
            }

            int aid = ReadInt("Assignment id: ");
            int sid = ReadInt("Student id: ");
            DateTime time = ReadDateTime("Submit time: ");

            var assignment = Platform.AssignmentsRepo.GetById(aid);
            if (assignment == null)
            {
                Console.WriteLine("Assignment not found.");
                return;
            }

            var student = Platform.GetStudent(sid);
            assignment.Submit(student, time);
            Console.WriteLine("Submission accepted.");
        }

        private static void ShowCoursesWithManyStudents()
        {
            Console.WriteLine("Courses with more than 50 students:");
            foreach (var c in Platform.CoursesWithMoreThanStudents(50))
            {
                Console.WriteLine(c);
            }
        }

        private static void ShowStudentsWithManyCourses()
        {
            Console.WriteLine("Students with more than 3 courses:");
            foreach (var s in Platform.StudentsWithMoreThanCourses(3))
            {
                Console.WriteLine(s);
            }
        }

        private static void ShowMostPopularCourse()
        {
            var c = Platform.MostPopularCourse();
            if (c == null)
            {
                Console.WriteLine("No enrollments yet.");
            }
            else
            {
                Console.WriteLine("Most popular course: " + c);
            }
        }

        private static void ShowAverageRating()
        {
            Console.WriteLine("Average course rating: " + Platform.AverageCourseRating().ToString("F2"));
        }

        private static void ShowInstructorsByEnrollments()
        {
            Console.WriteLine("Instructors by enrollments:");
            foreach (var (inst, count) in Platform.InstructorsByEnrollments())
            {
                Console.WriteLine($"{inst.Name} - {count} enrollments");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineLearningPlatform
{
    public class EnrollmentException : Exception
    {
        public EnrollmentException(string message) : base(message) { }
    }

    public class CapacityExceededException : Exception
    {
        public CapacityExceededException(string message) : base(message) { }
    }

    public class AssignmentDeadlineException : Exception
    {
        public AssignmentDeadlineException(string message) : base(message) { }
    }

    public class Course : IComparable<Course>
    {
        public int Id { get; }
        public string Title { get; }
        public int Capacity { get; }
        public double Rating { get; private set; }
        public int RatingCount { get; private set; }
        public Instructor Instructor { get; }

        public Course(int id, string title, int capacity, Instructor instructor)
        {
            Id = id;
            Title = title;
            Capacity = capacity;
            Instructor = instructor;
        }

        public void AddRating(double rating)
        {
            if (rating < 0 || rating > 5) return;
            Rating = (Rating * RatingCount + rating) / (RatingCount + 1);
            RatingCount++;
        }

        public int CompareTo(Course other)
        {
            if (other == null) return 1;
            int ratingCompare = other.Rating.CompareTo(Rating);
            if (ratingCompare != 0) return ratingCompare;
            return string.Compare(Title, other.Title, StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return $"{Id} - {Title} - Capacity: {Capacity} - Rating: {Rating:F1} - Instructor: {Instructor.Name}";
        }
    }

    public class Student
    {
        public int Id { get; }
        public string Name { get; }

        public Student(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id} - {Name}";
        }
    }

    public class Instructor
    {
        public int Id { get; }
        public string Name { get; }

        public Instructor(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id} - {Name}";
        }
    }

    public class Enrollment
    {
        public int Id { get; }
        public Student Student { get; }
        public Course Course { get; }
        public DateTime EnrolledOn { get; }

        public Enrollment(int id, Student student, Course course)
        {
            Id = id;
            Student = student;
            Course = course;
            EnrolledOn = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Id} - {Student.Name} -> {Course.Title} ({EnrolledOn:d})";
        }
    }

    public class Assignment
    {
        public int Id { get; }
        public Course Course { get; }
        public string Title { get; }
        public DateTime DueDate { get; }

        public Assignment(int id, Course course, string title, DateTime dueDate)
        {
            Id = id;
            Course = course;
            Title = title;
            DueDate = dueDate;
        }

        public void Submit(Student student, DateTime submittedOn)
        {
            if (submittedOn > DueDate)
                throw new AssignmentDeadlineException($"Assignment {Title} submitted after deadline by {student.Name}.");
        }

        public override string ToString()
        {
            return $"{Id} - {Title} - Course: {Course.Title} - Due: {DueDate:g}";
        }
    }

    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
    }

    public class InMemoryRepository<T> : IRepository<T>
    {
        private readonly List<T> _items = new List<T>();
        private readonly Func<T, int> _idSelector;

        public InMemoryRepository(Func<T, int> idSelector)
        {
            _idSelector = idSelector;
        }

        public IEnumerable<T> GetAll()
        {
            return _items;
        }

        public T GetById(int id)
        {
            return _items.FirstOrDefault(x => _idSelector(x) == id);
        }

        public void Add(T entity)
        {
            _items.Add(entity);
        }
    }

    public static class Platform
    {
        public static InMemoryRepository<Course> CoursesRepo { get; } = new InMemoryRepository<Course>(c => c.Id);
        public static InMemoryRepository<Student> StudentsRepo { get; } = new InMemoryRepository<Student>(s => s.Id);
        public static InMemoryRepository<Instructor> InstructorsRepo { get; } = new InMemoryRepository<Instructor>(i => i.Id);
        public static InMemoryRepository<Enrollment> EnrollmentsRepo { get; } = new InMemoryRepository<Enrollment>(e => e.Id);
        public static InMemoryRepository<Assignment> AssignmentsRepo { get; } = new InMemoryRepository<Assignment>(a => a.Id);

        private static int _nextEnrollmentId = 1;
        private static int _nextAssignmentId = 1;

        public static List<Course> Courses => CoursesRepo.GetAll().ToList();
        public static List<Student> Students => StudentsRepo.GetAll().ToList();
        public static List<Instructor> Instructors => InstructorsRepo.GetAll().ToList();
        public static List<Enrollment> Enrollments => EnrollmentsRepo.GetAll().ToList();

        public static void Seed()
        {
            if (Courses.Any()) return;

            var i1 = new Instructor(1, "Rahul");
            var i2 = new Instructor(2, "Riya");
            var i3 = new Instructor(3, "John");

            InstructorsRepo.Add(i1);
            InstructorsRepo.Add(i2);
            InstructorsRepo.Add(i3);

            var c1 = new Course(1, "C# Basics", 100, i1);
            var c2 = new Course(2, "LINQ Deep Dive", 60, i1);
            var c3 = new Course(3, "ASP.NET Core", 80, i2);
            var c4 = new Course(4, "SQL for Beginners", 120, i3);

            CoursesRepo.Add(c1);
            CoursesRepo.Add(c2);
            CoursesRepo.Add(c3);
            CoursesRepo.Add(c4);

            c1.AddRating(4.5);
            c1.AddRating(5);
            c2.AddRating(4);
            c3.AddRating(4.8);
            c4.AddRating(3.9);

            var s1 = new Student(1, "Ramesh");
            var s2 = new Student(2, "Sneha");
            var s3 = new Student(3, "Vikas");
            var s4 = new Student(4, "Rohan");
            var s5 = new Student(5, "Sita");

            StudentsRepo.Add(s1);
            StudentsRepo.Add(s2);
            StudentsRepo.Add(s3);
            StudentsRepo.Add(s4);
            StudentsRepo.Add(s5);

            EnrollStudentInCourse(1, 1);
            EnrollStudentInCourse(2, 1);
            EnrollStudentInCourse(3, 1);
            EnrollStudentInCourse(4, 1);
            EnrollStudentInCourse(1, 2);
            EnrollStudentInCourse(2, 2);
            EnrollStudentInCourse(3, 2);
            EnrollStudentInCourse(1, 3);
            EnrollStudentInCourse(2, 3);
            EnrollStudentInCourse(3, 3);
            EnrollStudentInCourse(4, 3);
            EnrollStudentInCourse(5, 3);
            EnrollStudentInCourse(1, 4);
            EnrollStudentInCourse(2, 4);
            EnrollStudentInCourse(3, 4);
            EnrollStudentInCourse(4, 4);
            EnrollStudentInCourse(5, 4);

            CreateAssignment(1, "C# Basics Assignment 1", DateTime.Now.AddDays(3));
            CreateAssignment(2, "LINQ Assignment", DateTime.Now.AddDays(5));
            CreateAssignment(3, "ASP.NET Project", DateTime.Now.AddDays(10));
        }

        public static Course GetCourse(int id)
        {
            var c = CoursesRepo.GetById(id);
            if (c == null) throw new ArgumentException("Course not found.");
            return c;
        }

        public static Student GetStudent(int id)
        {
            var s = StudentsRepo.GetById(id);
            if (s == null) throw new ArgumentException("Student not found.");
            return s;
        }

        public static Instructor GetInstructor(int id)
        {
            var i = InstructorsRepo.GetById(id);
            if (i == null) throw new ArgumentException("Instructor not found.");
            return i;
        }

        public static Enrollment EnrollStudentInCourse(int studentId, int courseId)
        {
            var student = GetStudent(studentId);
            var course = GetCourse(courseId);

            bool alreadyEnrolled = Enrollments.Any(e => e.Student.Id == studentId && e.Course.Id == courseId);
            if (alreadyEnrolled)
                throw new EnrollmentException("Student already enrolled in this course.");

            int currentCount = Enrollments.Count(e => e.Course.Id == courseId);
            if (currentCount >= course.Capacity)
                throw new CapacityExceededException("Course capacity reached.");

            var enrollment = new Enrollment(_nextEnrollmentId++, student, course);
            EnrollmentsRepo.Add(enrollment);
            return enrollment;
        }

        public static Assignment CreateAssignment(int courseId, string title, DateTime dueDate)
        {
            var course = GetCourse(courseId);
            var assignment = new Assignment(_nextAssignmentId++, course, title, dueDate);
            AssignmentsRepo.Add(assignment);
            return assignment;
        }

        public static IEnumerable<Course> CoursesWithMoreThanStudents(int count)
        {
            var query = Enrollments
                .GroupBy(e => e.Course)
                .Where(g => g.Count() > count)
                .Select(g => g.Key);
            return query;
        }

        public static IEnumerable<Student> StudentsWithMoreThanCourses(int count)
        {
            var query = Enrollments
                .GroupBy(e => e.Student)
                .Where(g => g.Count() > count)
                .Select(g => g.Key);
            return query;
        }

        public static Course MostPopularCourse()
        {
            var query = Enrollments
                .GroupBy(e => e.Course)
                .Select(g => new { Course = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .FirstOrDefault();
            return query?.Course;
        }

        public static double AverageCourseRating()
        {
            var rated = Courses.Where(c => c.RatingCount > 0);
            if (!rated.Any()) return 0;
            return rated.Average(c => c.Rating);
        }

        public static IEnumerable<(Instructor Instructor, int EnrollmentCount)> InstructorsByEnrollments()
        {
            var query = Enrollments
                .Join(Courses,
                    e => e.Course.Id,
                    c => c.Id,
                    (e, c) => new { e, c.Instructor })
                .GroupBy(x => x.Instructor)
                .Select(g => new { Instructor = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count);

            return query.Select(x => (x.Instructor, x.Count));
        }

        public static IEnumerable<Course> CoursesSorted()
        {
            var list = new List<Course>(Courses);
            list.Sort();
            return list;
        }
    }

    public class Program
    {
        public static void Main()
        {
            Platform.Seed();
            RunMenu();
        }

        private static void RunMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=== Online Learning Platform ===");
                Console.WriteLine("1. List courses (custom sorted)");
                Console.WriteLine("2. List students");
                Console.WriteLine("3. List instructors");
                Console.WriteLine("4. Enroll student in course");
                Console.WriteLine("5. Create assignment");
                Console.WriteLine("6. Submit assignment");
                Console.WriteLine("7. Courses with >50 students");
                Console.WriteLine("8. Students with >3 courses");
                Console.WriteLine("9. Most popular course");
                Console.WriteLine("10. Average course rating");
                Console.WriteLine("11. Instructors by enrollments");
                Console.WriteLine("0. Exit");
                Console.Write("Choose: ");

                var choice = Console.ReadLine();
                Console.WriteLine();

                if (choice == "0") break;

                try
                {
                    switch (choice)
                    {
                        case "1": ListCourses(); break;
                        case "2": ListStudents(); break;
                        case "3": ListInstructors(); break;
                        case "4": EnrollFromUser(); break;
                        case "5": CreateAssignmentFromUser(); break;
                        case "6": SubmitAssignmentFromUser(); break;
                        case "7": ShowCoursesWithManyStudents(); break;
                        case "8": ShowStudentsWithManyCourses(); break;
                        case "9": ShowMostPopularCourse(); break;
                        case "10": ShowAverageRating(); break;
                        case "11": ShowInstructorsByEnrollments(); break;
                        default: Console.WriteLine("Invalid option."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private static int ReadInt(string msg)
        {
            Console.Write(msg);
            return int.Parse(Console.ReadLine() ?? "0");
        }

        private static double ReadDouble(string msg)
        {
            Console.Write(msg);
            return double.Parse(Console.ReadLine() ?? "0");
        }

        private static DateTime ReadDateTime(string msg)
        {
            Console.Write(msg);
            return DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString("g"));
        }

        private static void ListCourses()
        {
            foreach (var c in Platform.CoursesSorted())
                Console.WriteLine(c);
        }

        private static void ListStudents()
        {
            foreach (var s in Platform.Students)
                Console.WriteLine(s);
        }

        private static void ListInstructors()
        {
            foreach (var i in Platform.Instructors)
                Console.WriteLine(i);
        }

        private static void EnrollFromUser()
        {
            ListStudents();
            int sid = ReadInt("Student id: ");
            ListCourses();
            int cid = ReadInt("Course id: ");
            var enrollment = Platform.EnrollStudentInCourse(sid, cid);
            Console.WriteLine("Enrolled: " + enrollment);
        }

        private static void CreateAssignmentFromUser()
        {
            ListCourses();
            int cid = ReadInt("Course id: ");
            Console.Write("Assignment title: ");
            string title = Console.ReadLine() ?? "";
            DateTime due = ReadDateTime("Due date (e.g. 2026-02-17 23:59): ");
            var a = Platform.CreateAssignment(cid, title, due);
            Console.WriteLine("Created: " + a);
        }

        private static void SubmitAssignmentFromUser()
        {
            foreach (var a in Platform.AssignmentsRepo.GetAll())
                Console.WriteLine(a);
            int aid = ReadInt("Assignment id: ");
            int sid = ReadInt("Student id: ");
            DateTime time = ReadDateTime("Submit time: ");

            var assignment = Platform.AssignmentsRepo.GetById(aid);
            if (assignment == null)
            {
                Console.WriteLine("Assignment not found.");
                return;
            }

            var student = Platform.GetStudent(sid);
            assignment.Submit(student, time);
            Console.WriteLine("Submission accepted.");
        }

        private static void ShowCoursesWithManyStudents()
        {
            Console.WriteLine("Courses with more than 50 students:");
            foreach (var c in Platform.CoursesWithMoreThanStudents(50))
                Console.WriteLine(c);
        }

        private static void ShowStudentsWithManyCourses()
        {
            Console.WriteLine("Students enrolled in more than 3 courses:");
            foreach (var s in Platform.StudentsWithMoreThanCourses(3))
                Console.WriteLine(s);
        }

        private static void ShowMostPopularCourse()
        {
            var c = Platform.MostPopularCourse();
            if (c == null) Console.WriteLine("No enrollments yet.");
            else Console.WriteLine("Most popular: " + c);
        }

        private static void ShowAverageRating()
        {
            Console.WriteLine("Average course rating: " + Platform.AverageCourseRating().ToString("F2"));
        }

        private static void ShowInstructorsByEnrollments()
        {
            Console.WriteLine("Instructors by enrollment count:");
            foreach (var (inst, count) in Platform.InstructorsByEnrollments())
                Console.WriteLine($"{inst.Name} - {count} enrollments");
        }
    }
}
