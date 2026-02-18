namespace InvestmentPortfolio.Domain;

public class Investment
{
    public Asset Asset { get; }
    public DateTime PurchaseDate { get; }

    public Investment(Asset asset, DateTime? purchaseDate = null)
    {
        Asset = asset ?? throw new ArgumentNullException(nameof(asset));
        PurchaseDate = purchaseDate ?? DateTime.UtcNow;
    }
}
