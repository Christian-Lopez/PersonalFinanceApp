using PersonalFinanceApp.Domain.Common;

namespace PersonalFinanceApp.Domain.Entities;

public class Tag : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string UserId { get; private set; } = null!;

    // Navigation property for EF Core many-to-many
    private readonly List<Transaction> _transactions = [];
    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

    private Tag() { }

    public Tag(string name, string userId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tag name cannot be empty.", nameof(name));

        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));

        // Enforce normalized formatting (e.g., lowercase without '#')
        Name = name.Trim().TrimStart('#').ToLowerInvariant();
        UserId = userId;
    }

    public void Update(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tag name cannot be empty.", nameof(name));

        Name = name.Trim().TrimStart('#').ToLowerInvariant();
        SetUpdated();
    }
}