using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Contextes;
using GameStore.DataAccess.Entities;
using GameStore.Domain.BusinessObjects;
using GameStore.DataAccess.Repositories;
using Ninject;

namespace GameStore.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GamesSqlContext _context;
        private readonly GamesMongoContext _mongoContext;

         public UnitOfWork(GamesSqlContext context)
        {
            _context = context;
            _mongoContext = new GamesMongoContext();
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

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
