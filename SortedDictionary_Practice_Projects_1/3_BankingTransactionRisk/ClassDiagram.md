# 3. Banking Transaction Risk — Class Diagram

```
                    <<interface>>
                  IRiskCalculator
                  + CalculateRisk(Transaction): int
                           △
                           | (Strategy)
                  DefaultRiskCalculator

                    <<abstract>>
                   Transaction
    + Id, Amount, Timestamp, RiskScore
    + GetTransactionType(): string
           △
           |
    +------+--------+
    |               |
OnlineTransaction  WireTransfer
+ IpAddress        + SwiftCode
```

**Exceptions:** `TransactionException` → `FraudDetectedException`, `NegativeAmountException`, `TransactionLimitExceededException`

**Core:** `SortedDictionary<int, List<Transaction>>` with custom comparer for descending risk (high first). Strategy pattern: IRiskCalculator.
