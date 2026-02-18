namespace BankingTransactionRisk.Domain;

public abstract class Transaction
{
    public string Id { get; }
    public decimal Amount { get; }
    public DateTime Timestamp { get; }
    public int RiskScore { get; set; }

    protected Transaction(string id, decimal amount, DateTime? timestamp = null)
    {
        if (amount < 0) throw new Exceptions.NegativeAmountException(amount);
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Amount = amount;
        Timestamp = timestamp ?? DateTime.UtcNow;
    }

    public abstract string GetTransactionType();
}
