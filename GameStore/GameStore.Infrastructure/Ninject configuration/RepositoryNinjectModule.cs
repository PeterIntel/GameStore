using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using GameStore.DataAccess.Repositories;
using GameStore.DataAccess.Entities;
using GameStore.Domain.Business_objects;

namespace GameStore.Infrastructure.Ninject_configuration
{
    public class RepositoryNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGenericDataRepository<GameEntity, Game>>().To<GenericDataRepository<GameEntity, Game>>();
            Bind<IGenericDataRepository<PlatformTypeEntity, PlatformType>>().To<GenericDataRepository<PlatformTypeEntity, PlatformType>>();
            Bind<IGenericDataRepository<GenreEntity, Genre>>().To<GenericDataRepository<GenreEntity, Genre>>();
            Bind<IGenericDataRepository<CommentEntity, Comment>>().To<GenericDataRepository<CommentEntity, Comment>>();
        }
    }
}
