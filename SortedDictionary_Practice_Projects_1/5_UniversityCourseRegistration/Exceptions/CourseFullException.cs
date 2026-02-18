namespace UniversityCourseRegistration.Exceptions;

public class CourseFullException : RegistrationException
{
    public string CourseId { get; }
    public int Capacity { get; }
    public CourseFullException(string courseId, int capacity)
        : base($"Course {courseId} is full (capacity: {capacity}).") { CourseId = courseId; Capacity = capacity; }
}
