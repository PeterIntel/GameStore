using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using GameStore.DataAccess.MSSQL.Repositories;
using GameStore.Domain.BusinessObjects;
using GameStore.DataAccess;
using GameStore.DataAccess.Mongo.MongoRepositories;

namespace GameStore.Infrastructure.NinjectConfiguration
{
    public class RepositoryNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IGenericDataRepository<,>)).To(typeof(GenericDataRepository<,>));
            Bind<IGameRepository>().To<GameRepository>();
            Bind<IGenreRepository>().To<GenreRepository>();
            Bind<IPlatformTypeRepository>().To<PlatformTypeRepository>();
            Bind<IPublisherRepository>().To<PublisherRepository>();

            Bind(typeof(IReadOnlyGenericRepository<,>)).To(typeof(ReadOnlyGenericRepository<,>));
        }
    }
}
