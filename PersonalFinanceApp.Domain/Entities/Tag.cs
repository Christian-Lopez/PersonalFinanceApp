using PersonalFinanceApp.Domain.Common;

namespace PersonalFinanceApp.Domain.Entities;

public class Tag : BaseEntity
{
    public string Name { get; private set; } = string.Empty;

    // Navigation property for EF Core many-to-many
    private readonly List<Transaction> _transactions = [];
    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

    private Tag() { }

    public Tag(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tag name cannot be empty.", nameof(name));

        // Enforce normalized formatting (e.g., lowercase without '#')
        Name = name.Trim().TrimStart('#').ToLowerInvariant();
    }
}