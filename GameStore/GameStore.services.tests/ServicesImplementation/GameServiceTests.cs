using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using GameStore.DataAccess.Decorators;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Services.ServicesImplementation;
using Moq;
using NUnit.Framework;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.DataAccess.Interfaces;
using GameStore.Logging.Loggers;

namespace GameStore.Services.Tests.ServicesImplementation
{
    [TestFixture]
    class GameServiceTests
    {
        private GameService _sut;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IGameDecoratorRepositoryRepository> _gameRepository;
        private Mock<IGenericDataRepository<GameInfoEntity, GameInfo>> _gameInfoRepository;
        private Mock<IGenericDecoratorRepository<GenreEntity, MongoCategoryEntity, Genre>> _genreRepository;
        private Mock<IGenericDecoratorRepository<PublisherEntity, MongoSupplierEntity, Publisher>> _publisherRepository;
        private Mock<IMapper> _mapper;
        private Mock<IMongoLogger<Game>> _logger;
        private static readonly string _gameKey = "game";
        private readonly Game _gameStub = new Game() { Id = "1", Key = _gameKey };
        private readonly GameInfo _gameInfoStub = new GameInfo() { CountOfViews = 0 };

        private readonly FilterCriteria _filters = new FilterCriteria()
        {
            SortCriteria = SortCriteria.ByPriceAsc
        };


        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _gameRepository = new Mock<IGameDecoratorRepositoryRepository>();
            _gameInfoRepository = new Mock<IGenericDataRepository<GameInfoEntity, GameInfo>>();
            _genreRepository = new Mock<IGenericDecoratorRepository<GenreEntity, MongoCategoryEntity, Genre>>();
            _publisherRepository = new Mock<IGenericDecoratorRepository<PublisherEntity, MongoSupplierEntity, Publisher>>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<IMongoLogger<Game>>();
            _sut = new GameService(_unitOfWork.Object, _gameRepository.Object, _gameInfoRepository.Object, _genreRepository.Object, _publisherRepository.Object, _mapper.Object, _logger.Object);
        }

        [Test]
        public void Add_IsCalled_CalledOneTime()
        {
            _gameRepository.Setup(p => p.Add(It.IsAny<Game>()));

            _sut.Add(new Game());

            _gameRepository.Verify(p => p.Add(It.IsAny<Game>()), Times.Once);
        }

        [Test]
        public void GetAll_IsCalled_CalledOneTime()
        {
            _gameRepository.Setup(g => g.Get(It.IsAny<Expression<Func<Game, bool>>>(), It.IsAny<Expression<Func<Game, string>>>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() => new List<Game>());
            _gameRepository.Setup(g => g.GetCountObject(It.IsAny<Expression<Func<Game, bool>>>())).Returns(() => It.IsAny<int>());

            _sut.Get(x => x.Genres);

            _gameRepository.Verify(u => u.Get(It.IsAny<Expression<Func<Game, bool>>>(), It.IsAny<Expression<Func<Game, string>>>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void GetItemByKey_IsCalled_CalledOneTime()
        {
            _gameRepository.Setup(g => g.GetFirst(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns(It.IsAny<Game>());

            _sut.GetItemByKey("game");

            _gameRepository.Verify(u => u.GetFirst(It.IsAny<Expression<Func<Game, bool>>>()), Times.Once);
        }

        [Test]
        public void GetItemByKey_GameNotFoundWithKey_ReturnNull()
        {
            _gameRepository.Setup(g => g.Get(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns(() => It.IsAny<IList<Game>>());

            var result = _sut.GetItemByKey("game");

            Assert.AreEqual(null, result);
        }

        [Test]
        public void RemoteGameId_IsCalled_CalledOneTime()
        {
            _gameRepository.Setup(g => g.Remove(It.IsAny<string>()));

            _sut.Remove("133");

            _gameRepository.Verify(u => u.Remove(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void RemoteGame_IsCalled_CalledOneTime()
        {
            _gameRepository.Setup(g => g.Remove(It.IsAny<Game>()));

            _sut.Remove(_gameStub);

            _gameRepository.Verify(u => u.Remove(It.IsAny<Game>()), Times.Once);
        }

        [Test]
        public void UpdateGame_IsCalled_CalledOneTime()
        {
            _gameRepository.Setup(g => g.Update(It.IsAny<Game>()));

            _sut.Update(_gameStub);

            _gameRepository.Verify(u => u.Update(It.IsAny<Game>()), Times.Once);
        }

        [Test]
        public void AddViewToGame_IsCalledGetGameByKey_OneCall()
        {
            _gameRepository.Setup(m => m.GetFirst(It.IsAny<Expression<Func<Game, bool>>>())).Returns(_gameStub);
            _gameInfoRepository.Setup(m => m.GetItemById(It.IsAny<string>())).Returns(_gameInfoStub);
            _gameInfoRepository.Setup(m => m.Update(It.IsAny<GameInfo>()));

            _sut.AddViewToGame(_gameKey);

            _gameRepository.Verify(x => x.GetFirst(It.IsAny<Expression<Func<Game, bool>>>()), Times.Exactly(2));
        }

        [Test]
        public void AddViewToGame_IsCalledGetItemById_OneCall()
        {
            _gameRepository.Setup(m => m.GetFirst(It.IsAny<Expression<Func<Game, bool>>>())).Returns(_gameStub);
            _gameInfoRepository.Setup(m => m.GetItemById(_gameStub.Id)).Returns(_gameInfoStub);
            _gameInfoRepository.Setup(m => m.Update(It.IsAny<GameInfo>()));

            _sut.AddViewToGame(It.IsAny<string>());

            _gameInfoRepository.Verify(x => x.GetItemById(_gameStub.Id), Times.Once);
        }

        [Test]
        public void AddViewToGame_IsCalledUpdate_OneCall()
        {
            _gameRepository.Setup(m => m.GetFirst(It.IsAny<Expression<Func<Game, bool>>>())).Returns(_gameStub);
            _gameInfoRepository.Setup(m => m.GetItemById(It.IsAny<string>())).Returns(_gameInfoStub);
            _gameInfoRepository.Setup(m => m.Update(_gameInfoStub));

            _sut.AddViewToGame(It.IsAny<string>());

            _gameInfoRepository.Verify(x => x.Update(_gameInfoStub), Times.Once);
        }

        [Test]
        public void AddViewToGame_ChangeCountOfView_ExpectCountOfViewEqual1()
        {
            _gameRepository.Setup(m => m.GetFirst(It.IsAny<Expression<Func<Game, bool>>>())).Returns(_gameStub);
            _gameInfoRepository.Setup(m => m.GetItemById(It.IsAny<string>())).Returns(_gameInfoStub);
            _gameInfoRepository.Setup(m => m.Update(It.IsAny<GameInfo>()));

            _sut.AddViewToGame(It.IsAny<string>());

            Assert.AreEqual(1, _gameInfoStub.CountOfViews);
        }

        [Test]
        public void FilterGames_IsCalledGetWithSortPriceAsc_OneTime()
        {
            int count;
            _gameRepository.Setup(x => x.GetCountObject(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns(It.IsAny<int>());
            _gameRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Game, bool>>>(),
                y => y.Price, It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>())).Returns(new List<Game>());

            _sut.FilterGames(_filters, 1, "10");

            _gameRepository.Verify(x => x.Get(It.IsAny<Expression<Func<Game, bool>>>(), y => y.Price, It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(1));
        }

        [Test]
        public void FilterGames_IsCalledGetCountObject_OneTime()
        {
            int count;
            _gameRepository.Setup(x => x.GetCountObject(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns(It.IsAny<int>());
            _gameRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Game, bool>>>()));

            _sut.FilterGames(_filters, 1, "10");

            _gameRepository.Verify(x => x.GetCountObject(It.IsAny<Expression<Func<Game, bool>>>()), Times.Exactly(1));
        }

        [Test]
        public void FilterGames_IsCalledGetWithoutSortCriterion_OneTime()
        {
            _gameRepository.Setup(x => x.GetCountObject(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns(It.IsAny<int>());
            _gameRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Game, bool>>>(), It.IsAny<Expression<Func<Game, string>>>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>())).Returns(new List<Game>());

            _sut.FilterGames(new FilterCriteria(), 1, "10");

            _gameRepository.Verify(x => x.Get(It.IsAny<Expression<Func<Game, bool>>>(), It.IsAny<Expression<Func<Game, string>>>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(1));
        }

    }
}
