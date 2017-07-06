using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using GameStore.Services.Services_implementation;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.Tests.Services_implementation
{
    [TestFixture]
    class GameServiceTests
    {
        private GameService _sut;
        private Mock<IUnitOfWork> _unitOfWork;
        private Game _gameStub = new Game();

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new GameService(_unitOfWork.Object);
        }

        [Test]
        public void Add_IsCalled_CalledOneTime()
        {
            _unitOfWork.Setup(p => p.GameRepository.Add(It.IsAny<Game>()));

            _sut.Add(new Game());

            _unitOfWork.Verify(u => u.GameRepository.Add(It.IsAny<Game>()), Times.Once);
        }

        [Test]
        public void Add_ExceptoinThrown_DoesnotCatch()
        {
            _unitOfWork.Setup(g => g.GameRepository.Add(It.IsAny<Game>()))
                .Throws(new Exception());

            Assert.Throws<Exception>(() => _sut.Add(It.IsAny<Game>()));
        }

        [Test]
        public void GetAll_IsCalled_CalledOneTime()
        {
            _unitOfWork.Setup(g => g.GameRepository.GetAll(It.IsAny<Expression<Func<Game, bool>>>(), It.IsAny<string>()))
                .Returns(() => It.IsAny<IList<Game>>());

            _sut.GetAll(gameStub => false, "");

            _unitOfWork.Verify(u => u.GameRepository.GetAll(It.IsAny<Expression<Func<Game, bool>>>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetAll_ExceptoinThrown_DoesnotCatch()
        {
            _unitOfWork.Setup(g => g.GameRepository.GetAll(It.IsAny<Expression<Func<Game, bool>>>(), It.IsAny<string>()))
               .Throws(new Exception());

            Assert.Throws<Exception>(() => _sut.GetAll(It.IsAny<Expression<Func<Game, bool>>>(), It.IsAny<string>()));
        }

        [Test]
        public void GetItemByKey_IsCalled_CalledOneTime()
        {
            _unitOfWork.Setup(g => g.GameRepository.GetAll(It.IsAny<Expression<Func<Game, bool>>>(), It.IsAny<string>()))
                .Returns(() => It.IsAny<IList<Game>>());

            _sut.GetItemByKey("game");

            _unitOfWork.Verify(u => u.GameRepository.GetAll(It.IsAny<Expression<Func<Game, bool>>>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetItemByKey_GameNotFoundWithKey_ReturnNull()
        {
            _unitOfWork.Setup(g => g.GameRepository.GetAll(It.IsAny<Expression<Func<Game, bool>>>(), It.IsAny<string>()))
                .Returns(() => It.IsAny<IList<Game>>());

            var result = _sut.GetItemByKey("game");

            Assert.AreEqual(null, result);
        }

        [Test]
        public void RemoteGameId_IsCalled_CalledOneTime()
        {
            _unitOfWork.Setup(g => g.GameRepository.Remove(It.IsAny<int>()));

            _sut.Remove(133);

            _unitOfWork.Verify(u => u.GameRepository.Remove(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void RemoteGameId_ExceptoinThrown_DoesnotCatch()
        {
            _unitOfWork.Setup(g => g.GameRepository.Remove(It.IsAny<int>()))
               .Throws(new Exception());

            Assert.Throws<Exception>(() => _sut.Remove(It.IsAny<int>()));
        }

        [Test]
        public void RemoteGame_IsCalled_CalledOneTime()
        {
            _unitOfWork.Setup(g => g.GameRepository.Remove(It.IsAny<Game>()));

            _sut.Remove(_gameStub);

            _unitOfWork.Verify(u => u.GameRepository.Remove(It.IsAny<Game>()), Times.Once);
        }

        [Test]
        public void RemoteGame_ExceptoinThrown_DoesnotCatch()
        {
            _unitOfWork.Setup(g => g.GameRepository.Remove(It.IsAny<Game>()))
               .Throws(new Exception());

            Assert.Throws<Exception>(() => _sut.Remove(It.IsAny<Game>()));
        }

        [Test]
        public void UpdateGame_IsCalled_CalledOneTime()
        {
            _unitOfWork.Setup(g => g.GameRepository.Update(It.IsAny<Game>()));

            _sut.Update(_gameStub);

            _unitOfWork.Verify(u => u.GameRepository.Update(It.IsAny<Game>()), Times.Once);
        }

        [Test]
        public void UpdateGame_ExceptoinThrown_DoesnotCatch()
        {
            _unitOfWork.Setup(g => g.GameRepository.Update(It.IsAny<Game>()))
               .Throws(new Exception());

            Assert.Throws<Exception>(() => _sut.Update(It.IsAny<Game>()));
        }

    }
}
