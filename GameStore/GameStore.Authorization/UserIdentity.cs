using System.Security.Principal;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Authorization
{
    public class UserIdentity : IIdentity, IUserProvider
    {
        public UserIdentity()
        {
            
        }
        public UserIdentity(string name, IGenericDataRepository<UserEntity, User> userRepository)
        {
            Init(name, userRepository);
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
                User = userRepository.First(u => u.Login == name || u.Email == name);
            }
        }
    }
}
