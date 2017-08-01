using System;
using System.Collections.Generic;
using GameStore.Domain.BusinessObjects;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels
{
    public class OrderViewModel
    {
        public int Id { set; get; }
        public int CustomerId { set; get; }
        public DateTime OrderDate { set; get; }
        public CompletionStatus Status { set; get; }
        public IList<OrderDetailsViewModel> OrderDetails { set; get; }
    }
}