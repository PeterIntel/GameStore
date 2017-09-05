using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.Localization.Specific
{
    public class PublisherLocalizationProvider : ILocalizationProvider<Publisher>
    {
        public Publisher Localize(Publisher publisher, string cultureCode)
        {
            if (publisher.Locals != null && publisher.Locals.Any())
            {
                var local = publisher.Locals.FirstOrDefault(x => x.Culture.Code == cultureCode) ??
                            publisher.Locals.First();
                publisher.Description = local.Description;
            }
            return publisher;
        }
    }
}
