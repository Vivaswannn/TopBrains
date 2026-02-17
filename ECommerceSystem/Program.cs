using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem
{
    public enum OrderStatus
    {
        Pending,
        Shipped,
        Cancelled
    }

    public class OutOfStockException : Exception
    {
        public OutOfStockException(string message) : base(message) { }
    }

    public class OrderAlreadyShippedException : Exception
    {
        public OrderAlreadyShippedException(string message) : base(message) { }
    }

    public class CustomerBlacklistedException : Exception
    {
        public CustomerBlacklistedException(string message) : base(message) { }
    }

    public class Product
    {
        public int Id { get; }
        public string Name { get; }
        public decimal Price { get; }
        public int Stock { get; private set; }

        public Product(int id, string name, decimal price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
        }

        public void ReduceStock(int quantity)
        {
            if (Stock < quantity)
            {
                throw new OutOfStockException($"Not enough stock for {Name}. In stock: {Stock}, requested: {quantity}");
            }

            Stock -= quantity;
        }

        public override string ToString()
        {
            return $"{Id} - {Name} - Price: {Price:C} - Stock: {Stock}";
        }
    }

    public class Customer
    {
        public int Id { get; }
        public string Name { get; }
        public bool IsBlacklisted { get; }

        public Customer(int id, string name, bool isBlacklisted = false)
        {
            Id = id;
            Name = name;
            IsBlacklisted = isBlacklisted;
        }

        public override string ToString()
        {
            return $"{Id} - {Name} - Blacklisted: {IsBlacklisted}";
        }
    }

    public interface IDiscountStrategy
    {
        decimal ApplyDiscount(decimal amount);
        string Description { get; }
    }

    public class NoDiscount : IDiscountStrategy
    {
        public string Description => "No discount";

        public decimal ApplyDiscount(decimal amount)
        {
            return amount;
        }
    }

    public class PercentageDiscount : IDiscountStrategy
    {
        public decimal Percentage { get; }

        public string Description => $"{Percentage:P0} percentage discount";

        public PercentageDiscount(decimal percentage)
        {
            Percentage = percentage;
        }

        public decimal ApplyDiscount(decimal amount)
        {
            return amount - (amount * Percentage);
        }
    }

    public class FlatDiscount : IDiscountStrategy
    {
        public decimal Amount { get; }

        public string Description => $"{Amount:C} flat discount";

        public FlatDiscount(decimal amount)
        {
            Amount = amount;
        }

        public decimal ApplyDiscount(decimal amount)
        {
            var result = amount - Amount;
            return result < 0 ? 0 : result;
        }
    }

    public class FestivalDiscount : IDiscountStrategy
    {
        public string FestivalName { get; }
        public decimal Percentage { get; }

        public string Description => $"{FestivalName} {Percentage:P0} discount";

        public FestivalDiscount(string festivalName, decimal percentage)
        {
            FestivalName = festivalName;
            Percentage = percentage;
        }

        public decimal ApplyDiscount(decimal amount)
        {
            return amount - (amount * Percentage);
        }
    }

    public class OrderItem
    {
        public Product Product { get; }
        public int Quantity { get; }

        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public decimal TotalPrice()
        {
            return Product.Price * Quantity;
        }

        public override string ToString()
        {
            return $"{Product.Name} x {Quantity} = {TotalPrice():C}";
        }
    }

    public class Order
    {
        public int OrderId { get; }
        public Customer Customer { get; }
        public List<OrderItem> Items { get; } = new List<OrderItem>();
        public DateTime OrderDate { get; }
        public OrderStatus Status { get; private set; }
        public IDiscountStrategy DiscountStrategy { get; private set; }

        public Order(int orderId, Customer customer, IDiscountStrategy discountStrategy)
        {
            OrderId = orderId;
            Customer = customer;
            DiscountStrategy = discountStrategy;
            OrderDate = DateTime.Now;
            Status = OrderStatus.Pending;
        }

        public void AddItem(Product product, int quantity)
        {
            if (Status != OrderStatus.Pending)
            {
                throw new InvalidOperationException("Can only add items to a pending order.");
            }

            product.ReduceStock(quantity);
            Items.Add(new OrderItem(product, quantity));
        }

        public decimal GetSubTotal()
        {
            return Items.Sum(i => i.TotalPrice());
        }

        public decimal GetTotal()
        {
            var subtotal = GetSubTotal();
            return DiscountStrategy.ApplyDiscount(subtotal);
        }

        public void Ship()
        {
            if (Status == OrderStatus.Shipped)
            {
                return;
            }

            if (Status == OrderStatus.Cancelled)
            {
                throw new InvalidOperationException("Cancelled order cannot be shipped.");
            }

            Status = OrderStatus.Shipped;
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Shipped)
            {
                throw new OrderAlreadyShippedException($"Order {OrderId} is already shipped and cannot be cancelled.");
            }

            Status = OrderStatus.Cancelled;
        }

        public override string ToString()
        {
            return $"Order {OrderId} - {Customer.Name} - Date: {OrderDate:g} - Status: {Status} - Total: {GetTotal():C} ({DiscountStrategy.Description})";
        }
    }

    public static class ECommerceStore
    {
        public static List<Product> Products { get; } = new List<Product>();
        public static List<Customer> Customers { get; } = new List<Customer>();
        public static List<Order> Orders { get; } = new List<Order>();
        public static Dictionary<int, Product> ProductDictionary { get; } = new Dictionary<int, Product>();

        private static int _nextOrderId = 1;

        public static void Seed()
        {
            if (Products.Any())
            {
                return;
            }

            AddProduct(new Product(1, "Laptop", 75000m, 10));
            AddProduct(new Product(2, "Phone", 30000m, 20));
            AddProduct(new Product(3, "Headphones", 2000m, 5));
            AddProduct(new Product(4, "Mouse", 800m, 50));

            Customers.Add(new Customer(1, "Rahul"));
            Customers.Add(new Customer(2, "Riya"));
            Customers.Add(new Customer(3, "John"));
            Customers.Add(new Customer(4, "Ramesh", isBlacklisted: true));

            var o1 = CreateOrder(Customers[0], new PercentageDiscount(0.10m));
            o1.AddItem(Products[0], 1);
            o1.AddItem(Products[2], 2);
            o1.Ship();
            Orders.Add(o1);

            var o2 = CreateOrder(Customers[1], new FlatDiscount(500m));
            o2.AddItem(Products[1], 1);
            Orders.Add(o2);

            var o3 = CreateOrder(Customers[2], new FestivalDiscount("Diwali", 0.20m));
            o3.AddItem(Products[3], 3);
            Orders.Add(o3);

            o1.OrderDate = DateTime.Now.AddDays(-3);
            o2.OrderDate = DateTime.Now.AddDays(-10);
        }

        private static void AddProduct(Product product)
        {
            Products.Add(product);
            ProductDictionary[product.Id] = product;
        }

        public static Order CreateOrder(Customer customer, IDiscountStrategy discountStrategy)
        {
            if (customer.IsBlacklisted)
            {
                throw new CustomerBlacklistedException($"Customer {customer.Name} is blacklisted.");
            }

            var order = new Order(_nextOrderId++, customer, discountStrategy);
            return order;
        }

        public static IEnumerable<Order> GetOrdersLast7Days()
        {
            var from = DateTime.Now.AddDays(-7);
            return Orders.Where(o => o.OrderDate >= from);
        }

        public static decimal GetTotalRevenue()
        {
            return Orders
                .Where(o => o.Status != OrderStatus.Cancelled)
                .Sum(o => o.GetTotal());
        }

        public static Product GetMostSoldProduct()
        {
            var group = Orders
                .Where(o => o.Status != OrderStatus.Cancelled)
                .SelectMany(o => o.Items)
                .GroupBy(i => i.Product)
                .Select(g => new { Product = g.Key, Quantity = g.Sum(i => i.Quantity) })
                .OrderByDescending(x => x.Quantity)
                .FirstOrDefault();

            return group?.Product;
        }

        public static IEnumerable<(Customer Customer, decimal TotalSpent)> GetTopCustomersBySpending(int count)
        {
            var query = Orders
                .Where(o => o.Status != OrderStatus.Cancelled)
                .GroupBy(o => o.Customer)
                .Select(g => new
                {
                    Customer = g.Key,
                    Total = g.Sum(o => o.GetTotal())
                })
                .OrderByDescending(x => x.Total)
                .Take(count)
                .Select(x => (x.Customer, x.Total));

            return query;
        }

        public static ILookup<OrderStatus, Order> GroupOrdersByStatus()
        {
            return Orders.ToLookup(o => o.Status);
        }

        public static IEnumerable<Product> GetLowStockProducts(int threshold)
        {
            return Products.Where(p => p.Stock < threshold);
        }
    }

    public class Program
    {
        public static void Main()
        {
            ECommerceStore.Seed();
            RunMenu();
        }

        private static void RunMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=== E-Commerce Order Management System ===");
                Console.WriteLine("1. List products");
                Console.WriteLine("2. List customers");
                Console.WriteLine("3. List orders");
                Console.WriteLine("4. Place new order");
                Console.WriteLine("5. Cancel order");
                Console.WriteLine("6. Orders in last 7 days");
                Console.WriteLine("7. Total revenue");
                Console.WriteLine("8. Most sold product");
                Console.WriteLine("9. Top 5 customers by spending");
                Console.WriteLine("10. Group orders by status");
                Console.WriteLine("11. Products with stock < 10");
                Console.WriteLine("0. Exit");
                Console.Write("Choose option: ");

                var choice = Console.ReadLine();
                Console.WriteLine();

                if (choice == "0")
                {
                    break;
                }

                try
                {
                    switch (choice)
                    {
                        case "1":
                            ListProducts();
                            break;
                        case "2":
                            ListCustomers();
                            break;
                        case "3":
                            ListOrders();
                            break;
                        case "4":
                            PlaceNewOrder();
                            break;
                        case "5":
                            CancelOrder();
                            break;
                        case "6":
                            ShowOrdersLast7Days();
                            break;
                        case "7":
                            ShowTotalRevenue();
                            break;
                        case "8":
                            ShowMostSoldProduct();
                            break;
                        case "9":
                            ShowTopCustomers();
                            break;
                        case "10":
                            ShowOrdersGroupedByStatus();
                            break;
                        case "11":
                            ShowLowStockProducts();
                            break;
                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private static void ListProducts()
        {
            foreach (var p in ECommerceStore.Products)
            {
                Console.WriteLine(p);
            }
        }

        private static void ListCustomers()
        {
            foreach (var c in ECommerceStore.Customers)
            {
                Console.WriteLine(c);
            }
        }

        private static void ListOrders()
        {
            foreach (var o in ECommerceStore.Orders)
            {
                Console.WriteLine(o);
            }
        }

        private static Customer PromptCustomer()
        {
            Console.Write("Enter customer id: ");
            int id = int.Parse(Console.ReadLine() ?? "0");
            var customer = ECommerceStore.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                throw new Exception("Customer not found.");
            }

            return customer;
        }

        private static Product PromptProduct()
        {
            Console.Write("Enter product id: ");
            int id = int.Parse(Console.ReadLine() ?? "0");
            if (!ECommerceStore.ProductDictionary.TryGetValue(id, out var product))
            {
                throw new Exception("Product not found.");
            }

            return product;
        }

        private static int PromptQuantity()
        {
            Console.Write("Enter quantity: ");
            int qty = int.Parse(Console.ReadLine() ?? "0");
            return qty;
        }

        private static IDiscountStrategy PromptDiscount()
        {
            Console.WriteLine("Discount type:");
            Console.WriteLine("1. No discount");
            Console.WriteLine("2. Percentage discount");
            Console.WriteLine("3. Flat discount");
            Console.WriteLine("4. Festival discount");
            Console.Write("Choose: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    return new NoDiscount();
                case "2":
                    Console.Write("Enter percentage (e.g. 10 for 10%): ");
                    decimal p = decimal.Parse(Console.ReadLine() ?? "0") / 100m;
                    return new PercentageDiscount(p);
                case "3":
                    Console.Write("Enter flat amount: ");
                    decimal a = decimal.Parse(Console.ReadLine() ?? "0");
                    return new FlatDiscount(a);
                case "4":
                    Console.Write("Enter festival name: ");
                    string name = Console.ReadLine() ?? "Festival";
                    Console.Write("Enter percentage (e.g. 20 for 20%): ");
                    decimal fp = decimal.Parse(Console.ReadLine() ?? "0") / 100m;
                    return new FestivalDiscount(name, fp);
                default:
                    return new NoDiscount();
            }
        }

        private static void PlaceNewOrder()
        {
            var customer = PromptCustomer();
            var discount = PromptDiscount();

            var order = ECommerceStore.CreateOrder(customer, discount);

            while (true)
            {
                Console.Write("Add product? (y/n): ");
                var ans = Console.ReadLine();
                if (!string.Equals(ans, "y", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                var product = PromptProduct();
                var qty = PromptQuantity();
                order.AddItem(product, qty);
            }

            if (!order.Items.Any())
            {
                Console.WriteLine("Order has no items.");
                return;
            }

            ECommerceStore.Orders.Add(order);
            Console.WriteLine($"Order placed. Id = {order.OrderId}, Total = {order.GetTotal():C}");
        }

        private static void CancelOrder()
        {
            Console.Write("Enter order id to cancel: ");
            int id = int.Parse(Console.ReadLine() ?? "0");
            var order = ECommerceStore.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            order.Cancel();
            Console.WriteLine("Order cancelled.");
        }

        private static void ShowOrdersLast7Days()
        {
            Console.WriteLine("Orders in last 7 days:");
            foreach (var o in ECommerceStore.GetOrdersLast7Days())
            {
                Console.WriteLine(o);
            }
        }

        private static void ShowTotalRevenue()
        {
            Console.WriteLine("Total revenue: " + ECommerceStore.GetTotalRevenue().ToString("C"));
        }

        private static void ShowMostSoldProduct()
        {
            var product = ECommerceStore.GetMostSoldProduct();
            if (product == null)
            {
                Console.WriteLine("No orders yet.");
            }
            else
            {
                Console.WriteLine("Most sold product: " + product.Name);
            }
        }

        private static void ShowTopCustomers()
        {
            Console.WriteLine("Top 5 customers:");
            foreach (var (customer, total) in ECommerceStore.GetTopCustomersBySpending(5))
            {
                Console.WriteLine($"{customer.Name} - {total:C}");
            }
        }

        private static void ShowOrdersGroupedByStatus()
        {
            Console.WriteLine("Orders by status:");
            var groups = ECommerceStore.GroupOrdersByStatus();
            foreach (var group in groups)
            {
                Console.WriteLine(group.Key + ":");
                foreach (var order in group)
                {
                    Console.WriteLine("  " + order);
                }
            }
        }

        private static void ShowLowStockProducts()
        {
            Console.WriteLine("Products with stock < 10:");
            foreach (var p in ECommerceStore.GetLowStockProducts(10))
            {
                Console.WriteLine(p);
            }
        }
    }
}