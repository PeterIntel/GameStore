﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Entities;
using GameStore.Domain.BusinessObjects;
using GameStore.DataAccess.Repositories;
using Ninject;

namespace GameStore.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private GamesContext _context;

         public UnitOfWork(GamesContext context)
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

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
