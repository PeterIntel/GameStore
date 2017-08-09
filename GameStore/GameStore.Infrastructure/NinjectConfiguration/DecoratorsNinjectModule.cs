using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Decorators;
using Ninject.Modules;

namespace GameStore.Infrastructure.NinjectConfiguration
{
    public class DecoratorsNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IGenericDecoratorRepository<,,>)).To(typeof(GenericDecoratorRepositoryRepository<,,>));
            Bind<IGameDecoratorRepositoryRepository>().To<GameDecoratorRepositoryRepository>();
        }
    }
}
