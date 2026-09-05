namespace PersonalFinanceApp.Application.Common.Models;

public record AuthResult(
    bool Succeeded,
    string? Token,
    string? UserId,
    string? Email,
    IEnumerable<string> Errors
);