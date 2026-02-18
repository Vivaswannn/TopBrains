namespace BankingTransactionRisk.Exceptions;

public class TransactionLimitExceededException : TransactionException
{
    public decimal Amount { get; }
    public decimal Limit { get; }
    public TransactionLimitExceededException(decimal amount, decimal limit)
        : base($"Transaction amount {amount} exceeds limit {limit}.") { Amount = amount; Limit = limit; }
}
