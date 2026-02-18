using WarehouseInventory.Domain;
using WarehouseInventory.Exceptions;
using WarehouseInventory.Services;

var service = new WarehouseService();

// Add products (polymorphism: Electronics, Perishable, FragileItem)
Product[] products =
{
    new Electronics("ELEC-001", "Laptop", 1, 20),
    new Perishable("PER-001", "Milk", 2, 15, DateTime.Now.AddDays(7)),
    new FragileItem("FRAG-001", "Glass Vase", 3, 8)
};

foreach (var p in products)
{
    service.AddProduct(p);
    Console.WriteLine($"Added: {p.Sku} - {p.GetCategoryDescription()}");
}

Console.WriteLine("\n--- Highest priority products ---");
foreach (var p in service.GetHighestPriorityProducts(5))
    Console.WriteLine($"P{p.Priority} {p.Sku} {p.Name} Stock={p.Stock}");

// Exceptions demo
try { service.AddProduct(new Electronics("ELEC-001", "Duplicate", 1, 10)); }
catch (DuplicateSKUException ex) { Console.WriteLine($"\nExpected: {ex.Message}"); }

try { service.UpdateStock("ELEC-001", 2); }
catch (LowStockException ex) { Console.WriteLine($"Expected: {ex.Message}"); }

Console.WriteLine("\nDone.");
