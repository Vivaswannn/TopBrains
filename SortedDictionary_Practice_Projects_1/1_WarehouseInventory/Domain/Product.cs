namespace WarehouseInventory.Domain;

/// <summary>Abstract product. Priority 1 = Critical, 10 = Low.</summary>
public abstract class Product
{
    public string Sku { get; }
    public string Name { get; }
    public int Priority { get; } // 1 = Critical Stock, 10 = Low Priority
    private int _stock;
    private readonly int _lowStockThreshold;

    protected Product(string sku, string name, int priority, int initialStock, int lowStockThreshold = 5)
    {
        if (priority < 1 || priority > 10)
            throw new ArgumentOutOfRangeException(nameof(priority), "Priority must be 1-10.");
        Sku = sku ?? throw new ArgumentNullException(nameof(sku));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Priority = priority;
        _lowStockThreshold = lowStockThreshold;
        SetStockValidated(initialStock);
    }

    /// <summary>Encapsulated stock validation. Throws LowStockException when below threshold.</summary>
    protected void SetStockValidated(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Stock cannot be negative.");
        _stock = value;
    }

    public int Stock
    {
        get => _stock;
        set => SetStockValidated(value);
    }

    public int LowStockThreshold => _lowStockThreshold;

    /// <summary>Check if current stock is below threshold (for exception).</summary>
    public bool IsBelowThreshold => _stock < _lowStockThreshold;

    public abstract string GetCategoryDescription();
}
