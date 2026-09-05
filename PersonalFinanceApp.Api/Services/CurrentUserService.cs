using System.Security.Claims;
using PersonalFinanceApp.Application.Common.Interfaces;

namespace PersonalFinanceApp.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId =>
        _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? _httpContextAccessor.HttpContext?.Request.Headers["X-User-Id"].FirstOrDefault()
        ?? "dev-user-default-guid";

    public bool IsAdmin =>
        _httpContextAccessor.HttpContext?.User?.IsInRole("Admin") ?? false;
}