using PersonalFinanceApp.Domain.Common;
using PersonalFinanceApp.Domain.Enums;

namespace PersonalFinanceApp.Domain.Entities;

public class Transaction : BaseEntity
{
   public string UserId { get; private set; } = null!;
    public Guid AccountId { get; private set; }
    public decimal Amount { get; private set; }
    public TransactionType Type { get; private set; }
    public DateTime TransactionDate { get; private set; }
    public string? Description { get; private set; }
    public Guid? CategoryId { get; private set; }
    public string? ReferenceId { get; private set; } // Used to prevent duplicate recurring transactions

    // Navigation properties
    public Account Account { get; private set; } = null!;
    public Category? Category { get; private set; }

    private Transaction() { }

    public Transaction(string userId, Guid accountId, decimal amount, TransactionType type, DateTime transactionDate, string? description = null, Guid? categoryId = null, string? referenceId = null)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));
        if (amount <= 0)
            throw new ArgumentException("Amount must be strictly positive.", nameof(amount));

        Id = Guid.NewGuid();
        UserId = userId;
        AccountId = accountId;
        Amount = amount;
        Type = type;
        TransactionDate = transactionDate.ToUniversalTime();
        Description = description?.Trim();
        CategoryId = categoryId;
        ReferenceId = referenceId;
        CreatedAtUtc = DateTime.UtcNow;
    }
    public void Update(decimal amount, TransactionType type, DateTime transactionDate, string? description, Guid? categoryId)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be strictly positive.", nameof(amount));

        Amount = amount;
        Type = type;
        TransactionDate = transactionDate.ToUniversalTime();
        Description = description?.Trim();
        CategoryId = categoryId;
        SetUpdated();
    }

    // For tags:

    private readonly List<Tag> _tags = [];
    public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

    public void AddTag(Tag tag)
    {
        ArgumentNullException.ThrowIfNull(tag);

        if (!_tags.Any(t => t.Id == tag.Id))
        {
            _tags.Add(tag);
            SetUpdated();
        }
    }

    public void RemoveTag(Guid tagId)
    {
        var existing = _tags.FirstOrDefault(t => t.Id == tagId);
        if (existing != null)
        {
            _tags.Remove(existing);
            SetUpdated();
        }
    }
}