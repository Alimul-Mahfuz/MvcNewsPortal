using NewsPortal.Domain.Entities;
using NewsPortal.Infrastructure.Data;

namespace NewsPortal.Infrastructure.Seeder;

public class UserSeeder
{
    public static void SeedUsers(ApplicationDbContext db)
    {
        if (db.Users.Any()) return; 

        db.Users.AddRange(
            new User
            {
                Username = "admin",
                Email = "admin@example.com",
                FullName = "Admin User",
                Role = UserRole.Admin,
                PasswordHash = HashPassword("12345")
            },
            new User
            {
                Username = "editor",
                Email = "editor@example.com",
                FullName = "Editor User",
                Role = UserRole.Editor,
                PasswordHash = HashPassword("12345")
            }
        );

        db.SaveChanges();
    }

    private static string HashPassword(string password)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var bytes = System.Text.Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}