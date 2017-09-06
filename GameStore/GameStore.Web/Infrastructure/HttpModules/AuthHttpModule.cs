using System;
using System.Web;
using System.Web.Mvc;
using GameStore.Authorization.Interfaces;

namespace GameStore.Web.Infrastructure.HttpModules
{
    public class AuthHttpModule : IHttpModule
    {
        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += this.Authenticate; //TODO Required: subscribe with direct 'Authenticate' method
		}

        private void Authenticate(object source, EventArgs e) //TODO Required: use 'object'
        {
            HttpApplication app = (HttpApplication)source;
            HttpContext context = app.Context;

            var auth = DependencyResolver.Current.GetService<IAuthentication>();
            auth.HttpContext = context;
            context.User = auth.CurrentUser;
        }
    }
}