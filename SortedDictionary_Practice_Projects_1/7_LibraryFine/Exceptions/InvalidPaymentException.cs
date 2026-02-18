namespace LibraryFine.Exceptions;

public class InvalidPaymentException : LibraryException
{
    public decimal Amount { get; }
    public InvalidPaymentException(decimal amount, string reason)
        : base($"Invalid payment: {amount}. {reason}") => Amount = amount;
}
