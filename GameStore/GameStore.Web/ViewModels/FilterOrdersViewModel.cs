using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.App_LocalResources;

namespace GameStore.Web.ViewModels
{
    public class FilterOrdersViewModel : IValidatableObject
    {
        [DataType(DataType.Date)]
        public DateTime? DateFrom { set; get; }

        [DataType(DataType.Date)]
        public DateTime? DateTo { set; get; }

        public IEnumerable<OrderViewModel> Orders { set; get; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateTo != null && DateFrom != null && DateFrom > DateTo)
            {
                yield return new ValidationResult(errorMessage: Resources.DateError);
            }
        }
    }
}