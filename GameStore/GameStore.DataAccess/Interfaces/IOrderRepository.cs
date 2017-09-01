using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.Interfaces
{
    public interface IOrderRepository : IGenericDataRepository<OrderEntity, Order>
    {
        void DeleteGameFromOrder(string orderId, string orderDetailsId);
    }
}
