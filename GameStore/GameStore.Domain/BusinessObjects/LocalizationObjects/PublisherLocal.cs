using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.BusinessObjects.LocalizationObjects
{
    public class PublisherLocal : AbstractLocalizationDomain
    {
        public string PublisherId { get; set; }

        public virtual Publisher Publisher { get; set; }

        public string Description { get; set; }
    }
}
