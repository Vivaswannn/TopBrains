namespace BankingTransactionRisk.Exceptions;

public class FraudDetectedException : TransactionException
{
    public string TransactionId { get; }
    public int RiskScore { get; }
    public FraudDetectedException(string transactionId, int riskScore)
        : base($"Fraud detected for transaction {transactionId} (risk score: {riskScore}).") { TransactionId = transactionId; RiskScore = riskScore; }
}
