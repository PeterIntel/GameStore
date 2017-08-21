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
        User Login(string userName, string password, bool isPersistent = false);
        User Login(string userName);
        void Logout();
        IPrincipal CurrentUser { get; }
    }
}
