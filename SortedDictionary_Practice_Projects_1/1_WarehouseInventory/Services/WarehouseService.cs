using WarehouseInventory.Domain;
using WarehouseInventory.Exceptions;

namespace WarehouseInventory.Services;

/// <summary>SortedDictionary&lt;int, List&lt;Product&gt;&gt; — key = priority (1–10), auto-sorted.</summary>
public class WarehouseService
{
    private readonly SortedDictionary<int, List<Product>> _inventory = new();
    private readonly Dictionary<string, Product> _bySku = new();
    private const int LowStockThresholdDefault = 5;

    public void AddProduct(Product product)
    {
        if (product == null) throw new InvalidProductException("Product cannot be null.");
        if (_bySku.ContainsKey(product.Sku))
            throw new DuplicateSKUException(product.Sku);
        if (product.Stock < LowStockThresholdDefault)
            throw new LowStockException(product.Sku, product.Stock, LowStockThresholdDefault);

        if (!_inventory.ContainsKey(product.Priority))
            _inventory[product.Priority] = new List<Product>();
        _inventory[product.Priority].Add(product);
        _bySku[product.Sku] = product;
    }

    public void RemoveProduct(string sku)
    {
        if (!_bySku.TryGetValue(sku, out var product))
            return;
        _inventory[product.Priority].Remove(product);
        if (_inventory[product.Priority].Count == 0)
            _inventory.Remove(product.Priority);
        _bySku.Remove(sku);
    }

    public void UpdateStock(string sku, int newStock)
    {
        if (!_bySku.TryGetValue(sku, out var product))
            return;
        if (newStock < 0)
            throw new InvalidProductException("Stock cannot be negative.");
        if (newStock < product.LowStockThreshold)
            throw new LowStockException(sku, newStock, product.LowStockThreshold);
        product.Stock = newStock;
    }

    /// <summary>Get products with highest priority (lowest key = 1).</summary>
    public IReadOnlyList<Product> GetHighestPriorityProducts(int maxCount = 10)
    {
        var result = new List<Product>();
        foreach (var kv in _inventory)
        {
            foreach (var p in kv.Value)
            {
                if (result.Count >= maxCount) return result;
                result.Add(p);
            }
        }
        return result;
    }

    public IReadOnlyDictionary<int, List<Product>> GetAllByPriority() => _inventory;
}
