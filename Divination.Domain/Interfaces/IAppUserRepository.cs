using Divination.Domain;
using Divination.Domain.Entities;
using Divination.Domain.Interfaces;

namespace Divination.Domain.Interfaces
{
    public interface IAppUserRepository : IBaseRepository<AppUser>
    {
        Task<bool> Register(AppUser appUser);
        Task<AppUser> GetEmailGoogle(string email);
        Task<bool> IsThereGoogleIdAsync(string googleId);
    }
}