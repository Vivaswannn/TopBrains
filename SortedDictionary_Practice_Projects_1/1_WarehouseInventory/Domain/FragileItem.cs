namespace WarehouseInventory.Domain;

public class FragileItem : Product
{
    public string HandlingInstructions { get; }

    public FragileItem(string sku, string name, int priority, int initialStock, string handlingInstructions = "Handle with care")
        : base(sku, name, priority, initialStock)
    {
        HandlingInstructions = handlingInstructions;
    }

    public override string GetCategoryDescription() => $"Fragile ({HandlingInstructions})";
}
