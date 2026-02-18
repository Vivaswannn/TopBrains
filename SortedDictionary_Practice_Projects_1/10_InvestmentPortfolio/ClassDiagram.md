# 10. Investment Portfolio Risk — Class Diagram

```
                    <<abstract>>
                       Asset
    + Id, Name, RiskRating, Value
    + CalculateReturn(ratePercent): decimal  (polymorphic)
           △
           |
    +------+------+------------+
    |             |            |
  Stocks       Bonds      MutualFunds
  (1x)         (0.8x)      (0.9x)
```

**Investment:** wraps Asset + PurchaseDate

**Exceptions:** `PortfolioException` → `InvalidRiskRatingException`, `InvestmentLimitExceededException`

**Core:** `SortedDictionary<int, List<Investment>>` — key = risk rating 1–5. RecalculatePortfolioRisk() = weighted average risk.
