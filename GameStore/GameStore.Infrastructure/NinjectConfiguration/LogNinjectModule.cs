using GameStore.Logging.Loggers;
using Ninject.Modules;

namespace GameStore.Infrastructure.NinjectConfiguration
{
    public class LogNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogWrapper>().To<WrapNLogLogger>();
            Bind(typeof(IMongoLogger<>)).To(typeof(MongoLogger<>));
        }
    }
}
