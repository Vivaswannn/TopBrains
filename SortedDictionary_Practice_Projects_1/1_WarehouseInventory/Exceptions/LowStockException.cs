namespace WarehouseInventory.Exceptions;

/// <summary>Raised when stock falls below threshold.</summary>
public class LowStockException : InventoryException
{
    public string Sku { get; }
    public int CurrentStock { get; }
    public int Threshold { get; }

    public LowStockException(string sku, int currentStock, int threshold)
        : base($"Low stock for SKU '{sku}': current={currentStock}, threshold={threshold}.")
    {
        Sku = sku;
        CurrentStock = currentStock;
        Threshold = threshold;
    }
}
