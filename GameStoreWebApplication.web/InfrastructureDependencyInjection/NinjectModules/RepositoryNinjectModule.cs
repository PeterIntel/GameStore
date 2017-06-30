using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using GameDataAccessLayer.DAL.Repositories;
using DomainLayer.contracts.DomainModels;

namespace InfrastructureDependencyInjection.NinjectModules
{
    public class RepositoryNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGenericDataRepository<Game>>().To<GenericDataRepository<Game>>();
            Bind<IGenericDataRepository<PlatformType>>().To<GenericDataRepository<PlatformType>>();
            Bind<IGenericDataRepository<Genre>>().To<GenericDataRepository<Genre>>();
            Bind<IGenericDataRepository<Comment>>().To<GenericDataRepository<Comment>>();
        }
    }
}
