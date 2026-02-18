namespace FlashSaleBidding.Domain;

public class Bid
{
    public string BidId { get; }
    public string UserId { get; }
    public decimal Amount { get; }
    public DateTime Timestamp { get; }

    public Bid(string bidId, string userId, decimal amount, DateTime? timestamp = null)
    {
        if (amount < 0) throw new ArgumentException("Bid amount cannot be negative.", nameof(amount));
        BidId = bidId ?? throw new ArgumentNullException(nameof(bidId));
        UserId = userId;
        Amount = amount;
        Timestamp = timestamp ?? DateTime.UtcNow;
    }
}
