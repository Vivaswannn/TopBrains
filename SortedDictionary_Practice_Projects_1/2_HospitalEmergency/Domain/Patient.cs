namespace HospitalEmergency.Domain;

public class Patient : Person
{
    public int Severity { get; } // 1 = Critical
    public string Condition { get; }

    public Patient(string id, string name, int severity, string condition)
        : base(id, name)
    {
        if (severity < 1 || severity > 5)
            throw new Exceptions.InvalidSeverityException(severity);
        Severity = severity;
        Condition = condition ?? "";
    }

    /// <summary>Polymorphic Treat method.</summary>
    public virtual string Treat() => $"Treating patient {Name} (Severity {Severity}): {Condition}";
}
