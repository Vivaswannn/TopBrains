using System.Linq;
using TrafficViolation.Domain;
using TrafficViolation.Exceptions;

namespace TrafficViolation.Services;

/// <summary>SortedDictionary&lt;decimal, List&lt;Violation&gt;&gt; — key = penalty amount.</summary>
public class ViolationMonitoringService
{
    private readonly SortedDictionary<decimal, List<Violation>> _byPenalty = new();
    private readonly Dictionary<string, int> _offenderCount = new();
    private const decimal MaxPenaltyLimit = 10_000m;

    /// <summary>Encapsulate violation logic: validate vehicle and penalty limit.</summary>
    public void AddViolation(Violation violation)
    {
        if (violation == null) throw new ArgumentNullException(nameof(violation));
        if (string.IsNullOrWhiteSpace(violation.Vehicle?.PlateNumber))
            throw new InvalidVehicleException(violation.Vehicle?.PlateNumber ?? "");
        if (violation.PenaltyAmount > MaxPenaltyLimit)
            throw new PenaltyExceedsLimitException(violation.PenaltyAmount, MaxPenaltyLimit);

        if (!_byPenalty.ContainsKey(violation.PenaltyAmount))
            _byPenalty[violation.PenaltyAmount] = new List<Violation>();
        _byPenalty[violation.PenaltyAmount].Add(violation);

        var key = violation.Vehicle.PlateNumber;
        _offenderCount[key] = _offenderCount.GetValueOrDefault(key) + 1;
    }

    /// <summary>Escalate repeat offenders (e.g. return list of plates with multiple violations).</summary>
    public IReadOnlyList<string> GetRepeatOffenders(int minViolations = 2)
    {
        return _offenderCount.Where(kv => kv.Value >= minViolations).Select(kv => kv.Key).ToList();
    }

    public IReadOnlyDictionary<decimal, List<Violation>> GetViolationsByPenalty() => _byPenalty;
}
