using PersonalFinanceApp.Domain.Common;
using PersonalFinanceApp.Domain.Enums;

namespace PersonalFinanceApp.Domain.Entities;

public class Account : BaseEntity
{
    public string UserId { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public AccountType Type { get; private set; }
    public decimal CurrentBalance { get; private set; }
    public string Currency { get; private set; } = null!;
    public bool IsActive { get; private set; }

    // Navigation property for EF Core
    private readonly List<Transaction> _transactions = [];
    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

    // Required by EF Core
    private Account() { }

    public Account(string userId, string name, AccountType type, decimal initialBalance, string currency = "USD")
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Account name is required.", nameof(name));

        if (string.IsNullOrWhiteSpace(currency) || currency.Trim().Length != 3)
            throw new ArgumentException("Currency must be a valid 3-character ISO code.", nameof(currency));

        Id = Guid.NewGuid();
        UserId = userId;
        Name = name.Trim();
        Type = type;
        CurrentBalance = initialBalance;
        Currency = currency.Trim().ToUpperInvariant();
        IsActive = true;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public void Credit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Credit amount must be greater than zero.");

        CurrentBalance += amount;
        SetUpdated();
    }

    public void Debit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Debit amount must be greater than zero.");

        // Credit cards can have negative balances, cash/checking cannot
        if (Type != AccountType.CreditCard && CurrentBalance - amount < 0)
            throw new InvalidOperationException("Insufficient funds for this transaction.");

        CurrentBalance -= amount;
        SetUpdated();
    }

    public void Deactivate()
    {
        IsActive = false;
        SetUpdated();
    }
    
    public void ApplyTransaction(TransactionType type, decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Transaction amount must be positive.", nameof(amount));

        CurrentBalance = type switch
        {
            TransactionType.Income => CurrentBalance + amount,
            TransactionType.Expense => CurrentBalance - amount,
            _ => CurrentBalance
        };

        UpdatedAtUtc = DateTime.UtcNow;
    }
}