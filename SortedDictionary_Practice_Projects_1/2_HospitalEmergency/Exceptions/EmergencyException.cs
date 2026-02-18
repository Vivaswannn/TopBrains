namespace HospitalEmergency.Exceptions;

public abstract class EmergencyException : Exception
{
    protected EmergencyException(string message) : base(message) { }
    protected EmergencyException(string message, Exception inner) : base(message, inner) { }
}
