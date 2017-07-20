using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.DataAccess.UnitOfWork;

namespace GameStore.Services.ServicesImplementation
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(Order item)
        {
            _unitOfWork.OrderRepository.Add(item);
            _unitOfWork.Save();
        }

        public IEnumerable<Order> GetAll(params Expression<Func<Order, object>>[] includeProperties)
        {
            var result = _unitOfWork.OrderRepository.GetAll();
            return result;
        }

        public IEnumerable<Order> GetAll(Expression<Func<Order, bool>> filter, params Expression<Func<Order, object>>[] includeProperties)
        {
            var result = _unitOfWork.OrderRepository.GetAll(filter, includeProperties);
            return result;
        }

        public Order GetItemByCustomerId(int id)
        {
            Order order;
            try
            {
                order = _unitOfWork.OrderRepository.GetAll(x => x.CustomerId == id).First();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Customer can not be find with id {id}", ex);
            }
            return order;
        }

        public Order GetItemById(int id)
        {
            var result = _unitOfWork.OrderRepository.GetItemById(id);
            return result;
        }

        public void Remove(int id)
        {
            _unitOfWork.OrderRepository.Remove(id);
            _unitOfWork.Save();
        }

        public void Remove(Order item)
        {
            _unitOfWork.OrderRepository.Remove(item);
            _unitOfWork.Save();
        }

        public void Update(Order item)
        {
            _unitOfWork.OrderRepository.Update(item);
            _unitOfWork.Save();
        }
    }
}
