using FlexibleInventorySystem.Models;

namespace FlexibleInventorySystem.Utilities
{
    public static class ProductValidator
    {
        public static bool ValidateProduct(Product product, out string errorMessage)
        {
            errorMessage = null;

            if (product == null)
            {
                errorMessage = "Product cannot be null.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(product.Id))
            {
                errorMessage = "Product ID cannot be null or empty.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(product.Name))
            {
                errorMessage = "Product name cannot be null or empty.";
                return false;
            }

            if (product.Price <= 0)
            {
                errorMessage = "Product price must be greater than zero.";
                return false;
            }

            if (product.Quantity < 0)
            {
                errorMessage = "Product quantity cannot be negative.";
                return false;
            }

            return true;
        }

        public static bool ValidateElectronicProduct(ElectronicProduct product, out string errorMessage)
        {
            if (!ValidateProduct(product, out errorMessage))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(product.Brand))
            {
                errorMessage = "Brand cannot be null or empty.";
                return false;
            }

            if (product.WarrantyMonths < 0)
            {
                errorMessage = "Warranty months cannot be negative.";
                return false;
            }

            return true;
        }

        public static bool ValidateGroceryProduct(GroceryProduct product, out string errorMessage)
        {
            if (!ValidateProduct(product, out errorMessage))
            {
                return false;
            }

            if (product.ExpiryDate == default(DateTime))
            {
                errorMessage = "Expiry date must be set.";
                return false;
            }

            if (product.Weight <= 0)
            {
                errorMessage = "Weight must be greater than zero.";
                return false;
            }

            return true;
        }

        public static bool ValidateClothingProduct(ClothingProduct product, out string errorMessage)
        {
            if (!ValidateProduct(product, out errorMessage))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(product.Size))
            {
                errorMessage = "Size cannot be null or empty.";
                return false;
            }

            if (!product.IsValidSize())
            {
                errorMessage = "Invalid size. Valid sizes are: XS, S, M, L, XL, XXL.";
                return false;
            }

            return true;
        }
    }
}
