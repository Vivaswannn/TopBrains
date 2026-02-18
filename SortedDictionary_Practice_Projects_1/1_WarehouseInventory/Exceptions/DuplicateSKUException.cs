namespace WarehouseInventory.Exceptions;

/// <summary>Raised when adding a product with an SKU that already exists.</summary>
public class DuplicateSKUException : InventoryException
{
    public string Sku { get; }

    public DuplicateSKUException(string sku)
        : base($"Duplicate SKU: '{sku}' already exists in inventory.")
    {
        Sku = sku;
    }
}
