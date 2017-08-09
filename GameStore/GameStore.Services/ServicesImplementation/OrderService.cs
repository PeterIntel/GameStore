using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.DataAccess.Decorators;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.DataAccess.UnitOfWork;
namespace GameStore.Services.ServicesImplementation
{
    public class OrderService : IOrderService
    {
        private readonly IGenericDecoratorRepository<OrderEntity, MongoOrderEntity, Order> _orderRepository;
        private readonly IGenericDecoratorRepository<OrderDetailsEntity, MongoOrderDetailsEntity, OrderDetails> _orderDetailsRepository;
        private readonly IGenericDecoratorRepository<GameEntity, MongoProductEntity, Game> _gameRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork, IGenericDecoratorRepository<OrderEntity, MongoOrderEntity, Order> orderRepository, IGenericDecoratorRepository<GameEntity, MongoProductEntity, Game> gameRepository, IGenericDecoratorRepository<OrderDetailsEntity, MongoOrderDetailsEntity, OrderDetails> orderDetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _gameRepository = gameRepository;
            _orderDetailsRepository = orderDetailsRepository;
        }

        public void AddGameToOrder(string gamekey, string customerId)
        {
            if (customerId == null)
            {
                throw new ArgumentNullException(nameof(customerId) + " references to NULL");
            }

            var game = _gameRepository.GetFirst(x => x.Key == gamekey);
            var order = GetOrderByCustomerId(customerId);

            var gameDetails = order.OrderDetails.FirstOrDefault(x => string.Equals(x.Game.Key, gamekey, StringComparison.OrdinalIgnoreCase));

            if (gameDetails != null)
            {
                gameDetails.Quantity++;
                gameDetails.Price = gameDetails.Quantity * gameDetails.Game.Price;
                gameDetails.Order = null;
                gameDetails.Game = null;
                _orderDetailsRepository.Update(gameDetails);
            }
            else
            {
                _orderDetailsRepository.Add(new OrderDetails()
                {
                    OrderId = order.Id,
                    GameId = game.Id,
                    Quantity = 1,
                    Price = game.Price
                });
            }

            _unitOfWork.Save();
        }

        public void Add(Order item)
        {
            _orderRepository.Add(item);
            _unitOfWork.Save();
        }

        public IEnumerable<Order> GetAll(params Expression<Func<Order, object>>[] includeProperties)
        {
            var result = _orderRepository.Get(includeProperties).ToList();
            return result;
        }

        public IEnumerable<Order> GetAll(Expression<Func<Order, bool>> filter, params Expression<Func<Order, object>>[] includeProperties)
        {
            var result = _orderRepository.Get(filter, includeProperties).ToList();
            return result;
        }

        public Order GetOrderByCustomerId(string id)
        {
            Order order = _orderRepository.Get(x => x.CustomerId == id && x.Status == CompletionStatus.InComplete).FirstOrDefault();

            if (order == null)
            {
                _orderRepository.Add(new Order()
                {
                    Status = CompletionStatus.InComplete,
                    CustomerId = id,
                    OrderDate = DateTime.UtcNow,
                });
                _unitOfWork.Save();

                order = _orderRepository.Get(x => x.CustomerId == id && x.Status == CompletionStatus.InComplete).FirstOrDefault();
            }

            return order;
        }

        public Order GetItemById(string id)
        {
            var result = _orderRepository.GetItemById(id);
            return result;
        }

        public void Remove(string id)
        {
            _orderRepository.Remove(id);
            _unitOfWork.Save();
        }

        public void Remove(Order item)
        {
            _orderRepository.Remove(item);
            _unitOfWork.Save();
        }

        public void Update(Order item)
        {
            _orderRepository.Update(item);
            _unitOfWork.Save();
        }

        public IEnumerable<Order> Get(Expression<Func<Order, bool>> filter, params Expression<Func<Order, object>>[] includeProperties) 
             
        {
            var games = _orderRepository.Get(filter, includeProperties).ToList();
            return games;
        }

        public IEnumerable<Order> Get(params Expression<Func<Order, object>>[] includeProperties)
        {
            var games = _orderRepository.Get(includeProperties).ToList();
            return games;
        }
    }
}
