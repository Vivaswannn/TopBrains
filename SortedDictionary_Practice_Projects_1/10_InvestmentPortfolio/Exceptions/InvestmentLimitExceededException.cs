namespace InvestmentPortfolio.Exceptions;

public class InvestmentLimitExceededException : PortfolioException
{
    public int CurrentCount { get; }
    public int Limit { get; }
    public InvestmentLimitExceededException(int currentCount, int limit)
        : base($"Investment count {currentCount} exceeds limit {limit}.") { CurrentCount = currentCount; Limit = limit; }
}
