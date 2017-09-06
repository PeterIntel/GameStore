using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace GameStore.Web.Attributes
{
    public class ValidateBirthDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = value != null && (DateTime)value <= HttpContext.Current.Timestamp.ToLocalTime() //TODO Consider: remove useless 'else'
                ? ValidationResult.Success
                : new ValidationResult("Birth date must be less than the current date.");
            return result;
        }
    }
}