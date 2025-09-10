namespace NewsPortal.Domain.Entities;

public enum UserRole
{
    NormalUser = 0,
    Editor = 1,
    Admin = 2
}

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; } 
    public string FullName { get; set; }
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

    public UserRole Role { get; set; } = UserRole.NormalUser;

    public ICollection<Article> Articles { get; set; } = new List<Article>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}