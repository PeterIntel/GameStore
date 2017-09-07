using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.BusinessObjects.LocalizationObjects
{
    public class PlatformTypeLocal : AbstractLocalizationDomain
    {
        public string PlatformTypeId { get; set; }

        public virtual PlatformType PlatformType { get; set; }

        public string TypeName { get; set; }
    }
}
