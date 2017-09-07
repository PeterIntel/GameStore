using System.Data.Entity;
using System.Linq;
using AutoMapper;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.MSSQL.Repositories
{
    public class OrderRepository : GenericDataRepository<OrderEntity, Order>, IOrderRepository
    {
        public OrderRepository(GamesSqlContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public void DeleteGameFromOrder(string orderId, string orderDetailsId)
        {
            var order = _dbSet.First(x => x.Id == orderId);
            var orderDetails = _context.OrderDetails.First(x => x.Id == orderDetailsId);
            order.OrderDetails.Remove(orderDetails);
        }

        public override void Update(Order order)
        {
            var orderEntity = _dbSet.Find(order.Id);
            if (orderEntity != null)
            {
                orderEntity.Status = order.Status;
                _context.Entry(orderEntity).State = EntityState.Modified;
            }
        }
    }
}
