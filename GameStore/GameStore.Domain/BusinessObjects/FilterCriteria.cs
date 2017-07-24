using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.BusinessObjects
{
    public class FilterCriteria
    {
        public IEnumerable<Genre> Genres { set; get; }
        public IEnumerable<PlatformType> Platformtypes { set; get; }
        public IEnumerable<Publisher> Publishers { set; get; }
        public SortCriteria SortCriteria { set; get; }
        public decimal PriceFrom { set; get; }
        public decimal PriceTo { set; get; }
        public PublishedDateCriteria PublishedDateCriteria { set; get; }
        public string GameName { set; get; }
    }
}
