using System;
using System.Collections.Generic;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Web.ViewModels
{
    public class OrderViewModel
    {
        public string Id { set; get; }

        public string CustomerId { set; get; }

        public DateTime OrderDate { set; get; }

        public CompletionStatus Status { set; get; }

        public IList<OrderDetailsViewModel> OrderDetails { set; get; }
    }
}