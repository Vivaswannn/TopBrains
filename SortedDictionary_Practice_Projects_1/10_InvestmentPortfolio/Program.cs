using InvestmentPortfolio.Domain;
using InvestmentPortfolio.Exceptions;
using InvestmentPortfolio.Services;

var service = new PortfolioRiskService();

Asset[] assets =
{
    new Stocks("S1", "Tech Corp", 4, 10_000m),
    new Bonds("B1", "Gov Bond", 1, 5_000m),
    new MutualFunds("M1", "Index Fund", 2, 8_000m)
};

foreach (var a in assets)
{
    service.AddInvestment(new Investment(a));
    Console.WriteLine($"{a.GetType().Name} {a.Name}: Return @5% = {a.CalculateReturn(5m):C}");
}

var portfolioRisk = service.RecalculatePortfolioRisk();
Console.WriteLine($"\nPortfolio risk (weighted avg): {portfolioRisk:F2}");

try { new Stocks("X", "Y", 10, 100m); }
catch (InvalidRiskRatingException ex) { Console.WriteLine($"\nExpected: {ex.Message}"); }

Console.WriteLine("Done.");
