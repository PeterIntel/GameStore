using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.BusinessObjects.LocalizationObjects
{
    public class GameLocal : AbstractLocalizationDomain
    {
        public string GameId { get; set; }

        public virtual Game Game { get; set; }

        public string Description { get; set; }
    }
}
