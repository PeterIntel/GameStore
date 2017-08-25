using GameStore.Domain.BusinessObjects;

namespace GameStore.Authorization.Interfaces
{
    public interface IUserProvider
    {
        User User { set; get; }
    }
}
