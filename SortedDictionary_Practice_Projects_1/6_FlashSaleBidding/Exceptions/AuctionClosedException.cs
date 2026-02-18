namespace FlashSaleBidding.Exceptions;

public class AuctionClosedException : AuctionException
{
    public string AuctionId { get; }
    public AuctionClosedException(string auctionId) : base($"Auction {auctionId} is closed.") => AuctionId = auctionId;
}
