using GameStore.Security;
using Ninject.Modules;

namespace GameStore.Infrastructure.NinjectConfiguration
{
    public class SecuriryNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IHashGenerator<string>>().To<HashGenerator>();
        }
    }
}
