using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace GameStore.Web.Attributes
{
    public class ValidateBirthDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((DateTime)value <= HttpContext.Current.Timestamp.ToLocalTime())
            {
                return ValidationResult.Success;
            }
            else //TODO Consider: remove useless 'else'
            {
                return new ValidationResult("Birth date must be less than the current date.");
            }
        }
    }
}