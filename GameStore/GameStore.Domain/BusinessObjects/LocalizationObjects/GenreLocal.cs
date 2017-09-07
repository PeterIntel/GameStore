using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.BusinessObjects.LocalizationObjects
{
    public class GenreLocal : AbstractLocalizationDomain
    {
        public string GenreId { get; set; }

        public virtual Genre Genre { get; set; }

        public string Name { get; set; }
    }
}
