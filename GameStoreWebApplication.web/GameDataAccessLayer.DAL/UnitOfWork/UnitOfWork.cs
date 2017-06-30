using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.contracts.DomainModels;
using GameDataAccessLayer.DAL.Repositories;
using Ninject;

namespace GameDataAccessLayer.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private GamesContext _context;

         public UnitOfWork()
        {
            //_context = new GamesContext();
        }
        
        [Inject]
        public IGenericDataRepository<Comment> Comments { set; get; }
        [Inject]
        public IGenericDataRepository<Game> Games { set; get; }
        [Inject]
        public IGenericDataRepository<Genre> Genres { set; get; }
        [Inject]
        public IGenericDataRepository<PlatformType> PlatformTypes { set; get; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
