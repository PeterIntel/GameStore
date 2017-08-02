using System;
using System.Collections.Generic;

namespace GameStore.Web.ViewModels
{
    public class PagingInfoViewModel
    {
        public int TotalItems { set; get; }
        public int? ItemsPerPage { set; get; }
        public int CurrentPage { set; get; }

        public IList<string> QuantityItems => new List<string>
        {
            "10", "20", "50", "100", "ALL"
        };

        public int? TotalPages
        {
            get
            {
                if (ItemsPerPage != null)
                {
                    return (int)Math.Ceiling((decimal) TotalItems / (int)ItemsPerPage);
                }
                return null;
            }
        }
        
    }
}