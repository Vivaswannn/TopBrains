namespace InvestmentPortfolio.Exceptions;

public class InvalidRiskRatingException : PortfolioException
{
    public int Rating { get; }
    public InvalidRiskRatingException(int rating) : base($"Invalid risk rating: {rating}. Must be 1–5.") => Rating = rating;
}
