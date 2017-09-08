using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using GameStore.Authorization.Interfaces;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Logging.Loggers;
using GameStore.Security;

namespace GameStore.Authorization.Implementation
{
    public class ApiAuthentication : IApiAuthentication
    {
        private readonly IGenericDataRepository<UserEntity, User> _userRepository;
        private readonly IHashGenerator<string> _generator;

        public IPrincipal User { get; private set; }

        public ApiAuthentication(IGenericDataRepository<UserEntity, User> userRepository, IHashGenerator<string> generator, ILogWrapper logger)
        {
            _userRepository = userRepository;
            _generator = generator;
        }

        public IPrincipal GetUserByToken(string token)
        {
            if (token == null)
            {
                return null;
            }

            var ticket = FormsAuthentication.Decrypt(token);
            if (ticket != null && !ticket.Expired)
            {
                var user = new UserProvider(ticket.Name, _userRepository);

                return user;
            }

            return null;
        }

        public string Login(string userName, string password, bool isPersistent)
        {
            var hashedPassword = _generator.Generate(password);
            var user = _userRepository.First(u => (u.Login == userName || u.Email == userName) && u.Password == password);
            if (user == null)
            {
                return null;
            }

            User = new UserProvider(userName, _userRepository);
            var encryptedTicket = CreateTicket(user.Id, isPersistent);

            return encryptedTicket;
        }

        private string CreateTicket(string userId, bool isPersistent)
        {
            var ticket = new FormsAuthenticationTicket(1, userId, DateTime.UtcNow,
                DateTime.UtcNow.Add(FormsAuthentication.Timeout), isPersistent, string.Empty);
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            return encryptedTicket;
        }
    }
}
