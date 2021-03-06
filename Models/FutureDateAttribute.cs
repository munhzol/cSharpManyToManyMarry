using System;
using System.ComponentModel.DataAnnotations;

namespace LoginRegTest.Models
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if((DateTime)value < DateTime.Now)
            {
                return new ValidationResult("Enter future day only!");
            }
            return ValidationResult.Success;
        }
    }
}