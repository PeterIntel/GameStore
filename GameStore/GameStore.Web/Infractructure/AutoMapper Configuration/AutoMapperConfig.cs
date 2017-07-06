using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.Infrastructure.AutomapperConfiguration;

namespace GameStore.Web.Infrastructure.AutoMapperConfiguration
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
