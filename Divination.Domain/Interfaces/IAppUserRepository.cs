using Divination.Domain;
using Divination.Domain.Entities;
using Divination.Domain.Interfaces;

public interface IAppUserRepository:IBaseRepository<AppUser>
{
    Task<bool> Register(AppUser appUser);
}