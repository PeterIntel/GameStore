using System.Collections.Generic;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Domain.ServicesInterfaces
{
    public interface IAccountService : ICrudService<User>
    {
        IEnumerable<Role> GetAllRolesAndMarkSelected(IEnumerable<string> selecredGenres);
        int GetCountAdministrators();
        IEnumerable<Role> GetRoles();
    }
}
