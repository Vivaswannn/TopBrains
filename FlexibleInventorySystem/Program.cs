using System;
using FlexibleInventorySystem.Services;
using FlexibleInventorySystem.Models;

namespace FlexibleInventorySystem
{
    class Program
    {
        private static InventoryManager _inventory = new InventoryManager();

        static void Main(string[] args)
        {
            SeedSampleData();

            while (true)
            {
                DisplayMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProductMenu();
                        break;
                    case "2":
                        RemoveProductMenu();
                        break;
                    case "3":
                        UpdateQuantityMenu();
                        break;
                    case "4":
                        FindProductMenu();
                        break;
                    case "5":
                        ViewAllProducts();
                        break;
                    case "6":
                        GenerateReportsMenu();
                        break;
                    case "7":
                        CheckLowStock();
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Flexible Inventory Management System ===");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Remove Product");
            Console.WriteLine("3. Update Quantity");
            Console.WriteLine("4. Find Product");
            Console.WriteLine("5. View All Products");
            Console.WriteLine("6. Generate Reports");
            Console.WriteLine("7. Check Low Stock");
            Console.WriteLine("8. Exit");
            Console.Write("\nChoose an option: ");
        }

        static void AddProductMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Add Product ===");
            Console.WriteLine("1. Electronic Product");
            Console.WriteLine("2. Grocery Product");
            Console.WriteLine("3. Clothing Product");
            Console.Write("Choose product type: ");

            string type = Console.ReadLine();
            Product product = null;

            try
            {
                switch (type)
                {
                    case "1":
                        product = CreateElectronicProduct();
                        break;
                    case "2":
                        product = CreateGroceryProduct();
                        break;
                    case "3":
                        product = CreateClothingProduct();
                        break;
                    default:
                        Console.WriteLine("Invalid product type.");
                        return;
                }

                if (_inventory.AddProduct(product))
                {
                    Console.WriteLine("Product added successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static ElectronicProduct CreateElectronicProduct()
        {
            var product = new ElectronicProduct();
            Console.Write("Product ID: ");
            product.Id = Console.ReadLine();
            Console.Write("Name: ");
            product.Name = Console.ReadLine();
            Console.Write("Price: ");
            product.Price = decimal.Parse(Console.ReadLine());
            Console.Write("Quantity: ");
            product.Quantity = int.Parse(Console.ReadLine());
            Console.Write("Category: ");
            product.Category = Console.ReadLine();
            Console.Write("Brand: ");
            product.Brand = Console.ReadLine();
            Console.Write("Warranty Months: ");
            product.WarrantyMonths = int.Parse(Console.ReadLine());
            Console.Write("Voltage: ");
            product.Voltage = Console.ReadLine();
            Console.Write("Is Refurbished (true/false): ");
            product.IsRefurbished = bool.Parse(Console.ReadLine());
            return product;
        }

        static GroceryProduct CreateGroceryProduct()
        {
            var product = new GroceryProduct();
            Console.Write("Product ID: ");
            product.Id = Console.ReadLine();
            Console.Write("Name: ");
            product.Name = Console.ReadLine();
            Console.Write("Price: ");
            product.Price = decimal.Parse(Console.ReadLine());
            Console.Write("Quantity: ");
            product.Quantity = int.Parse(Console.ReadLine());
            Console.Write("Category: ");
            product.Category = Console.ReadLine();
            Console.Write("Expiry Date (yyyy-mm-dd): ");
            product.ExpiryDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Is Perishable (true/false): ");
            product.IsPerishable = bool.Parse(Console.ReadLine());
            Console.Write("Weight: ");
            product.Weight = double.Parse(Console.ReadLine());
            Console.Write("Storage Temperature: ");
            product.StorageTemperature = Console.ReadLine();
            return product;
        }

        static ClothingProduct CreateClothingProduct()
        {
            var product = new ClothingProduct();
            Console.Write("Product ID: ");
            product.Id = Console.ReadLine();
            Console.Write("Name: ");
            product.Name = Console.ReadLine();
            Console.Write("Price: ");
            product.Price = decimal.Parse(Console.ReadLine());
            Console.Write("Quantity: ");
            product.Quantity = int.Parse(Console.ReadLine());
            Console.Write("Category: ");
            product.Category = Console.ReadLine();
            Console.Write("Size (XS/S/M/L/XL/XXL): ");
            product.Size = Console.ReadLine();
            Console.Write("Color: ");
            product.Color = Console.ReadLine();
            Console.Write("Material: ");
            product.Material = Console.ReadLine();
            Console.Write("Gender (Men/Women/Unisex): ");
            product.Gender = Console.ReadLine();
            Console.Write("Season (Summer/Winter/All-season): ");
            product.Season = Console.ReadLine();
            return product;
        }

        static void RemoveProductMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Remove Product ===");
            Console.Write("Enter Product ID: ");
            string id = Console.ReadLine();

            if (_inventory.RemoveProduct(id))
            {
                Console.WriteLine("Product removed successfully!");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        static void UpdateQuantityMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Update Quantity ===");
            Console.Write("Enter Product ID: ");
            string id = Console.ReadLine();
            Console.Write("Enter New Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            if (_inventory.UpdateQuantity(id, quantity))
            {
                Console.WriteLine("Quantity updated successfully!");
            }
            else
            {
                Console.WriteLine("Failed to update quantity. Product not found or invalid quantity.");
            }
        }

        static void FindProductMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Find Product ===");
            Console.Write("Enter Product ID: ");
            string id = Console.ReadLine();

            var product = _inventory.FindProduct(id);
            if (product != null)
            {
                Console.WriteLine("\nProduct Found:");
                Console.WriteLine(product);
                Console.WriteLine("Details: " + product.GetProductDetails());
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        static void ViewAllProducts()
        {
            Console.Clear();
            Console.WriteLine("=== All Products ===");
            var products = _inventory.SearchProducts(p => true);
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }

        static void GenerateReportsMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Generate Reports ===");
            Console.WriteLine("1. Inventory Report");
            Console.WriteLine("2. Category Summary");
            Console.WriteLine("3. Value Report");
            Console.WriteLine("4. Expiry Report");
            Console.Write("Choose report type: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine(_inventory.GenerateInventoryReport());
                    break;
                case "2":
                    Console.WriteLine(_inventory.GenerateCategorySummary());
                    break;
                case "3":
                    Console.WriteLine(_inventory.GenerateValueReport());
                    break;
                case "4":
                    Console.Write("Enter days threshold: ");
                    int days = int.Parse(Console.ReadLine());
                    Console.WriteLine(_inventory.GenerateExpiryReport(days));
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        static void CheckLowStock()
        {
            Console.Clear();
            Console.WriteLine("=== Low Stock Products ===");
            Console.Write("Enter threshold: ");
            int threshold = int.Parse(Console.ReadLine());

            var lowStock = _inventory.GetLowStockProducts(threshold);
            if (lowStock.Any())
            {
                foreach (var product in lowStock)
                {
                    Console.WriteLine($"{product.Name} - Quantity: {product.Quantity}");
                }
            }
            else
            {
                Console.WriteLine("No products below threshold.");
            }
        }

        static void SeedSampleData()
        {
            try
            {
                var e1 = new ElectronicProduct
                {
                    Id = "E001",
                    Name = "Laptop",
                    Price = 999.99m,
                    Quantity = 10,
                    Category = "Electronics",
                    Brand = "Dell",
                    WarrantyMonths = 24,
                    Voltage = "110-240V",
                    IsRefurbished = false
                };

                var g1 = new GroceryProduct
                {
                    Id = "G001",
                    Name = "Milk",
                    Price = 3.49m,
                    Quantity = 50,
                    Category = "Groceries",
                    ExpiryDate = DateTime.Now.AddDays(7),
                    IsPerishable = true,
                    Weight = 1.0,
                    StorageTemperature = "Refrigerated"
                };

                var c1 = new ClothingProduct
                {
                    Id = "C001",
                    Name = "T-Shirt",
                    Price = 19.99m,
                    Quantity = 100,
                    Category = "Clothing",
                    Size = "L",
                    Color = "Blue",
                    Material = "Cotton",
                    Gender = "Men",
                    Season = "Summer"
                };

                _inventory.AddProduct(e1);
                _inventory.AddProduct(g1);
                _inventory.AddProduct(c1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding data: {ex.Message}");
            }
        }
    }
}
