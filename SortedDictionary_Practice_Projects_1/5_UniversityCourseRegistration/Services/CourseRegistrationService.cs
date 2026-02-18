using System.Linq;
using UniversityCourseRegistration.Domain;
using UniversityCourseRegistration.Exceptions;

namespace UniversityCourseRegistration.Services;

/// <summary>SortedDictionary&lt;double, List&lt;Student&gt;&gt; — key = GPA (higher first with desc comparer).</summary>
public class CourseRegistrationService
{
    private readonly SortedDictionary<double, List<Student>> _byGpa = new(Comparer<double>.Create((a, b) => b.CompareTo(a)));
    private readonly Dictionary<string, HashSet<string>> _enrollments = new(); // courseId -> studentIds
    private readonly int _courseCapacity;

    public CourseRegistrationService(int courseCapacity = 30)
    {
        _courseCapacity = courseCapacity;
    }

    /// <summary>Encapsulated enrollment logic: validate GPA, capacity, duplicate.</summary>
    public void RegisterStudent(Student student, string courseId)
    {
        if (student == null) throw new ArgumentNullException(nameof(student));
        if (student.Gpa < 0 || student.Gpa > 4.0) throw new InvalidGPAException(student.Gpa);

        if (!_enrollments.ContainsKey(courseId)) _enrollments[courseId] = new HashSet<string>();
        var enrolled = _enrollments[courseId];
        if (enrolled.Contains(student.Id))
            throw new DuplicateEnrollmentException(student.Id, courseId);
        if (enrolled.Count >= _courseCapacity)
            throw new CourseFullException(courseId, _courseCapacity);

        enrolled.Add(student.Id);
        if (!_byGpa.ContainsKey(student.Gpa)) _byGpa[student.Gpa] = new List<Student>();
        if (!_byGpa[student.Gpa].Any(s => s.Id == student.Id))
            _byGpa[student.Gpa].Add(student);
    }

    public bool AllocateSeat(string studentId, string courseId)
    {
        return _enrollments.TryGetValue(courseId, out var set) && set.Contains(studentId);
    }

    public IReadOnlyDictionary<double, List<Student>> GetStudentsByGpa() => _byGpa;
}
