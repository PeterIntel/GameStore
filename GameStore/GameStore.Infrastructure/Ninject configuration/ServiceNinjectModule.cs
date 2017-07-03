using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using GameStore.DataAccess.UnitOfWork;

namespace GameStore.Infrastructure.Ninject_configuration
{
    public class ServiceNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
