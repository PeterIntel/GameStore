using AutoMapper;
using GameStore.Infrastructure.AutomapperConfiguration;

namespace GameStore.Web.Infractructure.AutoMapperConfiguration
{
    public static class AutoMapperConfig
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainProfile>();
                cfg.AddProfile<ViewModelsProfile>();
            });

            return config.CreateMapper();
        }
    }
}
