using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using GameStore.Authorization.Interfaces;
using GameStore.Domain.BusinessObjects;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;

namespace GameStore.Web.Attributes
{
    public class CustomApiAuthorize : AuthorizeAttribute
    {
        private const string TokenName = "__GAMESTORE_TOKEN";
        private readonly RoleEnum[] _roles;
        private IApiAuthentication _authentication;

        public CustomApiAuthorize(params RoleEnum[] roles)
        {
            _roles = roles;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var isAuthrized = base.IsAuthorized(actionContext);

            if (!isAuthrized)
            {
                return false;
            }

            _authentication = DependencyResolver.Current.GetService<IApiAuthentication>();
            IEnumerable<string> tokens;
            actionContext.Request.Headers.TryGetValues(TokenName, out tokens);
            var token = tokens?.First();

            if (token == null)
            {
                return false;
            }

            var user = _authentication.GetUserByToken(token);

            var result = user != null && _roles.Any(x => user.IsInRole(x.ToString()));

            return result;
        }
    }
}