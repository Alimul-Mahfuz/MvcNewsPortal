using Microsoft.EntityFrameworkCore;
using NewsPortal.Domain.Entities;
using NewsPortal.Infrastructure.Data;
using NewsPortal.Infrastructure.Interfaces;

namespace NewsPortal.Infrastructure.Repositories;

public class UserRepositoryImpl:IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepositoryImpl(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id) ?? throw new Exception();
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }
}