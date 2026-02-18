using FlashSaleBidding.Domain;
using FlashSaleBidding.Exceptions;

namespace FlashSaleBidding.Services;

/// <summary>SortedDictionary&lt;decimal, List&lt;Bid&gt;&gt; — key = bid amount, descending (highest first).</summary>
public class BiddingEngineService
{
    private readonly SortedDictionary<decimal, List<Bid>> _bids = new(Comparer<decimal>.Create((a, b) => b.CompareTo(a)));
    private decimal _minimumBid;
    private bool _isClosed;

    public void OpenAuction(decimal minimumBid = 0)
    {
        _minimumBid = minimumBid;
        _isClosed = false;
    }

    public void CloseAuction() => _isClosed = true;

    /// <summary>Polymorphic bid validation: ensure bid is valid for auction state.</summary>
    public void AddBid(Bid bid)
    {
        if (bid == null) throw new ArgumentNullException(nameof(bid));
        if (_isClosed) throw new AuctionClosedException("AUCTION-1");
        if (bid.Amount < _minimumBid) throw new BidTooLowException(bid.Amount, _minimumBid);

        if (!_bids.ContainsKey(bid.Amount)) _bids[bid.Amount] = new List<Bid>();
        _bids[bid.Amount].Add(bid);
    }

    /// <summary>Determine winner: highest bid (first key in sorted dict).</summary>
    public Bid? DetermineWinner()
    {
        foreach (var kv in _bids)
            if (kv.Value.Count > 0) return kv.Value[0];
        return null;
    }

    public IReadOnlyDictionary<decimal, List<Bid>> GetAllBids() => _bids;
}
