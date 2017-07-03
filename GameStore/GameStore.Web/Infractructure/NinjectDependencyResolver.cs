using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using GameStore.Domain.Services_interfaces;
using GameStore.Services.Services_implementation;
using GameStore.DataAccess;
using Ninject.Web.Common;

namespace GameStore.Web.Infractructure
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
        }
    }
}