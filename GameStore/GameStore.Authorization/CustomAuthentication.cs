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
using GameStore.Logging.Loggers;
using Ninject;
using NLog;

namespace GameStore.Authorization
{
    public class CustomAuthentication : IAuthentication
    {
        private const string CookieName = "__AUTH_COOKIE";
        private readonly IGenericDataRepository<UserEntity, User> _userRepository;
        private readonly ILogWrapper _logger;
        private IPrincipal _currentUser;
        public CustomAuthentication(IGenericDataRepository<UserEntity, User> userRepository, ILogWrapper logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public IPrincipal CurrentUser
        {
            get
            {
                try
                {
                    HttpCookie authCookie = HttpContext.Request.Cookies.Get(CookieName);
                    if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
                    {
                        var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                        _currentUser = new UserProvider(ticket.Name, _userRepository);
                    }
                    else
                    {
                        _currentUser = new UserProvider();
                    }
                }
                catch (Exception ex)
                {
                    _logger.Write(null, ex, "Error when getting user", LogLevels.Error);
                    _currentUser = new UserProvider();
                }
                return _currentUser;
            }
        }
        public HttpContext HttpContext { set; get; }

        public User Login(string userName)
        {
            User user = _userRepository.First(u => u.Login == userName || u.Email == userName);

            if (user != null)
            {
                CreateCookie(userName);
            }

            return user;
        }

        private void CreateCookie(string userName, bool isPersistent = false)
        {
            var ticket = new FormsAuthenticationTicket(1, userName, DateTime.UtcNow,
                DateTime.UtcNow.Add(FormsAuthentication.Timeout), isPersistent, string.Empty,
                FormsAuthentication.FormsCookiePath);

            var encTicket = FormsAuthentication.Encrypt(ticket);

            var authCookie = new HttpCookie(CookieName)
            {
                Value = encTicket,
                Expires = DateTime.UtcNow.Add(FormsAuthentication.Timeout),
                
            };

            HttpContext.Response.Cookies.Set(authCookie);
        }

        public User Login(string userName, string password, bool isPersistent = false)
        {

            User user = _userRepository.First(u => u.Login == userName || u.Email == userName && u.Password == password);

            if (user != null)
            {
                CreateCookie(userName);
            }

            return user;
        }

        public void Logout()
        {
            var httpCookie = HttpContext.Response.Cookies[CookieName];

            if (httpCookie != null)
            {
                httpCookie.Value = string.Empty;
            }
        }
    }
}
