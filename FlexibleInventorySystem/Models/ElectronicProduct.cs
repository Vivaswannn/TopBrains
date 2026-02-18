using System;

namespace FlexibleInventorySystem.Models
{
    public class ElectronicProduct : Product
    {
        public string Brand { get; set; }
        public int WarrantyMonths { get; set; }
        public string Voltage { get; set; }
        public bool IsRefurbished { get; set; }

        public override string GetProductDetails()
        {
            return $"Brand: {Brand}, Model: {Name}, Warranty: {WarrantyMonths} months";
        }

        public DateTime GetWarrantyExpiryDate()
        {
            return DateAdded.AddMonths(WarrantyMonths);
        }

        public bool IsWarrantyValid()
        {
            return GetWarrantyExpiryDate() > DateTime.Now;
        }
    }
}
