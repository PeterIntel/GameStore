using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.BusinessObjects
{
    public class OrderDetails : BasicDomain
    {
        public string GameId { set; get; }
        public string OrderId { set; get; }
        public decimal Price { set; get; }
        public short Quantity { set; get; }
        public double Discount { set; get; }
        public Game Game { set; get; }
        public Order Order { set; get; }
    }
}
