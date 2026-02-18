using System.Linq;
using InvestmentPortfolio.Domain;
using InvestmentPortfolio.Exceptions;

namespace InvestmentPortfolio.Services;

/// <summary>SortedDictionary&lt;int, List&lt;Investment&gt;&gt; — key = risk rating (1–5).</summary>
public class PortfolioRiskService
{
    private readonly SortedDictionary<int, List<Investment>> _byRisk = new();
    private const int MaxInvestments = 100;

    public void AddInvestment(Investment investment)
    {
        if (investment?.Asset == null) throw new ArgumentNullException(nameof(investment));
        var rating = investment.Asset.RiskRating;
        if (rating < 1 || rating > 5) throw new InvalidRiskRatingException(rating);

        var total = _byRisk.Values.Sum(list => list.Count);
        if (total >= MaxInvestments)
            throw new InvestmentLimitExceededException(total, MaxInvestments);

        if (!_byRisk.ContainsKey(rating)) _byRisk[rating] = new List<Investment>();
        _byRisk[rating].Add(investment);
    }

    /// <summary>Recalculate portfolio risk (e.g. weighted average risk).</summary>
    public double RecalculatePortfolioRisk()
    {
        double totalValue = 0;
        double weightedRisk = 0;
        foreach (var kv in _byRisk)
            foreach (var inv in kv.Value)
            {
                var v = (double)inv.Asset.Value;
                totalValue += v;
                weightedRisk += kv.Key * v;
            }
        return totalValue > 0 ? weightedRisk / totalValue : 0;
    }

    public IReadOnlyDictionary<int, List<Investment>> GetInvestmentsByRisk() => _byRisk;
}
