# 6. E-Commerce Flash Sale Bidding — Class Diagram

```
                    <<abstract>>
                       User
    + Id, Name
           △
           |
    +------+------+
    |             |
  Buyer        Seller
```

**Bid:** BidId, UserId, Amount, Timestamp

**Exceptions:** `AuctionException` → `BidTooLowException`, `AuctionClosedException`

**Core:** `SortedDictionary<decimal, List<Bid>>` — descending by amount. Winner = first entry. Polymorphic bid validation in AddBid.
