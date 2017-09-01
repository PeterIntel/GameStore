using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.Interfaces
{
    public interface IDecoratorOrderRepository : IGenericDataRepository<OrderEntity, Order>
    {
        IEnumerable<Order> GetOrders(Expression<Func<Order, bool>> filter, params Expression<Func<Order, object>>[] includeProperties);
        void DeleteGameFromOrder(string orderId, string orderDetailsId);
    }
}
