using System.Collections.Generic;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Domain.ServicesInterfaces
{
    public interface IPublisherService : ICrudService<Publisher>
    {
        Publisher GetPublisherByCompanyName(string companyName, string cultureCode);
        IEnumerable<Publisher> GetAllPublishersAndMarkSelected(IEnumerable<string> selecredPublishers, string cultureCode);
    }
}
