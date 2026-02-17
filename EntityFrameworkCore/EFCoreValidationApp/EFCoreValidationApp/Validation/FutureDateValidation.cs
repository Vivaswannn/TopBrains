using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EFCoreValidationApp.Validation
{
    public class FutureDateValidation: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                if (dateValue > DateTime.Now)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("The date must be in the future.");
                }
            }
            return new ValidationResult("Invalid date format.");
        }
    }
}
