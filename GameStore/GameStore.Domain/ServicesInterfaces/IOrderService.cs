using GameStore.Domain.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GameStore.Domain.ServicesInterfaces
{
    public interface IOrderService : ICrudService<Order>
    {
        IEnumerable<Order> Get(Expression<Func<Order, bool>> filter, params Expression<Func<Order, object>>[] includeProperties);
        Order GetOrderByCustomerId(string id);
        void AddGameToOrder(string gamekey, string customerId);
    }
}
