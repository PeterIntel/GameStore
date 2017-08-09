using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Interfaces;
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
        
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
