namespace FlashSaleBidding.Exceptions;

public class BidTooLowException : AuctionException
{
    public decimal BidAmount { get; }
    public decimal MinimumBid { get; }
    public BidTooLowException(decimal bidAmount, decimal minimumBid)
        : base($"Bid {bidAmount} is below minimum {minimumBid}.") { BidAmount = bidAmount; MinimumBid = minimumBid; }
}
