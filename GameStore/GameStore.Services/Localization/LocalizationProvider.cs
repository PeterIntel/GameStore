using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.Localization
{
    public class LocalizationProvider<T> : ILocalizationProvider<T> where T : BasicDomain
    {
        public T Localize(T entity, string cultureCode)
        {
            return entity;
        }
    }
}
