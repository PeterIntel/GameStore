using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Domain.BusinessObjects;
using GameStore.Web.Attributes;
using GameStore.Web.App_LocalResources;

namespace GameStore.Web.ViewModels
{
    public class FilterCriteriaViewModel : IValidatableObject
    {
        [Display(Name = "Genres", ResourceType = typeof(Resources))]
        public IList<GenreViewModel> Genres { set; get; }
        public IList<string> NameGenres { set; get; } 
        [Display(Name = "PlatformTypes", ResourceType = typeof(Resources))]
        public IList<PlatformTypeViewModel> PlatformTypes { set; get; }
        public IList<string> NamePlatformTypes { set; get; }
        [Display(Name = "Publishers", ResourceType = typeof(Resources))]
        public IList<PublisherViewModel> Publishers { set; get; }
        public IList<string> NamePublishers { set; get; }
        [Display(Name = "OrderBy", ResourceType = typeof(Resources))]
        public SortCriteria SortCriteria { set; get; }
        [Display(Name = "PriceRange", ResourceType = typeof(Resources))]
        [MinValue(minValue: 0, ErrorMessageResourceName = "PriceLessThanZero", ErrorMessageResourceType = typeof(Resources))]
        public decimal? PriceFrom { set; get; }
        [MinValue(minValue: 0, ErrorMessageResourceName = "PriceLessThanZero", ErrorMessageResourceType = typeof(Resources))]
        public decimal? PriceTo { set; get; }
        [Display(Name = "DatePrecision", ResourceType = typeof(Resources))]
        public DateTimeIntervals DateTimeIntervals { set; get; }
        [StringLength(100,MinimumLength = 3, ErrorMessageResourceName = "PartNameError", ErrorMessageResourceType = typeof(Resources))]
        public string GameName { set; get; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PriceTo != 0 && PriceFrom != 0 && PriceFrom > PriceTo)
            {
                yield return new ValidationResult(errorMessage: Resources.ResourceManager.GetString("PriceRangeError"));
            }
        }
    }
}