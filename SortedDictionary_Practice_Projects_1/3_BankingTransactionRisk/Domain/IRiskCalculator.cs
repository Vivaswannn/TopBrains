namespace BankingTransactionRisk.Domain;

/// <summary>Strategy for risk calculation.</summary>
public interface IRiskCalculator
{
    int CalculateRisk(Transaction transaction);
}
