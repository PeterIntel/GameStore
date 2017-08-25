using System.Linq;
using System.Security.Principal;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Authorization.Implementation
{
    public class UserProvider : IPrincipal
    {
        private readonly UserIdentity _userIdentity;

        public UserProvider()
        {
            _userIdentity = new UserIdentity();
        }
        public UserProvider(string name, IGenericDataRepository<UserEntity, User> userRepository)
        {
            _userIdentity = new UserIdentity(name, userRepository);
        }
        public IIdentity Identity
        {
            get
            {
                return _userIdentity;
            }
        }

        public bool IsInRole(string role)
        {
            return _userIdentity.User != null && _userIdentity.User.Roles.Any(r => r.RoleEnum.ToString() == role);
        }

        public override string ToString()
        {
            return _userIdentity.Name;
        }
    }
}
