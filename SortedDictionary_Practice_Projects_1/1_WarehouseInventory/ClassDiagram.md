# 1. Smart Warehouse Inventory — Class Diagram

```
                    <<abstract>>
                    Product
    + Sku, Name, Priority, Stock, LowStockThreshold
    + GetCategoryDescription(): string
    + IsBelowThreshold: bool
           △
           |
    +------+------+--------+
    |             |        |
Electronics  Perishable  FragileItem
+ Warranty   + ExpiryDate  + HandlingInstructions
```

**Exceptions:** `InventoryException` → `LowStockException`, `DuplicateSKUException`, `InvalidProductException`

**Core:** `SortedDictionary<int, List<Product>>` — key = priority (1–10), auto-sorted by priority.
