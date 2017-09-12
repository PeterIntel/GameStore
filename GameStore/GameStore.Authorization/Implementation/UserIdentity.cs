using System.Security.Principal;
using GameStore.Authorization.Interfaces;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Authorization.Implementation
{
    public class UserIdentity : IIdentity, IUserProvider
    {
        public UserIdentity(string name, IGenericDataRepository<UserEntity, User> userRepository)
        {
            if (name != null)
            {
                Init(name, userRepository);
            }
        }

        public User User { get; set; }

        public string AuthenticationType
        {
            get
            {
                return typeof(User).ToString();
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return User != null;
            }
        }

        public string Name
        {
            get
            {
                if (User != null)
                {
                    return User.Login;
                }

                return "guest";
            }
        }

        private void Init(string name, IGenericDataRepository<UserEntity, User> userRepository)
        {
            if (!string.IsNullOrEmpty(name))
            {
                User = userRepository.First(u => u.Login == name || u.Id == name || u.Email == name);
            }
        }
    }
}
