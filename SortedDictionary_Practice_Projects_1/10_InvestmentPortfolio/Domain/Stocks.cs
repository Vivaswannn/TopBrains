namespace InvestmentPortfolio.Domain;

public class Stocks : Asset
{
    public Stocks(string id, string name, int riskRating, decimal value) : base(id, name, riskRating, value) { }

    public override decimal CalculateReturn(decimal ratePercent) => Value * (ratePercent / 100m);
}
