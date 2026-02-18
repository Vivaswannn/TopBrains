namespace UniversityCourseRegistration.Exceptions;

public abstract class RegistrationException : Exception
{
    protected RegistrationException(string message) : base(message) { }
    protected RegistrationException(string message, Exception inner) : base(message, inner) { }
}
