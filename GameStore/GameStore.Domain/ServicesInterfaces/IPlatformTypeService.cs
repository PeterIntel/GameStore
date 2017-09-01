using System.Collections.Generic;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Domain.ServicesInterfaces
{
    public interface IPlatformTypeService : ICrudService<PlatformType>
    {
        IEnumerable<PlatformType> GetAllPlatformTypesAndMarkSelected(IEnumerable<string> selecredPlatforms);
    }
}
