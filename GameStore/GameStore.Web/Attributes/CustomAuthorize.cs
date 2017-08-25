using System.Web;
using System.Web.Mvc;
using GameStore.Authorization;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Web.Attributes
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        private RoleEnum[] _roles;
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

            if (_roles != null)
            {
                foreach (var role in _roles)
                {
                    if (httpContext.User.IsInRole(role.ToString()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}