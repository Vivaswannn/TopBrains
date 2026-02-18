namespace BankingTransactionRisk.Domain;

/// <summary>Strategy: risk based on amount tiers.</summary>
public class DefaultRiskCalculator : IRiskCalculator
{
    public int CalculateRisk(Transaction t)
    {
        if (t.Amount <= 100) return 1;
        if (t.Amount <= 1000) return 2;
        if (t.Amount <= 10000) return 3;
        if (t.Amount <= 50000) return 4;
        return 5;
    }
}
