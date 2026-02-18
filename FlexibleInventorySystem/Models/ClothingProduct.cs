using System;

namespace FlexibleInventorySystem.Models
{
    public class ClothingProduct : Product
    {
        public string Size { get; set; }
        public string Color { get; set; }
        public string Material { get; set; }
        public string Gender { get; set; }
        public string Season { get; set; }

        public override string GetProductDetails()
        {
            return $"Size: {Size}, Color: {Color}, Material: {Material}, Gender: {Gender}, Season: {Season}";
        }

        public bool IsValidSize()
        {
            string[] validSizes = { "XS", "S", "M", "L", "XL", "XXL" };
            return Array.Exists(validSizes, s => s.Equals(Size, StringComparison.OrdinalIgnoreCase));
        }

        public override decimal CalculateValue()
        {
            decimal baseValue = Price * Quantity;
            string currentSeason = GetCurrentSeason();
            if (Season != currentSeason && Season != "All-season")
            {
                return baseValue * 0.85m;
            }
            return baseValue;
        }

        private string GetCurrentSeason()
        {
            int month = DateTime.Now.Month;
            if (month >= 3 && month <= 5) return "Summer";
            if (month >= 6 && month <= 8) return "Summer";
            if (month >= 9 && month <= 11) return "Winter";
            return "Winter";
        }
    }
}
