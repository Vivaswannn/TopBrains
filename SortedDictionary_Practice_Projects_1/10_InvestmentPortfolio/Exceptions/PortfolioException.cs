namespace InvestmentPortfolio.Exceptions;

public abstract class PortfolioException : Exception
{
    protected PortfolioException(string message) : base(message) { }
    protected PortfolioException(string message, Exception inner) : base(message, inner) { }
}
