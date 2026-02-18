using FlashSaleBidding.Domain;
using FlashSaleBidding.Exceptions;
using FlashSaleBidding.Services;

var service = new BiddingEngineService();
service.OpenAuction(minimumBid: 10);

var b1 = new Bid("B1", "user1", 50m);
var b2 = new Bid("B2", "user2", 75m);
service.AddBid(b1);
service.AddBid(b2);

var winner = service.DetermineWinner();
Console.WriteLine(winner != null ? $"Winner: Bid {winner.BidId} for {winner.Amount:C}" : "No bids.");

service.CloseAuction();
try { service.AddBid(new Bid("B3", "user3", 100m)); }
catch (AuctionClosedException ex) { Console.WriteLine($"\nExpected: {ex.Message}"); }

Console.WriteLine("Done.");
