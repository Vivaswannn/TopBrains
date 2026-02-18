namespace InvestmentPortfolio.Domain;

public class MutualFunds : Asset
{
    public MutualFunds(string id, string name, int riskRating, decimal value) : base(id, name, riskRating, value) { }

    public override decimal CalculateReturn(decimal ratePercent) => Value * (ratePercent / 100m) * 0.9m;
}
