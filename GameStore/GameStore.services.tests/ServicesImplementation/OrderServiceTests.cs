using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.MSSQL.Repositories;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
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
        private Mock<IGenericDataRepository<OrderEntity, Order>> _orderRep;
        private Mock<IGenericDataRepository<OrderDetailsEntity, OrderDetails>> _orderDetalsRep;
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
            _orderRep = new Mock<IGenericDataRepository<OrderEntity, Order>>();
            _orderDetalsRep = new Mock<IGenericDataRepository<OrderDetailsEntity, OrderDetails>>();
            _unitOfWork.Setup(m => m.OrderRepository).Returns(_orderRep.Object);
            _unitOfWork.Setup(m => m.OrderDetailsRepository).Returns(_orderDetalsRep.Object);
            _unitOfWork.Setup(m => m.GameRepository.GetGameByKey("game")).Returns<string>(m =>  _game );
            _sut = new OrderService(_unitOfWork.Object);

        }

        [Test]
        public void AddGameToOrder_CustomerNULL_ReturnException()
        {
            Assert.Catch(() => _sut.AddGameToOrder(_gameKeyFirst, null));
        }

        [Test]
        public void AddGameToOrders_AddGameToExitingGameDetailsInOrder_IncreaseQuantity()
        {
            _unitOfWork.Setup(m => m.OrderRepository.Get(It.IsAny<Expression<Func<Order, bool>>>())).Returns(new List<Order>() { _order });
            
            _sut.AddGameToOrder("game", _customerId);

            Assert.AreEqual(3, _orderDetails.Quantity);
        }

        [Test]
        public void AddGameToOrder_AddGameToExitingGameDetailsInOrder_IncreaseCost()
        {
            _unitOfWork.Setup(m => m.OrderRepository.Get(It.IsAny<Expression<Func<Order, bool>>>())).Returns(new List<Order>() { _order });

            _sut.AddGameToOrder("game", _customerId);

            Assert.AreEqual(360, _orderDetails.Price);
        }

        [Test]
        public void AddGameToOrder_AddNewGameToExitingOrder_QuantityOfDistinctGames()
        {
            _unitOfWork.Setup(m => m.OrderRepository.Get(It.IsAny<Expression<Func<Order, bool>>>())).Returns(new List<Order>() { _order});
            _unitOfWork.Setup(m => m.GameRepository.GetGameByKey(It.IsAny<string>())).Returns(new Game());
            _unitOfWork.Setup(m => m.OrderDetailsRepository.Add(It.IsAny<OrderDetails>())).Callback(() => _order.OrderDetails.Add(It.IsAny<OrderDetails>()));

            _sut.AddGameToOrder(It.IsAny<string>(), _customerId);

            Assert.AreEqual(2, _order.OrderDetails.Count);
        }

        [Test]
        public void GetOrderByCustomerId_GiveInvalidId_ThrowException()
        {
            _unitOfWork.Setup(m => m.OrderRepository.Get(It.IsAny<Expression<Func<Order, bool>>>())).Throws(new ArgumentException());

            Assert.Catch(() => _sut.GetOrderByCustomerId(It.IsAny<string>()));
        }
    }
}
