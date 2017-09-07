using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.Localization.Specific
{
    public class PlatformTypeLocalizationProvider : ILocalizationProvider<PlatformType>
    {
        public PlatformType Localize(PlatformType platformType, string cultureCode)
        {
            if (platformType.Locals != null && platformType.Locals.Any())
            {
                var local = platformType.Locals.FirstOrDefault(x => x.Culture.Code == cultureCode) ??
                            platformType.Locals.First();
                platformType.TypeName = local.TypeName;
			} //TODO Required: blank line below
			return platformType;
        }
    }
}
