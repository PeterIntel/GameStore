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
        private const string TokenName = "__ACCESS_TOKEN";
        private readonly RoleEnum[] _roles;
        private readonly AuthorizationMode _mode;
        private IApiAuthentication _authentication;

        public CustomApiAuthorize(AuthorizationMode mode, params RoleEnum[] roles)
        {
            _mode = mode;
            _roles = roles;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            _authentication = DependencyResolver.Current.GetService<IApiAuthentication>();
            IEnumerable<string> tokens;
            actionContext.Request.Headers.TryGetValues(TokenName, out tokens);
            var token = tokens?.First();

            if (token == null)
            {
                return false;
            }

            var user = _authentication.GetUserByToken(token);

            if (_mode == AuthorizationMode.Allow)
            {
                return user != null && _roles.Any(x => user.IsInRole(x.ToString()));
            }

            return user != null && !_roles.Any(x => user.IsInRole(x.ToString()));
        }
    }
}