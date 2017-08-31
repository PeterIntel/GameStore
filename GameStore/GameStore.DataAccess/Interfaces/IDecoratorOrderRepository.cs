using GameStore.Domain.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.MSSQL.Entities;

namespace GameStore.DataAccess.Interfaces
{
    public interface IDecoratorOrderRepository : IGenericDataRepository<OrderEntity, Order>
    {
        IEnumerable<Order> GetOrders(Expression<Func<Order, bool>> filter, params Expression<Func<Order, object>>[] includeProperties);
        void DeleteGameFromOrder(string orderId, string orderDetailsId);
    }
}
