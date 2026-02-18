namespace HospitalEmergency.Exceptions;

public class InvalidSeverityException : EmergencyException
{
    public int Severity { get; }
    public InvalidSeverityException(int severity)
        : base($"Invalid severity level: {severity}. Must be 1 (Critical) to 5 (Low).") => Severity = severity;
}
