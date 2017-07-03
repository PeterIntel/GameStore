using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Entities;
using GameStore.Domain.Business_objects;
using GameStore.DataAccess.Repositories;
using Ninject;

namespace GameStore.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private GamesContext _context;

         public UnitOfWork()
        {
            //_context = new GamesContext();
        }
        
        [Inject]
        public IGenericDataRepository<CommentEntity, Comment> Comments { set; get; }
        [Inject]
        public IGenericDataRepository<GameEntity, Game> Games { set; get; }
        [Inject]
        public IGenericDataRepository<GenreEntity, Genre> Genres { set; get; }
        [Inject]
        public IGenericDataRepository<PlatformTypeEntity, PlatformType> PlatformTypes { set; get; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
