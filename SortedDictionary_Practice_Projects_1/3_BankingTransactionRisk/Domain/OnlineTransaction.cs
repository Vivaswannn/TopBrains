namespace BankingTransactionRisk.Domain;

public class OnlineTransaction : Transaction
{
    public string IpAddress { get; }

    public OnlineTransaction(string id, decimal amount, string ipAddress, DateTime? timestamp = null)
        : base(id, amount, timestamp)
    {
        IpAddress = ipAddress ?? "";
    }

    public override string GetTransactionType() => "Online";
}
