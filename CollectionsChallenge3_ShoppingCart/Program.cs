using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionsChallenge3_ShoppingCart
{
    public abstract class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }

    public class ShoppingCart<T> where T : Product
    {
        private readonly Dictionary<T, int> _cartItems = new Dictionary<T, int>();

        public void AddToCart(T product, int quantity)
        {
            if (product == null || quantity <= 0) return;
            if (_cartItems.ContainsKey(product))
                _cartItems[product] += quantity;
            else
                _cartItems[product] = quantity;
        }

        public double CalculateTotal(Func<T, double, double> discountCalculator = null)
        {
            double total = 0;
            foreach (var item in _cartItems)
            {
                double price = item.Key.Price * item.Value;
                if (discountCalculator != null)
                    price = discountCalculator(item.Key, price);
                total += price;
            }
            return total;
        }

        public List<T> GetTopExpensiveItems(int n)
        {
            return _cartItems.Keys
                .OrderByDescending(p => p.Price)
                .Take(n)
                .ToList();
        }
    }

    public class Electronics : Product { }
    public class Clothing : Product { }

    class Program
    {
        static void Main()
        {
            var cart = new ShoppingCart<Electronics>();
            cart.AddToCart(new Electronics { Id = 1, Name = "Laptop", Price = 999.99 }, 1);
            cart.AddToCart(new Electronics { Id = 2, Name = "Mouse", Price = 29.99 }, 2);

            double total = cart.CalculateTotal((product, price) => price > 100 ? price * 0.9 : price);
            Console.WriteLine($"Total: ${total:F2}");

            var topItems = cart.GetTopExpensiveItems(1);
            if (topItems.Count > 0)
                Console.WriteLine(topItems[0].Name);
        }
    }
}
