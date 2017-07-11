using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Services.ServicesImplementation;
using GameStore.DataAccess;
using GameStore.DataAccess.Context;
using Ninject.Web.Common;

namespace GameStore.Web.Infrastructure.NinjectConfiguration
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;
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
            _kernel.Bind<GamesContext>().ToSelf().InRequestScope();
            _kernel.Bind<IGameService>().To<GameService>();
            _kernel.Bind<ICommentService>().To<CommentService>();
            _kernel.Bind<IGenreService>().To<GenreService>();
            _kernel.Bind<IPlatformTypeService>().To<PlatformTypeService>();
        }
    }
}