using AutoMapper;
using GameStore.Infrastructure.AutomapperConfiguration;
using GameStore.Web.Infrastructure.AutoMapperConfiguration;
using Ninject.Modules;

namespace GameStore.Web.Infrastructure.NinjectConfiguration
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            var mapperConfiguration = AutoMapperConfig.GetMapper();

            //Mapper.Initialize(cfg =>
            //{
            //    cfg.AddProfile<DomainProfile>();
            //    cfg.AddProfile<ViewModelsProfile>();
            //});

            Bind<IMapper>().ToConstant(mapperConfiguration).InSingletonScope();
        }
    }
}
