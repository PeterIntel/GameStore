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

        public void AddGameToOrder(string gamekey, int customerId)
        {
            var game = _unitOfWork.GameRepository.GetAll(x => x.Key == gamekey).First();
            var order = _unitOfWork.OrderRepository.GetAll(x => x.CustomerId == customerId && x.Status == CompletionStatus.InComplete).FirstOrDefault();
            if (order != null)
            {
                var gameDetails = order.OrderDetails.FirstOrDefault(x => string.Equals(x.Game.Key, gamekey, StringComparison.OrdinalIgnoreCase));

                if (gameDetails != null)
                {
                    gameDetails.Quantity++;
                    gameDetails.Price = gameDetails.Quantity * gameDetails.Game.Price;
                    gameDetails.Order = null;
                    gameDetails.Game = null;
                    _unitOfWork.OrderDetailsRepository.Update(gameDetails);
                }
                else
                {
                    _unitOfWork.OrderDetailsRepository.Add(new OrderDetails()
                    {
                        OrderId = GetOrderByCustomerId(customerId).Id,
                        GameId = game.Id,
                        Quantity = 1,
                        Price = game.Price
                    });
                }
            }
            else
            {
                _unitOfWork.OrderRepository.Add(new Order()
                {
                    CustomerId = customerId,
                    OrderDate = DateTime.UtcNow,
                    Status = CompletionStatus.InComplete
                });
                _unitOfWork.Save();

                _unitOfWork.OrderDetailsRepository.Add(new OrderDetails()
                {
                    OrderId = GetOrderByCustomerId(customerId).Id,
                    GameId = game.Id,
                    Quantity = 1,
                    Price = game.Price
                });
            }
            _unitOfWork.Save();
        }

        public void Add(Order item)
        {
            _unitOfWork.OrderRepository.Add(item);
            _unitOfWork.Save();
        }

        public IEnumerable<Order> GetAll(params Expression<Func<Order, object>>[] includeProperties)
        {
            var result = _unitOfWork.OrderRepository.GetAll(includeProperties);
            return result;
        }

        public IEnumerable<Order> GetAll(Expression<Func<Order, bool>> filter, params Expression<Func<Order, object>>[] includeProperties)
        {
            var result = _unitOfWork.OrderRepository.GetAll(filter, includeProperties);
            return result;
        }

        public Order GetOrderByCustomerId(int id)
        {
            Order order;
            try
            {
                order = _unitOfWork.OrderRepository.GetAll(x => x.CustomerId == id && x.Status == CompletionStatus.InComplete).First();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"There is not open order for {id}", ex);
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
