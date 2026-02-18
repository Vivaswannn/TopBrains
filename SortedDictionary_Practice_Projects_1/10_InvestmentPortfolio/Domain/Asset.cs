namespace InvestmentPortfolio.Domain;

public abstract class Asset
{
    public string Id { get; }
    public string Name { get; }
    public int RiskRating { get; } // 1–5
    public decimal Value { get; }

    protected Asset(string id, string name, int riskRating, decimal value)
    {
        if (riskRating < 1 || riskRating > 5) throw new Exceptions.InvalidRiskRatingException(riskRating);
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        RiskRating = riskRating;
        Value = value >= 0 ? value : 0;
    }

    /// <summary>Polymorphic return calculation.</summary>
    public abstract decimal CalculateReturn(decimal ratePercent);
}
