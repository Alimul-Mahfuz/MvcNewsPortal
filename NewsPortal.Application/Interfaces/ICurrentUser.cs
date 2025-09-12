namespace NewsPortal.Application.Interfaces;

public interface ICurrentUser
{
    int? UserId { get; }
    string? UserName { get; }
    string? Email { get; }
    string? FullName { get; }
    string? Role { get; }
}