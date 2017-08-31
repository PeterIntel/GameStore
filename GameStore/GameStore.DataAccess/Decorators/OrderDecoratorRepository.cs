using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.Decorators
{
    public class DecoratorOrderDecoratorRepository : GenericDecoratorRepository<OrderEntity, MongoOrderEntity, Order>, IDecoratorOrderRepository
    {
        private readonly IOrderRepository _orderRepository;
        public DecoratorOrderDecoratorRepository(IOrderRepository sqlDataRepository, IReadOnlyGenericRepository<MongoOrderEntity, Order> mongoDataRepository) : base(sqlDataRepository, mongoDataRepository)
        {
            _orderRepository = sqlDataRepository;
        }

        public void DeleteGameFromOrder(string orderId, string orderDetailsId)
        {
            _orderRepository.DeleteGameFromOrder(orderId, orderDetailsId);
        }

        public IEnumerable<Order> GetOrders(Expression<Func<Order, bool>> filter, params Expression<Func<Order, object>>[] includeProperties)
        {
            return base.Get(filter, includeProperties);
        }

        public override Order GetItemById(string id)
        {
            var order = SqlDataRepository.GetItemById(id);
            return order;
        }

        public override Order First(Expression<Func<Order, bool>> filter)
        {
            var order = SqlDataRepository.First(filter);

            return order;
        }

        public override int GetCountObject(Expression<Func<Order, bool>> filter)
        {
            return SqlDataRepository.GetCountObject(filter);
        }

        public override IEnumerable<Order> Get(params Expression<Func<Order, object>>[] includeProperties)
        {
            var orders = SqlDataRepository.Get(includeProperties);
            return orders;
        }

        public override IEnumerable<Order> Get(Expression<Func<Order, bool>> filter, params Expression<Func<Order, object>>[] includeProperties)
        {
            var orders = SqlDataRepository.Get(filter, includeProperties);
            return orders;
        }
    }
}
