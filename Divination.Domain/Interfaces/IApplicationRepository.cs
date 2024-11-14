using Divination.Domain;
using Divination.Domain.Entities;
using Divination.Domain.Interfaces;

namespace Divination.Domain.Interfaces
{
    public interface IApplicationRepository : IBaseRepository<Applications>
    {
        Task<IEnumerable<Applications>> GetApplicationsIsAnswerFalseAsync(int id);
         Task IsAnswerTrue(int id);
         Task<IEnumerable<Applications>> GetApplicationsIsAnswerTrueAsync(int id);
         Task<IEnumerable<Applications>> GetApplicationByClientIdIsAnsweredTrueAsync(int id);
         Task<IEnumerable<Applications>> GetApplicationByClientIdIsAnsweredFalseAsync(int id);
         Task<int> GetFortuneTellerIdByApplicationId(int ApplicationId);
    }
}