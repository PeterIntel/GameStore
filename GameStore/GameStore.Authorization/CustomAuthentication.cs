using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Authorization
{
    public class CustomAuthentication : IAuthentication
    {
        private const string CookieName = "__AUTH_COOKIE";
        private IGenericDataRepository<UserEntity, User> _userRepository;

        public CustomAuthentication(IGenericDataRepository<UserEntity, User> userRepository)
        {
            _userRepository = userRepository;
        }
        public IPrincipal CurrentUser
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public HttpContext HttpContext { set; get; }

        public User Login(string login)
        {
            User user = _userRepository.GetFirst(u => u.Login == login || u.Email == login);
            throw new NotImplementedException();
        }

        private void CreateCookie(string login, bool isPersistent = false)
        {
            var ticket = new FormsAuthenticationTicket(1, login, DateTime.UtcNow, DateTime.UtcNow.Add(FormsAuthentication.Timeout), )
        }

        public User Login(string login, string password, bool isPersistent)
        {

            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}
