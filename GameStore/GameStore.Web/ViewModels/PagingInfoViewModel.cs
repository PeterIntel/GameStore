using System;
using System.Collections.Generic;

namespace GameStore.Web.ViewModels
{
    public class PagingInfoViewModel
    {
        public int TotalItems { set; get; }
        public int? ItemsPerPage { set; get; }
        public int CurrentPage { set; get; }

        public IDictionary<int, string> QuantityItems => new Dictionary<int, string>
        {
            { 10, "10"}, { 20, "20"}, { 50, "50"}, { 100, "100"}
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