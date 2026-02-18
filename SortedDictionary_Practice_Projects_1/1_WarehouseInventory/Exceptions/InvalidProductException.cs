namespace WarehouseInventory.Exceptions;

/// <summary>Raised when product data is invalid.</summary>
public class InvalidProductException : InventoryException
{
    public InvalidProductException(string message) : base(message) { }
    public InvalidProductException(string message, Exception inner) : base(message, inner) { }
}
