﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Logging.Loggers;
using GameStore.Services.Localization;
using GameStore.Services.ServicesImplementation;
using Moq;
using NUnit.Framework;

namespace GameStore.Services.Tests.ServicesImplementation
{
    [TestFixture]
    class GameServiceTests
    {
        private static readonly string _gameKey = "game";
        private readonly Game _gameStub = new Game() { Id = "1", Key = _gameKey };
        private readonly GameInfo _gameInfoStub = new GameInfo() { CountOfViews = 0 };
        private readonly FilterCriteria _filters = new FilterCriteria()
        {
            SortCriteria = SortCriteria.ByPriceAsc
        };
        private GameService _sut;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IGameRepository> _gameRepository;
        private Mock<IGenericDataRepository<GameInfoEntity, GameInfo>> _gameInfoRepository;
        private Mock<IGenericDataRepository<GenreEntity, Genre>> _genreRepository;
        private Mock<IGenericDataRepository<PublisherEntity, Publisher>> _publisherRepository;
        private Mock<IGenericDataRepository<PlatformTypeEntity, PlatformType>> _platformRepository;
        private Mock<IMongoLogger<Game>> _logger;
        private Mock<ILocalizationProvider<Game>> _localizationProvider;


        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _gameRepository = new Mock<IGameRepository>();
            _gameInfoRepository = new Mock<IGenericDataRepository<GameInfoEntity, GameInfo>>();
            _genreRepository = new Mock<IGenericDataRepository<GenreEntity, Genre>>();
            _publisherRepository = new Mock<IGenericDataRepository<PublisherEntity, Publisher>>();
            _logger = new Mock<IMongoLogger<Game>>();
            _localizationProvider = new Mock<ILocalizationProvider<Game>>();
            _platformRepository = new Mock<IGenericDataRepository<PlatformTypeEntity, PlatformType>>();
            _sut = new GameService(_unitOfWork.Object, _gameRepository.Object, _gameInfoRepository.Object, _genreRepository.Object, _publisherRepository.Object, _logger.Object, _localizationProvider.Object, _platformRepository.Object);
        }

        [Test]
        public void Add_IsCalled_CalledOneTime()
        {
            _gameRepository.Setup(p => p.Add(It.IsAny<Game>()));

            _sut.Add(new Game(), It.IsAny<string>());

            _gameRepository.Verify(p => p.Add(It.IsAny<Game>()), Times.Once);
        }

        [Test]
        public void GetAll_IsCalled_CalledOneTime()
        {
            _gameRepository.Setup(g => g.Get(It.IsAny<Expression<Func<Game, bool>>>(), It.IsAny<Expression<Func<Game, string>>>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() => new List<Game>());
            _gameRepository.Setup(g => g.GetCountObject(It.IsAny<Expression<Func<Game, bool>>>())).Returns(() => It.IsAny<int>());

            _sut.Get(It.IsAny<string>(), x => x.Genres);

            _gameRepository.Verify(u => u.Get(It.IsAny<Expression<Func<Game, bool>>>(), It.IsAny<Expression<Func<Game, string>>>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void GetItemByKey_IsCalled_CalledOneTime()
        {
            _gameRepository.Setup(g => g.First(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns(It.IsAny<Game>());

            _sut.GetItemByKey("game", It.IsAny<string>());

            _gameRepository.Verify(u => u.First(It.IsAny<Expression<Func<Game, bool>>>()), Times.Once);
        }

        [Test]
        public void GetItemByKey_GameNotFoundWithKey_ReturnNull()
        {
            _gameRepository.Setup(g => g.Get(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns(() => It.IsAny<IList<Game>>());

            var result = _sut.GetItemByKey("game", It.IsAny<string>());

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

            _sut.Update(_gameStub, It.IsAny<string>());

            _gameRepository.Verify(u => u.Update(It.IsAny<Game>()), Times.Once);
        }

        [Test]
        public void AddViewToGame_IsCalledGetGameByKey_OneCall()
        {
            _gameRepository.Setup(m => m.First(It.IsAny<Expression<Func<Game, bool>>>())).Returns(_gameStub);
            _gameInfoRepository.Setup(m => m.GetItemById(It.IsAny<string>())).Returns(_gameInfoStub);
            _gameInfoRepository.Setup(m => m.Update(It.IsAny<GameInfo>()));

            _sut.AddViewToGame(_gameKey, It.IsAny<string>());

            _gameRepository.Verify(x => x.First(It.IsAny<Expression<Func<Game, bool>>>()), Times.Exactly(2));
        }

        [Test]
        public void AddViewToGame_IsCalledGetItemById_OneCall()
        {
            _gameRepository.Setup(m => m.First(It.IsAny<Expression<Func<Game, bool>>>())).Returns(_gameStub);
            _gameInfoRepository.Setup(m => m.GetItemById(_gameStub.Id)).Returns(_gameInfoStub);
            _gameInfoRepository.Setup(m => m.Update(It.IsAny<GameInfo>()));

            _sut.AddViewToGame(It.IsAny<string>(), It.IsAny<string>());

            _gameInfoRepository.Verify(x => x.GetItemById(_gameStub.Id), Times.Once);
        }

        [Test]
        public void AddViewToGame_IsCalledUpdate_OneCall()
        {
            _gameRepository.Setup(m => m.First(It.IsAny<Expression<Func<Game, bool>>>())).Returns(_gameStub);
            _gameInfoRepository.Setup(m => m.GetItemById(It.IsAny<string>())).Returns(_gameInfoStub);
            _gameInfoRepository.Setup(m => m.Update(_gameInfoStub));

            _sut.AddViewToGame(It.IsAny<string>(), It.IsAny<string>());

            _gameInfoRepository.Verify(x => x.Update(_gameInfoStub), Times.Once);
        }

        [Test]
        public void AddViewToGame_ChangeCountOfView_ExpectCountOfViewEqual1()
        {
            _gameRepository.Setup(m => m.First(It.IsAny<Expression<Func<Game, bool>>>())).Returns(_gameStub);
            _gameInfoRepository.Setup(m => m.GetItemById(It.IsAny<string>())).Returns(_gameInfoStub);
            _gameInfoRepository.Setup(m => m.Update(It.IsAny<GameInfo>()));

            _sut.AddViewToGame(It.IsAny<string>(), It.IsAny<string>());

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

            _sut.FilterGames(_filters, 1, "10", It.IsAny<string>());

            _gameRepository.Verify(x => x.Get(It.IsAny<Expression<Func<Game, bool>>>(), y => y.Price, It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(1));
        }

        [Test]
        public void FilterGames_IsCalledGetCountObject_OneTime()
        {
            int count;
            _gameRepository.Setup(x => x.GetCountObject(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns(It.IsAny<int>());
            _gameRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Game, bool>>>()));

            _sut.FilterGames(_filters, 1, "10", It.IsAny<string>());

            _gameRepository.Verify(x => x.GetCountObject(It.IsAny<Expression<Func<Game, bool>>>()), Times.Exactly(1));
        }

        [Test]
        public void FilterGames_IsCalledGetWithoutSortCriterion_OneTime()
        {
            _gameRepository.Setup(x => x.GetCountObject(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns(It.IsAny<int>());
            _gameRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Game, bool>>>(), It.IsAny<Expression<Func<Game, string>>>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>())).Returns(new List<Game>());

            _sut.FilterGames(new FilterCriteria(), 1, "10", It.IsAny<string>());

            _gameRepository.Verify(x => x.Get(It.IsAny<Expression<Func<Game, bool>>>(), It.IsAny<Expression<Func<Game, string>>>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(1));
        }

    }
}
