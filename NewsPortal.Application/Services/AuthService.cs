using NewsPortal.Domain.Entities;
using NewsPortal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace NewsPortal.Application.Services;

public class AuthService
{
    private readonly ApplicationDbContext _db;

    public AuthService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<User> Register(string username, string email, string password, string fullName, UserRole role = UserRole.NormalUser)
    {
        var user = new User
        {
            Username = username,
            Email = email,
            FullName = fullName,
            Role = role,
            PasswordHash = HashPassword(password)
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<User?> Login(string email, string password)
    {
        var hash = HashPassword(password);
        return await _db.Users.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == hash);
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}