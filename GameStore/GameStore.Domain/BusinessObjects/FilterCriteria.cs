using System.Collections.Generic;

namespace GameStore.Domain.BusinessObjects
{
    public class FilterCriteria
    {
        public IEnumerable<Genre> Genres { set; get; }
        public IList<string> NameGenres { set; get; }
        public IEnumerable<PlatformType> Platformtypes { set; get; }
        public IList<string> NamePlatformTypes { set; get; }
        public IEnumerable<Publisher> Publishers { set; get; }
        public IList<string> NamePublishers { set; get; }
        public SortCriteria SortCriteria { set; get; }
        public decimal? PriceFrom { set; get; }
        public decimal? PriceTo { set; get; }
        public DateTimeIntervals DateTimeIntervals { set; get; }
        public string GameName { set; get; }
    }
}
