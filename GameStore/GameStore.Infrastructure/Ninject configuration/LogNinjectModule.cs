using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using GameStore.Logging.Loggers;

namespace GameStore.Infrastructure.Ninject_configuration
{
    public class LogNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogWrapper>().To<WrapNLogLogger>();
        }
    }
}
