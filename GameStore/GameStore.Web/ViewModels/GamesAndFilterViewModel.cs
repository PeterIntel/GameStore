using System.Collections.Generic;

namespace GameStore.Web.ViewModels
{
    public class GamesAndFilterViewModel
    {
        public IList<GameViewModel> Games { set; get; }
        public FilterCriteriaViewModel Filter { set; get; }
        public PagingInfoViewModel PagingInfo { set; get; }
    }
}