using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Web.Attributes
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        private readonly RoleEnum[] _roles;
        public CustomAuthorize(params RoleEnum[] roles)
        {
            _roles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isAuthenticated = base.AuthorizeCore(httpContext);
            if (!isAuthenticated)
            {
                return false;
            }

            var result = _roles.Any(x => httpContext.User.IsInRole(x.ToString())); // TODO Required: refactor using LINQ

            return result;
        }
    }
}