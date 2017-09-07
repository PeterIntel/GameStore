using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities.Localization;
using GameStore.Domain.BusinessObjects.LocalizationObjects;

namespace GameStore.DataAccess.MSSQL.Repositories
{
    public class CultureRepository : GenericDataRepository<CultureEntity, Culture>, ICultureRepository
    {
        public CultureRepository(GamesSqlContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public CultureEntity GetCultureByCode(string code)
        {
            var culture = _dbSet.First(c => c.Code == code);

            return culture;
        }
    }
}
