# 7. Library Fine & Penalty — Class Diagram

```
                    <<abstract>>
                   LibraryUser
    + MemberId, Name
    + CalculateFine(daysOverdue): decimal  (override)
           △
           |
    +------+------+
    |             |
StudentMember  FacultyMember
(0.25/day)     (0.10/day)
```

**Member:** wraps LibraryUser + OutstandingFine. **Exceptions:** `LibraryException` → `FineNotFoundException`, `InvalidPaymentException`

**Core:** `SortedDictionary<decimal, Member>` — key = outstanding fine.
