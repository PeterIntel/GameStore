using GameStore.Domain.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GameStore.Domain.ServicesInterfaces
{
    public interface IOrderService : ICrudService<Order>
    {
        IEnumerable<Order> GetAll(Expression<Func<Order, bool>> filter, params Expression<Func<Order, object>>[] includeProperties);
        IEnumerable<Order> GetAll(params Expression<Func<Order, object>>[] includeProperties);
        Order GetItemById(int id);
        Order GetItemByCustomerId(int id);
    }
}
