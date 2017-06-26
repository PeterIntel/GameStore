using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.contracts.DomainModels;
using GameDataAccessLayer.DAL.Repositories;
using DomainLayer.contracts;

namespace GameDataAccessLayer.DAL.UnitOfWork
{
    class UnitOfWork : IUnitOfWork
    {
        private GamesContext _context;
        private GenericDataRepository<Comment> _comments;
        private GenericDataRepository<Game> _games;
        private GenericDataRepository<Genre> _genres;
        private GenericDataRepository<PlatformType> _platformTypes;
        public UnitOfWork()
        {
            _context = new GamesContext();
        }
        public GenericDataRepository<Comment> Comments
        {
            get
            {
                if(this._comments == null)
                {
                    this._comments = new GenericDataRepository<Comment>(_context);
                }
                return _comments;
            }
        }

        public GenericDataRepository<Game> Games
        {
            get
            {
                if (this._games == null)
                {
                    this._games = new GenericDataRepository<Game>(_context);
                }
                return _games;
            }
        }

        public GenericDataRepository<Genre> Genres
        {
            get
            {
                if (this._genres == null)
                {
                    this._genres = new GenericDataRepository<Genre>(_context);
                }
                return _genres;
            }
        }

        public GenericDataRepository<PlatformType> PlatformTypes
        {
            get
            {
                if (this._platformTypes == null)
                {
                    this._platformTypes = new GenericDataRepository<PlatformType>(_context);
                }
                return _platformTypes;
            }
        }
    }
}
