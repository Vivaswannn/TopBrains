# 8. Smart Traffic Violation — Class Diagram

```
                    <<abstract>>
                      Vehicle
    + PlateNumber, Type
           △
           |
    +------+------+------+
    |             |      |
   Car          Truck   Bike
```

**Violation:** Id, Vehicle, PenaltyAmount, Description, Date

**Exceptions:** `ViolationException` → `InvalidVehicleException`, `PenaltyExceedsLimitException`

**Core:** `SortedDictionary<decimal, List<Violation>>` — key = penalty amount. Encapsulate violation logic in AddViolation; escalate via GetRepeatOffenders.
