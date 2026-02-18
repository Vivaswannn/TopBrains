namespace UniversityCourseRegistration.Exceptions;

public class DuplicateEnrollmentException : RegistrationException
{
    public string StudentId { get; }
    public string CourseId { get; }
    public DuplicateEnrollmentException(string studentId, string courseId)
        : base($"Student {studentId} is already enrolled in course {courseId}.") { StudentId = studentId; CourseId = courseId; }
}
