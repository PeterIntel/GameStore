using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels
{
    public class OrderDetailsViewModel
    {
        public string Id { set; get; }
        public string GameId { set; get; }
        public string OrderId { set; get; }
        public decimal Price { set; get; }
        public short Quantity { set; get; }
        public double Discount { set; get; }
        public GameViewModel Game { set; get; }
        public OrderViewModel Order { set; get; }
    }
}