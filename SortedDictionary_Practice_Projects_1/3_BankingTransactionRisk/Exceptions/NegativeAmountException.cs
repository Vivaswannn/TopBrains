namespace BankingTransactionRisk.Exceptions;

public class NegativeAmountException : TransactionException
{
    public decimal Amount { get; }
    public NegativeAmountException(decimal amount) : base($"Invalid amount: {amount}. Amount cannot be negative.") => Amount = amount;
}
