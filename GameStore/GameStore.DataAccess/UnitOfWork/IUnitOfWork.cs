using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.DataAccess.Mongo.MongoRepositories;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;
using GameStore.DataAccess.MSSQL.Repositories;

namespace GameStore.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Save();
    }
}
