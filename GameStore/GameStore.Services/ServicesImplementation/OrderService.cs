using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Logging.Loggers;
using GameStore.Services.ServicesImplementation.FilterImplementation.OrderFilter;

namespace GameStore.Services.ServicesImplementation
{
    public class OrderService : BasicService<OrderEntity, Order>, IOrderService
    {
        private readonly IDecoratorOrderRepository _decoratorOrderRepository;
        private readonly IGenericDataRepository<OrderDetailsEntity, OrderDetails> _orderDetailsRepository;
        private readonly IGameRepository _gameRepository;
        private readonly TimeSpan _periodDatetime = TimeSpan.FromDays(30);
        private readonly OrderPipeLine _orderPipeLine;

        public OrderService(IUnitOfWork unitOfWork, IDecoratorOrderRepository decoratorOrderRepository, IGameRepository gameRepository, IGenericDataRepository<OrderDetailsEntity, OrderDetails> orderDetailsRepository, IMongoLogger<Order> logger) : base(decoratorOrderRepository, unitOfWork, logger)
        {
            _decoratorOrderRepository = decoratorOrderRepository;
            _gameRepository = gameRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _orderPipeLine = new OrderPipeLine();
        }

        public void AddGameToOrder(string gameId, string orderId)
        {
            var game = _gameRepository.First(x => x.Id == gameId);
            var order = _decoratorOrderRepository.First(x => x.Id == orderId);
            AddGameToOrder(order, game.Key);
        }

        public void AddGameToCustomerOrder(string gamekey, string customerId)
        {
            if (customerId == null)
            {
                throw new ArgumentNullException(nameof(customerId) + " references to NULL");
            }

            var order = GetOrderByCustomerId(customerId);
            AddGameToOrder(order, gamekey);

        }

        public void DeleteGameFromOrder(string gameId, string orderId)
        {
            var order = _decoratorOrderRepository.First(x => x.Id == orderId);
            var game = _gameRepository.First(x => x.Id == gameId);

            var gameDetails = order.OrderDetails.FirstOrDefault(x => string.Equals(x.Game.Id, gameId, StringComparison.OrdinalIgnoreCase));

            if (gameDetails != null)
            {
                if (gameDetails.Quantity > 1)
                {
                    gameDetails.Quantity--;
                    gameDetails.Price = gameDetails.Game.Price;
                    gameDetails.Order = null;
                    gameDetails.Game = null;
                    _orderDetailsRepository.Update(gameDetails);

                    UnitOfWork.Save();
                }
                else
                {
                    _decoratorOrderRepository.DeleteGameFromOrder(order.Id, gameDetails.Id);
                    UnitOfWork.Save();
                }
            }

        }

        private void AddGameToOrder(Order order, string gamekey)
        {
            var game = _gameRepository.First(x => x.Key == gamekey);

            var gameDetails = order.OrderDetails.FirstOrDefault(x => string.Equals(x.Game.Key, gamekey, StringComparison.OrdinalIgnoreCase));

            if (gameDetails != null)
            {
                gameDetails.Quantity++;
                gameDetails.Price = gameDetails.Game.Price;
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

            UnitOfWork.Save();
            Logger.Write(Operation.Insert, order);
        }
        public Order GetOrderByCustomerId(string id)
        {
            Order order = _decoratorOrderRepository.Get(x => x.CustomerId == id && x.Status != CompletionStatus.Paid).FirstOrDefault();

            if (order == null)
            {
                Add(new Order()
                {
                    Id = Guid.NewGuid().ToString(),
                    Status = CompletionStatus.InComplete,
                    CustomerId = id,
                    OrderDate = DateTime.UtcNow,
                });

                order = _decoratorOrderRepository.Get(x => x.CustomerId == id && x.Status != CompletionStatus.Paid).FirstOrDefault();
            }

            return order;
        }

        public Order GetItemById(string id)
        {
            var result = _decoratorOrderRepository.GetItemById(id);
            return result;
        }

        public IEnumerable<Order> GetOrdersHistory(params Expression<Func<Order, object>>[] includeProperties)
        {

            var filterExpression = _orderPipeLine.ApplyFilters(new FilterOrders() { PeriodDate = _periodDatetime, DateTimeIntervalFlag = DateTimeIntervalFlag.BeforeDateTime });
            var orders = _decoratorOrderRepository.GetOrders(filterExpression, includeProperties).ToList();

            return orders;
        }

        public IEnumerable<Order> GetOrdersHistory(FilterOrders filter, params Expression<Func<Order, object>>[] includeProperties)
        {
            filter.PeriodDate = _periodDatetime;
            filter.DateTimeIntervalFlag = DateTimeIntervalFlag.BeforeDateTime;
            var filterExpression = _orderPipeLine.ApplyFilters(filter);
            var orders = _decoratorOrderRepository.GetOrders(filterExpression, includeProperties).ToList();

            return orders;
        }

        public IEnumerable<Order> GetCurrentOrders(params Expression<Func<Order, object>>[] includeProperties)
        {
            var filterExpression = _orderPipeLine.ApplyFilters(new FilterOrders() { PeriodDate = _periodDatetime, DateTimeIntervalFlag = DateTimeIntervalFlag.AfterDateTime });
            var orders = _decoratorOrderRepository.GetOrders(filterExpression, includeProperties).ToList();

            return orders;
        }

        public IEnumerable<Order> GetCurrentOrders(FilterOrders filter, params Expression<Func<Order, object>>[] includeProperties)
        {
            filter.PeriodDate = _periodDatetime;
            filter.DateTimeIntervalFlag = DateTimeIntervalFlag.AfterDateTime;
            var filterExpression = _orderPipeLine.ApplyFilters(filter);
            var orders = _decoratorOrderRepository.GetOrders(filterExpression, includeProperties).ToList();

            return orders;
        }
    }
}
