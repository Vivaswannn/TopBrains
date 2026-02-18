namespace WarehouseInventory.Domain;

public class Electronics : Product
{
    public string WarrantyMonths { get; }

    public Electronics(string sku, string name, int priority, int initialStock, string warrantyMonths = "12")
        : base(sku, name, priority, initialStock)
    {
        WarrantyMonths = warrantyMonths;
    }

    public override string GetCategoryDescription() => $"Electronics (Warranty: {WarrantyMonths} months)";
}
