using NewsPortal.Domain.Entities;

namespace NewsPortal.Infrastructure.Interfaces;

public interface IUserRepository
{
    public Task<User> GetUserByIdAsync(int id);
    public Task<IEnumerable<User>> GetUsersAsync();
}