using GameStore.DataAccess.MSSQL.Entities.Localization;

namespace GameStore.DataAccess.Interfaces
{
    public interface ICultureRepository
    {
        CultureEntity GetCultureByCode(string code);
    }
}