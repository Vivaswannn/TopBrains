using System;

namespace FlexibleInventorySystem.Models
{
    public abstract class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public DateTime DateAdded { get; set; }

        public Product()
        {
            DateAdded = DateTime.Now;
        }

        public abstract string GetProductDetails();

        public virtual decimal CalculateValue()
        {
            return Price * Quantity;
        }

        public override string ToString()
        {
            return $"{Id} - {Name} - Price: {Price:C} - Quantity: {Quantity}";
        }
    }
}
