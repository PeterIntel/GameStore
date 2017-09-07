using GameStore.DataAccess.UnitOfWork;
using Ninject.Modules;

namespace GameStore.Infrastructure.NinjectConfiguration
{
    public class UnitOfWorkNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
