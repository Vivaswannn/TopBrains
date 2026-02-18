using System;

namespace FlexibleInventorySystem.Models
{
    public class GroceryProduct : Product
    {
        public DateTime ExpiryDate { get; set; }
        public bool IsPerishable { get; set; }
        public double Weight { get; set; }
        public string StorageTemperature { get; set; }

        public override string GetProductDetails()
        {
            return $"{Name} - Weight: {Weight}kg - Expiry: {ExpiryDate:d} - Storage: {StorageTemperature}";
        }

        public bool IsExpired()
        {
            return ExpiryDate < DateTime.Now;
        }

        public int DaysUntilExpiry()
        {
            return (ExpiryDate - DateTime.Now).Days;
        }

        public override decimal CalculateValue()
        {
            decimal baseValue = Price * Quantity;
            if (DaysUntilExpiry() <= 3 && DaysUntilExpiry() >= 0)
            {
                return baseValue * 0.8m;
            }
            return baseValue;
        }
    }
}
