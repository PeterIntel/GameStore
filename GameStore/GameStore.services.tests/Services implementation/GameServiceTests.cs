using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using GameStore.Services.ServicesImplementation;
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
        public void GetAll_IsCalled_CalledOneTime()
        {
            _unitOfWork.Setup(g => g.GameRepository.GetAll(It.IsAny<Expression<Func<Game, object>>[]>()))
                .Returns(() => It.IsAny<IEnumerable<Game>>());

            _sut.GetAll(x => x.Comments, x => x.Genres);

            _unitOfWork.Verify(u => u.GameRepository.GetAll(It.IsAny<Expression<Func<Game, object>>[]>()), Times.Once);
        }

        [Test]
        public void GetItemByKey_IsCalled_CalledOneTime()
        {
            _unitOfWork.Setup(g => g.GameRepository.GetGameByKey(It.IsAny<string>()))
                .Returns(It.IsAny<Game>());

            _sut.GetItemByKey("game");

            _unitOfWork.Verify(u => u.GameRepository.GetGameByKey(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetItemByKey_GameNotFoundWithKey_ReturnNull()
        {
            _unitOfWork.Setup(g => g.GameRepository.GetAll(It.IsAny<Expression<Func<Game, bool>>>(), It.IsAny<Expression<Func<Game, object>>>()))
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
        public void RemoteGame_IsCalled_CalledOneTime()
        {
            _unitOfWork.Setup(g => g.GameRepository.Remove(It.IsAny<Game>()));

            _sut.Remove(_gameStub);

            _unitOfWork.Verify(u => u.GameRepository.Remove(It.IsAny<Game>()), Times.Once);
        }

        [Test]
        public void UpdateGame_IsCalled_CalledOneTime()
        {
            _unitOfWork.Setup(g => g.GameRepository.Update(It.IsAny<Game>()));

            _sut.Update(_gameStub);

            _unitOfWork.Verify(u => u.GameRepository.Update(It.IsAny<Game>()), Times.Once);
        }

    }
}
