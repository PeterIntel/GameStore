using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Logging;

namespace InfrastructureDependencyInjection.NinjectModules
{
    public class LogNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWrapper>().To<WrapNLogLogger>();
        }
    }
}
