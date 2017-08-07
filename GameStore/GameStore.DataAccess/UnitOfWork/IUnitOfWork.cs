using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.DataAccess.Mongo.MongoRepositories;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;
using GameStore.DataAccess.MSSQL.Repositories;

namespace GameStore.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericDataRepository<CommentEntity, Comment> CommentRepository { get; }
        IGameRepository GameRepository { get; }
        IGenericDataRepository<GenreEntity, Genre> GenreRepository { get; }
        IGenericDataRepository<PlatformTypeEntity, PlatformType> PlatformTypeRepository { get; }
        IPublisherRepository PublisherRepository { get; }
        IGenericDataRepository<OrderEntity, Order> OrderRepository { get; }
        IGenericDataRepository<OrderDetailsEntity, OrderDetails> OrderDetailsRepository { get; }
        IGenericDataRepository<GameInfoEntity, GameInfo> GameInfoRepository { get; }
        IReadOnlyGenericRepository<MongoProduct, Game> ProductRepository { get; }
        void Save();
    }
}
