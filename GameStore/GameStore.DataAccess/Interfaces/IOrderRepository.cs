using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.Interfaces
{
    public interface IOrderRepository : IGenericDataRepository<OrderEntity, Order>
    {
        void DeleteGameFromOrder(string orderId, string orderDetailsId);
    }
}
