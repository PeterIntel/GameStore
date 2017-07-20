using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.Entities
{
    public class OrderEntity : BasicEntity
    {
        public int Id { set; get; }
        public int CustomerId { set; get; }
        public DateTime OrderDate { set; get; }
        public CompletionStatus Status { set; get; }
        public virtual IList<OrderDetailsEntity> OrderDetails { set; get; }
    }
}
