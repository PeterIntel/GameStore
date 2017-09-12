using System.Security.Principal;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Authorization.Interfaces
{
    public interface IApiAuthentication
    {
        IPrincipal User { get; }

        string Login(string userName, string password, bool isPersistent);

        IPrincipal GetUserByToken(string token);
    }
}