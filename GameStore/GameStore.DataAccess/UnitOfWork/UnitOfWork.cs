using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Mongo;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.DataAccess.Mongo.MongoRepositories;
using GameStore.DataAccess.MSSQL;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;
using GameStore.DataAccess.MSSQL.Repositories;
using Ninject;

namespace GameStore.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GamesSqlContext _context;

         public UnitOfWork(GamesSqlContext context)
        {
            _context = context;
        }
        
        [Inject]
        public IGenericDataRepository<CommentEntity, Comment> CommentRepository { set; get; }
        [Inject]
        public IGameRepository GameRepository { set; get; }
        [Inject]
        public IGenericDataRepository<GenreEntity, Genre> GenreRepository { set; get; }
        [Inject]
        public IGenericDataRepository<PlatformTypeEntity, PlatformType> PlatformTypeRepository { set; get; }
        [Inject]
        public IPublisherRepository PublisherRepository { set; get; }
        [Inject]
        public IGenericDataRepository<OrderEntity, Order> OrderRepository { set; get; }
        [Inject]
        public IGenericDataRepository<OrderDetailsEntity, OrderDetails> OrderDetailsRepository { set; get; }
        [Inject]
        public IGenericDataRepository<GameInfoEntity, GameInfo> GameInfoRepository { set; get; }
        [Inject]
        public IReadOnlyGenericRepository<MongoProduct, Game> ProductRepository { set; get; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
