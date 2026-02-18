namespace BankingTransactionRisk.Domain;

public class WireTransfer : Transaction
{
    public string SwiftCode { get; }

    public WireTransfer(string id, decimal amount, string swiftCode, DateTime? timestamp = null)
        : base(id, amount, timestamp)
    {
        SwiftCode = swiftCode ?? "";
    }

    public override string GetTransactionType() => "WireTransfer";
}
