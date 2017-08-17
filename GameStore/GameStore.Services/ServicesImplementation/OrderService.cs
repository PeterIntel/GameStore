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
using GameStore.Logging.Loggers;
using GameStore.Services.ServicesImplementation.FilterImplementation.OrderFilter;

namespace GameStore.Services.ServicesImplementation
{
    public class OrderService : BasicService<Order>, IOrderService
    {
        private readonly IOrderDecoratorRepository _orderRepository;
        private readonly IGenericDecoratorRepository<OrderDetailsEntity, MongoOrderDetailsEntity, OrderDetails> _orderDetailsRepository;
        private readonly IGenericDecoratorRepository<GameEntity, MongoProductEntity, Game> _gameRepository;
        private readonly IMongoLogger<Order> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork, IOrderDecoratorRepository orderRepository, IGenericDecoratorRepository<GameEntity, MongoProductEntity, Game> gameRepository, IGenericDecoratorRepository<OrderDetailsEntity, MongoOrderDetailsEntity, OrderDetails> orderDetailsRepository, IMongoLogger<Order> logger)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _gameRepository = gameRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _logger = logger;
        }

        public void AddGameToOrder(string gamekey, string customerId)
        {
            if (customerId == null)
            {
                throw new ArgumentNullException(nameof(customerId) + " references to NULL");
            }

            var game = _gameRepository.First(x => x.Key == gamekey);
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
                    Id = Guid.NewGuid().ToString(),
                    OrderId = order.Id,
                    GameId = game.Id,
                    Quantity = 1,
                    Price = game.Price
                });
            }

            _unitOfWork.Save();
            _logger.Write(Operation.Insert, order);
        }

        public void Add(Order item)
        {
            _orderRepository.Add(item);
            _unitOfWork.Save();
            _logger.Write(Operation.Insert, item);
        }

        public Order GetOrderByCustomerId(string id)
        {
            Order order = _orderRepository.Get(x => x.CustomerId == id && x.Status == CompletionStatus.InComplete).FirstOrDefault();

            if (order == null)
            {
                Add(new Order()
                {
                    Id = Guid.NewGuid().ToString(),
                    Status = CompletionStatus.InComplete,
                    CustomerId = id,
                    OrderDate = DateTime.UtcNow,
                });

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
            _logger.Write(Operation.Delete, item);
        }

        public void Update(Order item)
        {
            _orderRepository.Update(item);
            _unitOfWork.Save();
            var updatedOrder = _orderRepository.GetItemById(item.Id);
            _logger.Write(Operation.Update, item, updatedOrder);
        }

        public IEnumerable<Order> Get(Expression<Func<Order, bool>> filter, params Expression<Func<Order, object>>[] includeProperties) 
             
        {
            var orders = _orderRepository.Get(filter, includeProperties).ToList();
            return orders;
        }

        public IEnumerable<Order> Get(params Expression<Func<Order, object>>[] includeProperties)
        {
            var orders = _orderRepository.Get(includeProperties).ToList();
            return orders;
        }

        public IEnumerable<Order> GetOrdersHistory(FilterOrders filter, params Expression<Func<Order, object>>[] includeProperties)
        {
            OrderPipeLine pipeline = new OrderPipeLine();
            var filterExpression = pipeline.ApplyFilters(filter);
            var orders = _orderRepository.GetOrders(filterExpression, includeProperties);
            return orders;
        }

        public IEnumerable<Order> GetOrdersHistory(params Expression<Func<Order, object>>[] includeProperties)
        {
            var orders = _orderRepository.GetOrders(x => true, includeProperties).ToList();
            return orders;
        }
    }
}
