using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Authorization;
using GameStore.Authorization.Implementation;
using GameStore.Authorization.Interfaces;
using Ninject;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Services.ServicesImplementation;
using GameStore.DataAccess;
using GameStore.DataAccess.Mongo;
using GameStore.DataAccess.MSSQL;
using Ninject.Web.Common;

namespace GameStore.Web.Infrastructure.NinjectConfiguration
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;
        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<GamesSqlContext>().ToSelf().InRequestScope();
            _kernel.Bind<GamesMongoContext>().ToSelf().InRequestScope();
            _kernel.Bind<IGameService>().To<GameService>();
            _kernel.Bind<ICommentService>().To<CommentService>();
            _kernel.Bind<IGenreService>().To<GenreService>();
            _kernel.Bind<IPlatformTypeService>().To<PlatformTypeService>();
            _kernel.Bind<IPublisherService>().To<PublisherService>();
            _kernel.Bind<IOrderService>().To<OrderService>();
            _kernel.Bind<IAccountService>().To<AccountService>();
            _kernel.Bind<IAuthentication>().To<CustomAuthentication>().InRequestScope();
        }
    }
}