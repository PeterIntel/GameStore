using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels
{
    public class GamesAndFilterViewModel
    {
        public IList<GameViewModel> Games { set; get; }
        public FilterCriteriaViewModel Filter { set; get; }
        public PagingInfoViewModel PagingInfo { set; get; }
    }
}