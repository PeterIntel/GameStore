using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Domain.ServicesInterfaces
{
    public interface IOrderService : ICrudService<Order>
    {
        IEnumerable<Order> GetOrdersHistory(params Expression<Func<Order, object>>[] includeProperties);
        IEnumerable<Order> GetOrdersHistory(FilterOrders filter, params Expression<Func<Order, object>>[] includeProperties);
        IEnumerable<Order> GetCurrentOrders(params Expression<Func<Order, object>>[] includeProperties);
        IEnumerable<Order> GetCurrentOrders(FilterOrders filter, params Expression<Func<Order, object>>[] includeProperties);
        Order GetOrderByCustomerId(string id);
        void AddGameToCustomerOrder(string gamekey, string customerId);
        void AddGameToOrder(string gamekey, string orderId);
        void DeleteGameFromOrder(string orderId, string gamekey);
    }
}
