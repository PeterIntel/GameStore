using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Authorization
{
    public interface IAuthentication
    {
        HttpContext HttpContext { set; get; }
        User Login(string login, string password, bool isPersistent);
        User Login(string login);
        void Logout();
        IPrincipal CurrentUser { get; }
    }
}
