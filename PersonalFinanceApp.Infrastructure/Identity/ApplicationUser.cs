using Microsoft.AspNetCore.Identity;

namespace PersonalFinanceApp.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string DefaultCurrency { get; set; } = "USD";
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAtUtc { get; set; }
}