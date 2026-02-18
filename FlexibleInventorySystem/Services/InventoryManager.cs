using System;
using System.Collections.Generic;
using System.Linq;
using FlexibleInventorySystem.Interfaces;
using FlexibleInventorySystem.Models;
using FlexibleInventorySystem.Utilities;

namespace FlexibleInventorySystem.Services
{
    public class InventoryManager : IInventoryOperations, IReportGenerator
    {
        private readonly List<Product> _products;

        public InventoryManager()
        {
            _products = new List<Product>();
        }

        public bool AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            string errorMessage;
            if (!ProductValidator.ValidateProduct(product, out errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            if (_products.Any(p => p.Id == product.Id))
            {
                throw new ArgumentException($"Product with ID {product.Id} already exists.");
            }

            _products.Add(product);
            return true;
        }

        public bool RemoveProduct(string productId)
        {
            if (string.IsNullOrWhiteSpace(productId))
            {
                return false;
            }

            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return false;
            }

            _products.Remove(product);
            return true;
        }

        public Product FindProduct(string productId)
        {
            if (string.IsNullOrWhiteSpace(productId))
            {
                return null;
            }

            return _products.FirstOrDefault(p => p.Id.Equals(productId, StringComparison.OrdinalIgnoreCase));
        }

        public List<Product> GetProductsByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return new List<Product>();
            }

            return _products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public bool UpdateQuantity(string productId, int newQuantity)
        {
            if (newQuantity < 0)
            {
                return false;
            }

            var product = FindProduct(productId);
            if (product == null)
            {
                return false;
            }

            product.Quantity = newQuantity;
            return true;
        }

        public decimal GetTotalInventoryValue()
        {
            return _products.Sum(p => p.CalculateValue());
        }

        public List<Product> GetLowStockProducts(int threshold)
        {
            return _products.Where(p => p.Quantity < threshold).ToList();
        }

        public string GenerateInventoryReport()
        {
            var report = new System.Text.StringBuilder();
            report.AppendLine("================================");
            report.AppendLine("INVENTORY REPORT");
            report.AppendLine("================================");
            report.AppendLine($"Total Products: {_products.Count}");
            report.AppendLine($"Total Value: {GetTotalInventoryValue():C}");
            report.AppendLine();
            report.AppendLine("Product List:");

            foreach (var product in _products)
            {
                report.AppendLine($"{product.Id} - {product.Name} - {product.Category} - Quantity: {product.Quantity} - Value: {product.CalculateValue():C}");
            }

            return report.ToString();
        }

        public string GenerateCategorySummary()
        {
            var report = new System.Text.StringBuilder();
            report.AppendLine("CATEGORY SUMMARY");

            var groups = _products.GroupBy(p => p.Category);
            foreach (var group in groups)
            {
                int count = group.Count();
                decimal totalValue = group.Sum(p => p.CalculateValue());
                report.AppendLine($"{group.Key}: {count} items - Total Value: {totalValue:C}");
            }

            return report.ToString();
        }

        public string GenerateValueReport()
        {
            var report = new System.Text.StringBuilder();
            report.AppendLine("VALUE ANALYSIS REPORT");

            if (!_products.Any())
            {
                report.AppendLine("No products in inventory.");
                return report.ToString();
            }

            var mostValuable = _products.OrderByDescending(p => p.CalculateValue()).First();
            var leastValuable = _products.OrderBy(p => p.CalculateValue()).First();
            var avgPrice = _products.Average(p => p.Price);
            var prices = _products.Select(p => p.Price).OrderBy(p => p).ToList();
            decimal medianPrice = prices.Count % 2 == 0
                ? (prices[prices.Count / 2 - 1] + prices[prices.Count / 2]) / 2
                : prices[prices.Count / 2];

            report.AppendLine($"Most Valuable Product: {mostValuable.Name} - Value: {mostValuable.CalculateValue():C}");
            report.AppendLine($"Least Valuable Product: {leastValuable.Name} - Value: {leastValuable.CalculateValue():C}");
            report.AppendLine($"Average Price: {avgPrice:C}");
            report.AppendLine($"Median Price: {medianPrice:C}");
            report.AppendLine();
            report.AppendLine("Products Above Average Price:");

            foreach (var product in _products.Where(p => p.Price > avgPrice))
            {
                report.AppendLine($"  {product.Name} - {product.Price:C}");
            }

            return report.ToString();
        }

        public string GenerateExpiryReport(int daysThreshold)
        {
            var report = new System.Text.StringBuilder();
            report.AppendLine($"EXPIRY REPORT (Products expiring within {daysThreshold} days)");

            var expiringProducts = _products
                .OfType<GroceryProduct>()
                .Where(g => g.DaysUntilExpiry() <= daysThreshold && g.DaysUntilExpiry() >= 0)
                .ToList();

            if (!expiringProducts.Any())
            {
                report.AppendLine("No products expiring within the threshold.");
                return report.ToString();
            }

            foreach (var product in expiringProducts)
            {
                report.AppendLine($"{product.Name} - Expires in {product.DaysUntilExpiry()} days - Expiry Date: {product.ExpiryDate:d}");
            }

            return report.ToString();
        }

        public IEnumerable<Product> SearchProducts(Func<Product, bool> predicate)
        {
            return _products.Where(predicate);
        }

        public int GetTotalProductCount()
        {
            return _products.Count;
        }

        public IEnumerable<string> GetCategories()
        {
            return _products.Select(p => p.Category).Distinct();
        }
    }
}
