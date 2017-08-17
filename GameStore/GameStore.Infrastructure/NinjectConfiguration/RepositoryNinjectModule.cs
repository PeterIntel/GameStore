using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using GameStore.DataAccess.MSSQL.Repositories;
using GameStore.Domain.BusinessObjects;
using GameStore.DataAccess;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.DataAccess.Mongo.MongoRepositories;
using GameStore.DataAccess.MSSQL.Entities;

namespace GameStore.Infrastructure.NinjectConfiguration
{
    public class RepositoryNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IGenericDataRepository<,>)).To(typeof(GenericDataRepository<,>));
            Bind(typeof(IGenericDataRepository<GameEntity, Game>)).To<GameRepository>();
            Bind<IGenreRepository>().To<GenreRepository>();
            Bind<IPlatformTypeRepository>().To<PlatformTypeRepository>();

            Bind(typeof(IReadOnlyGenericRepository<,>)).To(typeof(ReadOnlyGenericRepository<,>));
            Bind<IReadOnlyGenericRepository<MongoOrderEntity, Order>>().To<ReadOnlyOrderRepository>();
            Bind<IReadOnlyGenericRepository<MongoProductEntity, Game>>().To<ReadOnlyGameRepository>();
        }
    }
}
