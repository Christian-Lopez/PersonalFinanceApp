using PersonalFinanceApp.Application.Common.Models;

namespace PersonalFinanceApp.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<AuthResult> RegisterAsync(
        string email, 
        string password, 
        string firstName, 
        string lastName, 
        string defaultCurrency, 
        CancellationToken cancellationToken = default);

    Task<AuthResult> LoginAsync(
        string email, 
        string password, 
        CancellationToken cancellationToken = default);

    Task<AuthResult> ForgotPasswordAsync(string email, CancellationToken cancellationToken = default);
    
    Task<AuthResult> ResetPasswordAsync(string email, string token, string newPassword, CancellationToken cancellationToken = default);
}