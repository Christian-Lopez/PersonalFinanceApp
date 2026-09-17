using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Application.Common.Interfaces;
using PersonalFinanceApp.Infrastructure.Identity;

namespace PersonalFinanceApp.Api.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationDbContext _context;

    public AdminController(UserManager<ApplicationUser> userManager, IApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userManager.Users.ToListAsync();
        var adminRoleUsers = await _userManager.GetUsersInRoleAsync("Admin");
        var adminUserIds = adminRoleUsers.Select(u => u.Id).ToHashSet();

        // Get counts
        var userStats = await _context.Accounts
            .GroupBy(a => a.UserId)
            .Select(g => new { UserId = g.Key, AccountCount = g.Count() })
            .ToDictionaryAsync(x => x.UserId, x => x.AccountCount);

        var txStats = await _context.Transactions
            .GroupBy(t => t.UserId)
            .Select(g => new { UserId = g.Key, TransactionCount = g.Count() })
            .ToDictionaryAsync(x => x.UserId, x => x.TransactionCount);

        var result = users.Select(u => new
        {
            u.Id,
            u.Email,
            u.FirstName,
            u.LastName,
            u.IsActive,
            u.CreatedAtUtc,
            u.LastLoginAtUtc,
            IsAdmin = adminUserIds.Contains(u.Id),
            AccountCount = userStats.GetValueOrDefault(u.Id, 0),
            TransactionCount = txStats.GetValueOrDefault(u.Id, 0)
        }).OrderByDescending(u => u.CreatedAtUtc).ToList();

        return Ok(result);
    }

    [HttpPost("users/{id}/toggle-lock")]
    public async Task<IActionResult> ToggleUserLock(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound("User not found");

        var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
        if (isAdmin) return BadRequest("Cannot lock an admin account.");

        user.IsActive = !user.IsActive;
        await _userManager.UpdateAsync(user);

        return Ok(new { user.Id, user.IsActive });
    }

    [HttpPost("users/{id}/toggle-admin")]
    public async Task<IActionResult> ToggleAdmin(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound("User not found");

        var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
        
        if (isAdmin)
        {
            // Simple safeguard to prevent removing the only admin (can be improved)
            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            if (admins.Count <= 1) return BadRequest("Cannot remove the last admin.");
            
            await _userManager.RemoveFromRoleAsync(user, "Admin");
        }
        else
        {
            await _userManager.AddToRoleAsync(user, "Admin");
        }

        return Ok(new { user.Id, IsAdmin = !isAdmin });
    }

    [HttpPost("users/{id}/reset-password")]
    public async Task<IActionResult> ResetPassword(string id, [FromBody] ResetPasswordRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.NewPassword)) return BadRequest("Password is required.");

        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound("User not found");

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors.Select(e => e.Description));
        }

        return Ok(new { message = "Password reset successfully." });
    }

    [HttpPost("users/{id}/change-email")]
    public async Task<IActionResult> ChangeEmail(string id, [FromBody] ChangeEmailRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.NewEmail)) return BadRequest("Email is required.");

        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound("User not found");

        var existingEmail = await _userManager.FindByEmailAsync(request.NewEmail);
        if (existingEmail != null && existingEmail.Id != user.Id)
        {
            return BadRequest("Email is already in use by another account.");
        }

        var token = await _userManager.GenerateChangeEmailTokenAsync(user, request.NewEmail);
        var result = await _userManager.ChangeEmailAsync(user, request.NewEmail, token);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors.Select(e => e.Description));
        }

        // Must update UserName too for ASP.NET Identity to allow login properly if login is by UserName
        await _userManager.SetUserNameAsync(user, request.NewEmail);
        
        // Let's set it as confirmed since an Admin changed it manually
        user.EmailConfirmed = true;
        await _userManager.UpdateAsync(user);

        return Ok(new { message = "Email changed successfully." });
    }
}

public class ResetPasswordRequest
{
    public string NewPassword { get; set; } = null!;
}

public class ChangeEmailRequest
{
    public string NewEmail { get; set; } = null!;
}
