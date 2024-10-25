using Divination.Domain;
using Divination.Domain.Entities;
using Divination.Domain.Interfaces;

namespace Divination.Domain.Interfaces
{
    public interface IApplicationRepository : IBaseRepository<Applications>
    {
        Task<IEnumerable<Applications>> GetApplicationsIsAnswerFalseAsync(int id);
         Task IsAnswerTrue(int id);
    }
}