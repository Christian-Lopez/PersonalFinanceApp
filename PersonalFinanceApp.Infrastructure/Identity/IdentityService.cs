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

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<JwtSettings> jwtOptions)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtSettings = jwtOptions.Value;
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

        var token = await GenerateJwtTokenAsync(user);

        return new AuthResult(true, token, user.Id, user.Email, Enumerable.Empty<string>());
    }

    public async Task<AuthResult> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || !user.IsActive)
        {
            return new AuthResult(false, null, null, null, new[] { "Invalid email or password." });
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
        if (!isPasswordValid)
        {
            return new AuthResult(false, null, null, null, new[] { "Invalid email or password." });
        }

        var token = await GenerateJwtTokenAsync(user);

        return new AuthResult(true, token, user.Id, user.Email, Enumerable.Empty<string>());
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