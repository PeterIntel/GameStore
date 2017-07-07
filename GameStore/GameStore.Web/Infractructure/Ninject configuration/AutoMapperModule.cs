using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using GameStore.Web.Infrastructure.AutoMapperConfiguration;
using AutoMapper;

namespace GameStore.Infrastructure.Ninject_configuration
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            var mapperConfiguration = AutoMapperConfig.GetMapper();

            Bind<IMapper>().ToConstant(mapperConfiguration).InSingletonScope();
        }
    }
}
