namespace TrafficViolation.Domain;

public class Violation
{
    public string Id { get; }
    public Vehicle Vehicle { get; }
    public decimal PenaltyAmount { get; }
    public string Description { get; }
    public DateTime Date { get; }

    public Violation(string id, Vehicle vehicle, decimal penaltyAmount, string description, DateTime? date = null)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Vehicle = vehicle ?? throw new ArgumentNullException(nameof(vehicle));
        PenaltyAmount = penaltyAmount;
        Description = description ?? "";
        Date = date ?? DateTime.UtcNow;
    }
}
