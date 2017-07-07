using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using AutoMapper;
using GameStore.Web.Infractructure.AutoMapperConfiguration;

namespace GameStore.Infrastructure.NinjectConfiguration
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
