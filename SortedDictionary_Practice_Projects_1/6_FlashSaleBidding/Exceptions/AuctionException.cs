namespace FlashSaleBidding.Exceptions;

public abstract class AuctionException : Exception
{
    protected AuctionException(string message) : base(message) { }
    protected AuctionException(string message, Exception inner) : base(message, inner) { }
}
