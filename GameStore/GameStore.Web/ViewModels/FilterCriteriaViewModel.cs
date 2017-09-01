using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Domain.BusinessObjects;
using GameStore.Web.Attributes;

namespace GameStore.Web.ViewModels
{
    public class FilterCriteriaViewModel : IValidatableObject
    {
        public IList<GenreViewModel> Genres { set; get; }
        public IList<string> NameGenres { set; get; }
        public IList<PlatformTypeViewModel> PlatformTypes { set; get; }
        public IList<string> NamePlatformTypes { set; get; }
        public IList<PublisherViewModel> Publishers { set; get; }
        public IList<string> NamePublishers { set; get; }
        [Display(Name = "Sort by")]
        public SortCriteria SortCriteria { set; get; }
        [Display(Name = "Price Range")]
        [MinValue(minValue: 0, ErrorMessage = "PriceFrom is less than 0")]
        public decimal? PriceFrom { set; get; }
        [MinValue(minValue: 0, ErrorMessage = "PriceTo is less than 0")]
        public decimal? PriceTo { set; get; }
        [Display(Name = "Filter by published date")]
        public DateTimeIntervals DateTimeIntervals { set; get; }
        [StringLength(100,MinimumLength = 3)]
        public string GameName { set; get; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PriceTo != 0 && PriceFrom != 0 && PriceFrom > PriceTo)
            {
                yield return new ValidationResult(errorMessage: "PriceTo less than PriceFrom");
            }
        }
    }
}