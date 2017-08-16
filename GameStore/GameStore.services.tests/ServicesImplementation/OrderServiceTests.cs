using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.DataAccess.Decorators;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Services.ServicesImplementation;
using Moq;
using NUnit.Framework;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.Logging.Loggers;

namespace GameStore.Services.Tests.ServicesImplementation
{
    [TestFixture]
    class OrderServiceTests
    {
        private OrderService _sut;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IOrderDecoratorRepository> _orderRepository;
        private Mock<IGenericDecoratorRepository<OrderDetailsEntity, MongoOrderDetailsEntity, OrderDetails>> _orderDetailsRepository;
        private Mock<IGenericDecoratorRepository<GameEntity, MongoProductEntity, Game>> _gameRepository;
        private Mock<IMongoLogger<Order>> _logger;
        private static string _gameKeyFirst = "game";
        private static string _gameKeySecond = "game2";
        private static string _customerId = "1";
        private static Game _game = new Game() { Id = "1", Key = "game" , Price = 120};
        private static OrderDetails _orderDetails = new OrderDetails() {Id = "1", Quantity = 2, OrderId = "1", Game = _game, Price = _game.Price * 2};
        private  Order _order = new Order() {Id = "1", OrderDetails = new List<OrderDetails>() {_orderDetails}};

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _orderRepository = new Mock<IOrderDecoratorRepository>();
            _orderDetailsRepository = new Mock<IGenericDecoratorRepository<OrderDetailsEntity, MongoOrderDetailsEntity, OrderDetails>>();
            _gameRepository = new Mock<IGenericDecoratorRepository<GameEntity, MongoProductEntity, Game>>();
            _logger = new Mock<IMongoLogger<Order>>();
            _sut = new OrderService(_unitOfWork.Object, _orderRepository.Object, _gameRepository.Object, _orderDetailsRepository.Object, _logger.Object);

        }

        [Test]
        public void AddGameToOrder_CustomerNULL_ReturnException()
        {
            Assert.Catch(() => _sut.AddGameToOrder(_gameKeyFirst, null));
        }

        [Test]
        public void AddGameToOrders_AddGameToExitingGameDetailsInOrder_IncreaseQuantity()
        {
            _orderRepository.Setup(m => m.Get(It.IsAny<Expression<Func<Order, bool>>>())).Returns(new List<Order>() { _order });
            
            _sut.AddGameToOrder("game", _customerId);

            Assert.AreEqual(3, _orderDetails.Quantity);
        }

        [Test]
        public void AddGameToOrder_AddGameToExitingGameDetailsInOrder_IncreaseCost()
        {
            _orderRepository.Setup(m => m.Get(It.IsAny<Expression<Func<Order, bool>>>())).Returns(new List<Order>() { _order });

            _sut.AddGameToOrder("game", _customerId);

            Assert.AreEqual(360, _orderDetails.Price);
        }

        [Test]
        public void AddGameToOrder_AddNewGameToExitingOrder_QuantityOfDistinctGames()
        {
            _orderRepository.Setup(m => m.Get(It.IsAny<Expression<Func<Order, bool>>>())).Returns(new List<Order>() { _order});
            _gameRepository.Setup(m => m.First(It.IsAny<Expression<Func<Game, bool>>>())).Returns(new Game());
            _orderDetailsRepository.Setup(m => m.Add(It.IsAny<OrderDetails>())).Callback(() => _order.OrderDetails.Add(It.IsAny<OrderDetails>()));

            _sut.AddGameToOrder(It.IsAny<string>(), _customerId);

            Assert.AreEqual(2, _order.OrderDetails.Count);
        }

        [Test]
        public void GetOrderByCustomerId_GiveInvalidId_ThrowException()
        {
            _orderRepository.Setup(m => m.Get(It.IsAny<Expression<Func<Order, bool>>>())).Throws(new ArgumentException());

            Assert.Catch(() => _sut.GetOrderByCustomerId(It.IsAny<string>()));
        }
    }
}
