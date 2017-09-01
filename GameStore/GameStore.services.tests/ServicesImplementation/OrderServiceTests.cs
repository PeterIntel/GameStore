using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Logging.Loggers;
using GameStore.Services.ServicesImplementation;
using Moq;
using NUnit.Framework;

namespace GameStore.Services.Tests.ServicesImplementation
{
    [TestFixture]
    class OrderServiceTests
    {
        private OrderService _sut;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IDecoratorOrderRepository> _orderRepository;
        private Mock<IGenericDataRepository<OrderDetailsEntity, OrderDetails>> _orderDetailsRepository;
        private Mock<IGameRepository> _gameRepository;
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
            _orderRepository = new Mock<IDecoratorOrderRepository>();
            _orderDetailsRepository = new Mock<IGenericDataRepository<OrderDetailsEntity, OrderDetails>>();
            _gameRepository = new Mock<IGameRepository>();
            _logger = new Mock<IMongoLogger<Order>>();
            _sut = new OrderService(_unitOfWork.Object, _orderRepository.Object, _gameRepository.Object, _orderDetailsRepository.Object, _logger.Object);
            _gameRepository.Setup(m => m.First(It.IsAny<Expression<Func<Game, bool>>>())).Returns(_game);

        }

        [Test]
        public void AddGameToOrder_CustomerNULL_ReturnException()
        {
            Assert.Catch(() => _sut.AddGameToOrder(_gameKeyFirst, null));
        }

        [Test]
        public void AddGameToOrders_AddGameToExitingGameDetailsInOrder_IncreaseQuantity()
        {
            _orderRepository.Setup(m => m.First(It.IsAny<Expression<Func<Order, bool>>>())).Returns(_order);
            
            _sut.AddGameToOrder("game", _customerId);

            Assert.AreEqual(3, _orderDetails.Quantity);
        }

        [Test]
        public void AddGameToOrder_AddGameToExitingGameDetailsInOrder_IncreaseCost()
        {
            _orderRepository.Setup(m => m.First(It.IsAny<Expression<Func<Order, bool>>>())).Returns(_order );

            _sut.AddGameToOrder("game", _customerId);

            Assert.AreEqual(120, _orderDetails.Price);
        }

        [Test]
        public void AddGameToOrder_AddNewGameToExitingOrder_QuantityOfDistinctGames()
        {
            _orderRepository.Setup(m => m.First(It.IsAny<Expression<Func<Order, bool>>>())).Returns(_order);
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
