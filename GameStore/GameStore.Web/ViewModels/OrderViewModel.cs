using System;
using System.Collections.Generic;
using GameStore.Domain.BusinessObjects;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.App_LocalResources;

namespace GameStore.Web.ViewModels
{
    public class OrderViewModel
    {
        public string Id { set; get; }
        public string CustomerId { set; get; }
        [Display(Name = "OrderDate", ResourceType = typeof(Resources))]
        public DateTime OrderDate { set; get; }
        [Display(Name = "OrderStatus", ResourceType = typeof(Resources))]
        public CompletionStatus Status { set; get; }
        public IList<OrderDetailsViewModel> OrderDetails { set; get; }
    }
}