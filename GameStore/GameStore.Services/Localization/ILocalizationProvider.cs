using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.Localization
{
    public interface ILocalizationProvider<T> where T : BasicDomain
    {
        T Localize(T entity, string cultureCode);
    }
}