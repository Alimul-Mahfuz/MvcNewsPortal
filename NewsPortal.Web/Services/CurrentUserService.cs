using NewsPortal.Application.Interfaces;

namespace NewsPortal.Web.Services;

public class CurrentUserService : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? UserId => _httpContextAccessor.HttpContext?.Session.GetInt32("UserId");
    public string? UserName => _httpContextAccessor.HttpContext?.Session.GetString("UserName");
    public string? Email => _httpContextAccessor.HttpContext?.Session.GetString("Email");
    public string? FullName => _httpContextAccessor.HttpContext?.Session.GetString("FullName");
    public string? Role => _httpContextAccessor.HttpContext?.Session.GetString("Role");
}