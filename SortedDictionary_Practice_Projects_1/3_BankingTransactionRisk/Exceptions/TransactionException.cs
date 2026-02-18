namespace BankingTransactionRisk.Exceptions;

public abstract class TransactionException : Exception
{
    protected TransactionException(string message) : base(message) { }
    protected TransactionException(string message, Exception inner) : base(message, inner) { }
}
