using System;
using System.Collections.Generic;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.MSSQL.Entities
{
    public class OrderEntity : BasicEntity
    {
        public string CustomerId { set; get; }
        public DateTime OrderDate { set; get; }
        public CompletionStatus Status { set; get; }
        public virtual IList<OrderDetailsEntity> OrderDetails { set; get; }
        public virtual UserEntity Customer { set; get; }
    }
}
