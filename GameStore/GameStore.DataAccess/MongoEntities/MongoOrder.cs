using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataAccess.MongoEntities
{
    public class MongoOrder
    {
        public int CustomerId { set; get; }
        public DateTime OrderDate { set; get; }
        public CompletionStatus Status { set; get; }
        public virtual IList<OrderDetailsEntity> OrderDetails { set; get; }
    }
}
