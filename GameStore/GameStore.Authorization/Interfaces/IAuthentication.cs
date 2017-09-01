using System.Security.Principal;
using System.Web;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Authorization.Interfaces
{
    public interface IAuthentication
    {
        HttpContext HttpContext { set; get; }
        IPrincipal CurrentUser { get; }
        User Login(string userName, string password, bool isPersistent = false);
        User Login(string userName);
        void Logout();
    }
}
