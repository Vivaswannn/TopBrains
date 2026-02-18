using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericsScenario1_ECommerce
{
    public interface IProduct
    {
        int Id { get; }
        string Name { get; }
        decimal Price { get; }
        Category Category { get; }
    }

    public enum Category { Electronics, Clothing, Books, Groceries }

    public class ProductRepository<T> where T : class, IProduct
    {
        private List<T> _products = new List<T>();

        public void AddProduct(T product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Name cannot be null or empty.");
            if (product.Price <= 0)
                throw new ArgumentException("Price must be positive.");
            if (_products.Any(p => p.Id == product.Id))
                throw new InvalidOperationException($"Product ID {product.Id} already exists.");
            _products.Add(product);
        }

        public IEnumerable<T> FindProducts(Func<T, bool> predicate)
        {
            return _products.Where(predicate);
        }

        public decimal CalculateTotalValue()
        {
            return _products.Sum(p => p.Price);
        }

        public IReadOnlyList<T> GetAll() => _products;
    }

    public class ElectronicProduct : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Category Category => Category.Electronics;
        public int WarrantyMonths { get; set; }
        public string Brand { get; set; }
    }

    public class DiscountedProduct<T> where T : IProduct
    {
        private readonly T _product;
        private readonly decimal _discountPercentage;

        public DiscountedProduct(T product, decimal discountPercentage)
        {
            _product = product ?? throw new ArgumentNullException(nameof(product));
            if (discountPercentage < 0 || discountPercentage > 100)
                throw new ArgumentOutOfRangeException(nameof(discountPercentage), "Discount must be between 0 and 100.");
            _discountPercentage = discountPercentage;
        }

        public decimal DiscountedPrice => _product.Price * (1 - _discountPercentage / 100);

        public override string ToString() => $"{_product.Name} - Original: {_product.Price:C}, Discount: {_discountPercentage}%, Final: {DiscountedPrice:C}";
    }

    public class InventoryManager
    {
        public void ProcessProducts<T>(IEnumerable<T> products) where T : IProduct
        {
            var list = products.ToList();
            foreach (var p in list)
                Console.WriteLine($"{p.Name} - {p.Price:C}");
            var mostExpensive = list.OrderByDescending(p => p.Price).FirstOrDefault();
            if (mostExpensive != null)
                Console.WriteLine($"Most expensive: {mostExpensive.Name} - {mostExpensive.Price:C}");
            foreach (var group in list.GroupBy(p => p.Category))
                Console.WriteLine($"Category {group.Key}: {group.Count()} items");
            foreach (var p in list.Where(p => p.Category == Category.Electronics && p.Price > 500))
                Console.WriteLine($"Electronics over $500 (10% off): {p.Name} - {p.Price * 0.9m:C}");
        }

        public void UpdatePrices<T>(List<T> products, Func<T, decimal> priceAdjuster) where T : IProduct
        {
            foreach (var p in products)
            {
                try
                {
                    var newPrice = priceAdjuster(p);
                    if (newPrice <= 0) continue;
                    if (p is ElectronicProduct ep)
                        ep.Price = newPrice;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Update failed for {p.Name}: {ex.Message}");
                }
            }
        }
    }

    class Program
    {
        static void Main()
        {
            var repo = new ProductRepository<ElectronicProduct>();
            repo.AddProduct(new ElectronicProduct { Id = 1, Name = "Laptop", Price = 999m, Brand = "Dell", WarrantyMonths = 24 });
            repo.AddProduct(new ElectronicProduct { Id = 2, Name = "Phone", Price = 599m, Brand = "Apple", WarrantyMonths = 12 });
            repo.AddProduct(new ElectronicProduct { Id = 3, Name = "Monitor", Price = 350m, Brand = "Dell", WarrantyMonths = 36 });
            repo.AddProduct(new ElectronicProduct { Id = 4, Name = "Tablet", Price = 450m, Brand = "Samsung", WarrantyMonths = 12 });
            repo.AddProduct(new ElectronicProduct { Id = 5, Name = "Headphones", Price = 150m, Brand = "Sony", WarrantyMonths = 24 });

            Console.WriteLine("Total value: " + repo.CalculateTotalValue().ToString("C"));
            var byBrand = repo.FindProducts(p => p.Brand == "Dell");
            Console.WriteLine("Dell products: " + string.Join(", ", byBrand.Select(p => p.Name)));
            var discounted = new DiscountedProduct<ElectronicProduct>(repo.GetAll().First(), 15);
            Console.WriteLine("Discounted: " + discounted);
            decimal totalBefore = repo.CalculateTotalValue();
            var allDiscounted = repo.GetAll().Select(p => new DiscountedProduct<ElectronicProduct>(p, 10));
            decimal totalAfter = allDiscounted.Sum(d => d.DiscountedPrice);
            Console.WriteLine($"Total before discount: {totalBefore:C}, after 10%: {totalAfter:C}");

            var manager = new InventoryManager();
            manager.ProcessProducts(repo.GetAll());
        }
    }
}
