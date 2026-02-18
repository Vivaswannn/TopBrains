namespace InvestmentPortfolio.Domain;

public class Bonds : Asset
{
    public Bonds(string id, string name, int riskRating, decimal value) : base(id, name, riskRating, value) { }

    public override decimal CalculateReturn(decimal ratePercent) => Value * (ratePercent / 100m) * 0.8m;
}
