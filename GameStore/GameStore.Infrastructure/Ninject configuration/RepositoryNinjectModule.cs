using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using GameStore.DataAccess.Repositories;
using GameStore.DataAccess.Entities;
using GameStore.Domain.BusinessObjects;
using GameStore.DataAccess;

namespace GameStore.Infrastructure.Ninject_configuration
{
    public class RepositoryNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IGenericDataRepository<,>)).To(typeof(GenericDataRepository<,>));
            Bind<IGameRepository>().To<GameRepository>();
        }
    }
}
