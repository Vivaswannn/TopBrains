using BankingTransactionRisk.Domain;
using BankingTransactionRisk.Exceptions;

namespace BankingTransactionRisk.Services;

/// <summary>SortedDictionary&lt;int, List&lt;Transaction&gt;&gt; — key = RiskScore, descending (high risk first).</summary>
public class RiskMonitoringService
{
    private readonly SortedDictionary<int, List<Transaction>> _byRisk = new(Comparer<int>.Create((a, b) => b.CompareTo(a)));
    private readonly IRiskCalculator _riskCalculator;
    private const decimal MaxTransactionAmount = 100_000;

    public RiskMonitoringService(IRiskCalculator? riskCalculator = null)
    {
        _riskCalculator = riskCalculator ?? new DefaultRiskCalculator();
    }

    public void AddTransaction(Transaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction.Amount < 0) throw new NegativeAmountException(transaction.Amount);
        if (transaction.Amount > MaxTransactionAmount)
            throw new TransactionLimitExceededException(transaction.Amount, MaxTransactionAmount);

        var risk = _riskCalculator.CalculateRisk(transaction);
        transaction.RiskScore = risk;
        if (risk >= 5) throw new FraudDetectedException(transaction.Id, risk);

        if (!_byRisk.ContainsKey(risk)) _byRisk[risk] = new List<Transaction>();
        _byRisk[risk].Add(transaction);
    }

    public void ValidateAmount(decimal amount)
    {
        if (amount < 0) throw new NegativeAmountException(amount);
        if (amount > MaxTransactionAmount) throw new TransactionLimitExceededException(amount, MaxTransactionAmount);
    }

    public IReadOnlyDictionary<int, List<Transaction>> GetTransactionsByRisk() => _byRisk;
}
