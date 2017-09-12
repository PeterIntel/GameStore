using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Services.Localization;
using GameStore.Services.Localization.Specific;
using GameStore.Services.ServicesImplementation;
using Ninject.Modules;

namespace GameStore.Infrastructure.NinjectConfiguration
{
    public class ServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGameService>().To<GameService>();
            Bind<ICommentService>().To<CommentService>();
            Bind<IGenreService>().To<GenreService>();
            Bind<IPlatformTypeService>().To<PlatformTypeService>();
            Bind<IPublisherService>().To<PublisherService>();
            Bind<IOrderService>().To<OrderService>();
            Bind<IAccountService>().To<AccountService>();

            Bind(typeof(ILocalizationProvider<>)).To(typeof(LocalizationProvider<>));
            Bind<ILocalizationProvider<Game>>().To<GameLocalizationProvider>();
            Bind<ILocalizationProvider<Genre>>().To<GenreLocalizationProvider>();
            Bind<ILocalizationProvider<Publisher>>().To<PublisherLocalizationProvider>();
            Bind<ILocalizationProvider<PlatformType>>().To<PlatformTypeLocalizationProvider>();
        }
    }
}
