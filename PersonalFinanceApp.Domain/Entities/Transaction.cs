using PersonalFinanceApp.Domain.Common;
using PersonalFinanceApp.Domain.Enums;

namespace PersonalFinanceApp.Domain.Entities;

public class Transaction : BaseEntity
{
    public Guid AccountId { get; private set; }
    public Guid? CategoryId { get; private set; }
    public decimal Amount { get; private set; }
    public TransactionType Type { get; private set; }
    public DateTime TransactionDateUtc { get; private set; }
    public string Description { get; private set; } = string.Empty;

    // Navigation properties
    public Account Account { get; private set; } = null!;
    public Category? Category { get; private set; }

    private Transaction() { }

    public Transaction(
        Guid accountId, 
        decimal amount, 
        TransactionType type, 
        DateTime transactionDateUtc, 
        string description, 
        Guid? categoryId = null)
    {
        if (accountId == Guid.Empty)
            throw new ArgumentException("Transaction must be linked to a valid account.", nameof(accountId));

        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");

        AccountId = accountId;
        Amount = amount;
        Type = type;
        TransactionDateUtc = transactionDateUtc;
        Description = description?.Trim() ?? string.Empty;
        CategoryId = categoryId;
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