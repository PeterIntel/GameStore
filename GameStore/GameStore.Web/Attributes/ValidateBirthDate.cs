using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using GameStore.Web.App_LocalResources;

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
            else
            {
                return new ValidationResult(Resources.ErrorBirthDay);
            }
        }
    }
}