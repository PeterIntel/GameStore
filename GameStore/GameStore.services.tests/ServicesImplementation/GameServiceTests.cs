using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Services.ServicesImplementation;
using Moq;
using NUnit.Framework;

namespace GameStore.Services.Tests.ServicesImplementation
{
    [TestFixture]
    class GameServiceTests
    {
        private GameService _sut;
        private Mock<IUnitOfWork> _unitOfWork;
        private static readonly string _gameKey = "game";
        private readonly Game _gameStub = new Game() { Id = 1, Key = _gameKey };
        private readonly GameInfo _gameInfoStub = new GameInfo() { CountOfViews = 0 };

        private readonly FilterCriteria _filters = new FilterCriteria()
        {
            SortCriteria = SortCriteria.ByPriceAsc
        };


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
            _unitOfWork.Setup(g => g.GameRepository.Get(It.IsAny<Expression<Func<Game, object>>[]>()))
                .Returns(() => It.IsAny<IEnumerable<Game>>());

            _sut.Get(x => x.Comments, x => x.Genres);

            _unitOfWork.Verify(u => u.GameRepository.Get(It.IsAny<Expression<Func<Game, object>>[]>()), Times.Once);
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
            _unitOfWork.Setup(g => g.GameRepository.Get(It.IsAny<Expression<Func<Game, bool>>>()))
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

        [Test]
        public void AddViewToGame_IsCalledGetGameByKey_OneCall()
        {
            _unitOfWork.Setup(m => m.GameRepository.GetGameByKey(It.IsAny<string>())).Returns(_gameStub);
            _unitOfWork.Setup(m => m.GameInfoRepository.GetItemById(It.IsAny<int>())).Returns(_gameInfoStub);
            _unitOfWork.Setup(m => m.GameInfoRepository.Update(It.IsAny<GameInfo>()));

            _sut.AddViewToGame(_gameKey);

            _unitOfWork.Verify(x => x.GameRepository.GetGameByKey(_gameKey), Times.Once);
        }

        [Test]
        public void AddViewToGame_IsCalledGetItemById_OneCall()
        {
            _unitOfWork.Setup(m => m.GameRepository.GetGameByKey(It.IsAny<string>())).Returns(_gameStub);
            _unitOfWork.Setup(m => m.GameInfoRepository.GetItemById(_gameStub.Id)).Returns(_gameInfoStub);
            _unitOfWork.Setup(m => m.GameInfoRepository.Update(It.IsAny<GameInfo>()));

            _sut.AddViewToGame(It.IsAny<string>());

            _unitOfWork.Verify(x => x.GameInfoRepository.GetItemById(_gameStub.Id), Times.Once);
        }

        [Test]
        public void AddViewToGame_IsCalledUpdate_OneCall()
        {
            _unitOfWork.Setup(m => m.GameRepository.GetGameByKey(It.IsAny<string>())).Returns(_gameStub);
            _unitOfWork.Setup(m => m.GameInfoRepository.GetItemById(It.IsAny<int>())).Returns(_gameInfoStub);
            _unitOfWork.Setup(m => m.GameInfoRepository.Update(_gameInfoStub));

            _sut.AddViewToGame(It.IsAny<string>());

            _unitOfWork.Verify(x => x.GameInfoRepository.Update(_gameInfoStub), Times.Once);
        }

        [Test]
        public void AddViewToGame_ChangeCountOfView_ExpectCountOfViewEqual1()
        {
            _unitOfWork.Setup(m => m.GameRepository.GetGameByKey(It.IsAny<string>())).Returns(_gameStub);
            _unitOfWork.Setup(m => m.GameInfoRepository.GetItemById(It.IsAny<int>())).Returns(_gameInfoStub);
            _unitOfWork.Setup(m => m.GameInfoRepository.Update(It.IsAny<GameInfo>()));

            _sut.AddViewToGame(It.IsAny<string>());

            Assert.AreEqual(1, _gameInfoStub.CountOfViews);
        }

        [Test]
        public void FilterGames_IsCalledGetWithSortPriceAsc_OneTime()
        {
            int count;
            _unitOfWork.Setup(x => x.GameRepository.GetCountObject(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns(It.IsAny<int>());
            _unitOfWork.Setup(x => x.GameRepository.Get(It.IsAny<Expression<Func<Game, bool>>>(),
                y => y.Price, It.IsAny<int>(), It.IsAny<int>()));

            _sut.FilterGames(_filters, out count, 1, 10);

            _unitOfWork.Verify(x => x.GameRepository.Get(It.IsAny<Expression<Func<Game, bool>>>(), y => y.Price, It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(1));
        }

        [Test]
        public void FilterGames_IsCalledGetCountObject_OneTime()
        {
            int count;
            _unitOfWork.Setup(x => x.GameRepository.GetCountObject(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns(It.IsAny<int>());
            _unitOfWork.Setup(x => x.GameRepository.Get(It.IsAny<Expression<Func<Game, bool>>>(),
                y => y.Price, It.IsAny<int>(), It.IsAny<int>()));

            _sut.FilterGames(_filters, out count, 1, 10);

            _unitOfWork.Verify(x => x.GameRepository.GetCountObject(It.IsAny<Expression<Func<Game, bool>>>()), Times.Exactly(1));
        }

        [Test]
        public void FilterGames_IsCalledGetWithoutSortCriterion_OneTime()
        {
            int count;
            _unitOfWork.Setup(x => x.GameRepository.GetCountObject(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns(It.IsAny<int>());
            _unitOfWork.Setup(x => x.GameRepository.Get(It.IsAny<Expression<Func<Game, bool>>>(),
                y => y.Id, It.IsAny<int>(), It.IsAny<int>()));

            _sut.FilterGames(new FilterCriteria(), out count, 1, 10);

            _unitOfWork.Verify(x => x.GameRepository.Get(It.IsAny<Expression<Func<Game, bool>>>(), y => y.Id, It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(1));
        }

    }
}
