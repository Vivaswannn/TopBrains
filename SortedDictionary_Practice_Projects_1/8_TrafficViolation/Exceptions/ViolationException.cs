namespace TrafficViolation.Exceptions;

public abstract class ViolationException : Exception
{
    protected ViolationException(string message) : base(message) { }
    protected ViolationException(string message, Exception inner) : base(message, inner) { }
}
