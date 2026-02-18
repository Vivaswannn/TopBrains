namespace UniversityCourseRegistration.Exceptions;

public class InvalidGPAException : RegistrationException
{
    public double Gpa { get; }
    public InvalidGPAException(double gpa) : base($"Invalid GPA: {gpa}. Must be 0.0–4.0.") => Gpa = gpa;
}
