using PersonalFinanceApp.Domain.Common;
using PersonalFinanceApp.Domain.Enums;

namespace PersonalFinanceApp.Domain.Entities;

public class RecurringTransaction : BaseEntity
{
    public string UserId { get; private set; } = string.Empty;
    public Guid AccountId { get; private set; }
    public Guid? CategoryId { get; private set; }
    
    public decimal Amount { get; private set; }
    public TransactionType Type { get; private set; }
    public string Description { get; private set; } = string.Empty;
    
    public RecurringTransactionFrequency Frequency { get; private set; }
    
    public DateTime StartDate { get; private set; }
    public DateTime NextDueDate { get; set; }
    public DateTime? EndDate { get; private set; }
    
    public bool IsActive { get; set; }
    
    // Navigation properties
    public Account Account { get; private set; } = null!;
    public Category? Category { get; private set; }

    private RecurringTransaction() { } // EF Core

    public RecurringTransaction(
        string userId, 
        Guid accountId, 
        decimal amount, 
        TransactionType type, 
        string description, 
        RecurringTransactionFrequency frequency, 
        DateTime startDate, 
        Guid? categoryId)
    {
        if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("Required", nameof(userId));
        if (amount <= 0) throw new ArgumentException("Amount > 0 required", nameof(amount));

        UserId = userId;
        AccountId = accountId;
        Amount = amount;
        Type = type;
        Description = description;
        Frequency = frequency;
        StartDate = startDate.ToUniversalTime();
        NextDueDate = StartDate;
        CategoryId = categoryId;
        IsActive = true;
    }
}
