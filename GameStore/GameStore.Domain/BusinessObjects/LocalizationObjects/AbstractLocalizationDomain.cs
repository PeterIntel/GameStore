using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.BusinessObjects.LocalizationObjects
{
    public class AbstractLocalizationDomain
    {
        public string Id { get; set; }

        public string CultureId { get; set; }

        public virtual Culture Culture { get; set; }
    }
}
