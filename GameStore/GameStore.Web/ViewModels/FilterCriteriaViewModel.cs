using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameStore.Domain.BusinessObjects;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.ViewModels
{
    public class FilterCriteriaViewModel
    {
        public IList<GenreViewModel> Genres { set; get; }
        public string[] NameGenres { set; get; }
        public IList<PlatformTypeViewModel> PlatformTypes { set; get; }
        public IList<string> NamePlatformTypes { set; get; }
        public IList<PublisherViewModel> Publishers { set; get; }
        public IList<string> NamePublishers { set; get; }
        [Display(Name = "Sort by")]
        public SortCriteria SortCriteria { set; get; }
        [Display(Name = "Price Range")]
        public decimal PriceFrom { set; get; }
        public decimal PriceTo { set; get; }
        [Display(Name = "Filter by published date")]
        public PublishedDateCriteria PublishedDateCriteria { set; get; }
        public string GameName { set; get; }
    }
}