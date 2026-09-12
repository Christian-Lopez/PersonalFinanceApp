using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PersonalFinanceApp.Application.Common.Interfaces;
using PersonalFinanceApp.Application.Common.Models;

namespace PersonalFinanceApp.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JwtSettings _jwtSettings;
    private readonly IEmailService _emailService;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<JwtSettings> jwtOptions,
        IEmailService emailService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtSettings = jwtOptions.Value;
        _emailService = emailService;
    }

    public async Task<AuthResult> RegisterAsync(
        string email,
        string password,
        string firstName,
        string lastName,
        string defaultCurrency,
        CancellationToken cancellationToken = default)
    {
        var existingUser = await _userManager.FindByEmailAsync(email);
        if (existingUser != null)
        {
            return new AuthResult(false, null, null, null, new[] { "A user with this email already exists." });
        }

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            DefaultCurrency = defaultCurrency.Trim().ToUpperInvariant(),
            CreatedAtUtc = DateTime.UtcNow,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            return new AuthResult(false, null, null, null, result.Errors.Select(e => e.Description));
        }

        // Ensure default "User" role exists and assign
        if (!await _roleManager.RoleExistsAsync("User"))
        {
            await _roleManager.CreateAsync(new IdentityRole("User"));
        }
        await _userManager.AddToRoleAsync(user, "User");

        // Generate email confirmation token
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        
        // Send email
        var encodedEmail = System.Web.HttpUtility.UrlEncode(email);
        var encodedToken = System.Web.HttpUtility.UrlEncode(token);
        var verifyLink = $"http://localhost:5173/verify-email?email={encodedEmail}&token={encodedToken}";
        
        var htmlBody = $@"
            <h2>Welcome to Personal Finance App!</h2>
            <p>Hi {firstName},</p>
            <p>Please confirm your email address by clicking the link below:</p>
            <p><a href='{verifyLink}'>Verify Email</a></p>
            <br/>
            <p>If you did not request this, you can safely ignore this email.</p>";

        await _emailService.SendEmailAsync(email, "Verify Your Email - Personal Finance App", htmlBody, cancellationToken);

        // Do not log them in automatically - return success but null token
        return new AuthResult(true, null, user.Id, user.Email, Enumerable.Empty<string>());
    }

    public async Task<AuthResult> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || !user.IsActive)
        {
            return new AuthResult(false, null, null, null, new[] { "Invalid email or password." });
        }

        if (!await _userManager.IsEmailConfirmedAsync(user))
        {
            return new AuthResult(false, null, null, null, new[] { "EmailNotConfirmed: Please verify your email before logging in." });
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
        if (!isPasswordValid)
        {
            return new AuthResult(false, null, null, null, new[] { "Invalid email or password." });
        }

        var token = await GenerateJwtTokenAsync(user);

        return new AuthResult(true, token, user.Id, user.Email, Enumerable.Empty<string>());
    }

    public async Task<AuthResult> ConfirmEmailAsync(string email, string token, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return new AuthResult(false, null, null, null, new[] { "Invalid email." });
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
        {
            return new AuthResult(false, null, null, null, result.Errors.Select(e => e.Description));
        }

        return new AuthResult(true, null, null, null, Enumerable.Empty<string>());
    }

    public async Task<AuthResult> ForgotPasswordAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || !user.IsActive)
        {
            // Do not reveal that the user does not exist for security reasons
            return new AuthResult(true, null, null, null, Enumerable.Empty<string>());
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        
        var encodedEmail = System.Web.HttpUtility.UrlEncode(email);
        var encodedToken = System.Web.HttpUtility.UrlEncode(token);
        var resetLink = $"http://localhost:5173/reset-password?email={encodedEmail}&token={encodedToken}";

        var htmlBody = $@"
            <h2>Password Reset Request</h2>
            <p>We received a request to reset your password.</p>
            <p>Click the link below to set a new password:</p>
            <p><a href='{resetLink}'>Reset Password</a></p>
            <br/>
            <p>If you did not request this, you can safely ignore this email.</p>";

        await _emailService.SendEmailAsync(email, "Password Reset Request", htmlBody, cancellationToken);
        
        return new AuthResult(true, null, null, null, Enumerable.Empty<string>());
    }

    public async Task<AuthResult> ResetPasswordAsync(string email, string token, string newPassword, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return new AuthResult(false, null, null, null, new[] { "Invalid request." });
        }

        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
        if (!result.Succeeded)
        {
            return new AuthResult(false, null, null, null, result.Errors.Select(e => e.Description));
        }

        return new AuthResult(true, null, null, null, Enumerable.Empty<string>());
    }

    public async Task<AuthResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return new AuthResult(false, null, null, null, new[] { "User not found." });
        }

        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        if (!result.Succeeded)
        {
            return new AuthResult(false, null, null, null, result.Errors.Select(e => e.Description));
        }

        return new AuthResult(true, null, null, null, Enumerable.Empty<string>());
    }

    private async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("firstName", user.FirstName),
            new("lastName", user.LastName),
            new("defaultCurrency", user.DefaultCurrency)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}