namespace TrafficViolation.Exceptions;

public class PenaltyExceedsLimitException : ViolationException
{
    public decimal Penalty { get; }
    public decimal Limit { get; }
    public PenaltyExceedsLimitException(decimal penalty, decimal limit)
        : base($"Penalty {penalty} exceeds maximum limit {limit}.") { Penalty = penalty; Limit = limit; }
}
