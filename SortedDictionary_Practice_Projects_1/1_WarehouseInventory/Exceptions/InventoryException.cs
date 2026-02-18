namespace WarehouseInventory.Exceptions;

/// <summary>Base exception for inventory operations.</summary>
public abstract class InventoryException : Exception
{
    protected InventoryException(string message) : base(message) { }
    protected InventoryException(string message, Exception inner) : base(message, inner) { }
}
