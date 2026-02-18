namespace HospitalEmergency.Exceptions;

public class QueueOverflowException : EmergencyException
{
    public int Severity { get; }
    public int MaxCapacity { get; }
    public QueueOverflowException(int severity, int maxCapacity)
        : base($"Queue for severity {severity} is full (max {maxCapacity}).") { Severity = severity; MaxCapacity = maxCapacity; }
}
