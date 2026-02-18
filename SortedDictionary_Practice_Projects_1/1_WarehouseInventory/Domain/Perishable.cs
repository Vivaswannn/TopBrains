namespace WarehouseInventory.Domain;

public class Perishable : Product
{
    public DateTime ExpiryDate { get; }

    public Perishable(string sku, string name, int priority, int initialStock, DateTime expiryDate)
        : base(sku, name, priority, initialStock)
    {
        ExpiryDate = expiryDate;
    }

    public override string GetCategoryDescription() => $"Perishable (Expires: {ExpiryDate:yyyy-MM-dd})";
}
