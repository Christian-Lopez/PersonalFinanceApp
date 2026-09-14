using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Application.Common.Interfaces;
using PersonalFinanceApp.Domain.Entities;
using PersonalFinanceApp.Domain.Enums;

namespace PersonalFinanceApp.Application.Features.RecurringTransactions.Commands;

public record ProcessRecurringTransactionsCommand : IRequest<int>;

public class ProcessRecurringTransactionsCommandHandler : IRequestHandler<ProcessRecurringTransactionsCommand, int>
{
    private readonly IApplicationDbContext _context;

    public ProcessRecurringTransactionsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(ProcessRecurringTransactionsCommand request, CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow.Date;
        var generatedCount = 0;

        // Find all active subscriptions where NextDueDate is today or in the past
        var dueSubscriptions = await _context.RecurringTransactions
            .Where(rt => rt.IsActive && rt.NextDueDate.Date <= today)
            .ToListAsync(cancellationToken);

        foreach (var sub in dueSubscriptions)
        {
            // Catch-up loop: keep processing until NextDueDate is in the future
            while (sub.NextDueDate.Date <= today)
            {
                // Unique fingerprint to ensure idempotency
                var referenceId = $"REC_{sub.Id}_{sub.NextDueDate:yyyyMMdd}";
                
                // Safety net: Check if this specific transaction already exists
                var alreadyProcessed = await _context.Transactions
                    .AnyAsync(t => t.ReferenceId == referenceId, cancellationToken);
                
                if (!alreadyProcessed)
                {
                    var transaction = new Transaction(
                        userId: sub.UserId,
                        accountId: sub.AccountId,
                        amount: sub.Amount,
                        type: sub.Type,
                        transactionDate: sub.NextDueDate,
                        description: sub.Description,
                        categoryId: sub.CategoryId,
                        referenceId: referenceId
                    );
                    
                    _context.Transactions.Add(transaction);
                    generatedCount++;
                }

                // Move the goalpost (Calculate next due date)
                sub.NextDueDate = CalculateNextDueDate(sub.NextDueDate, sub.Frequency);
                
                // If it has an end date and we surpassed it, deactivate it
                if (sub.EndDate.HasValue && sub.NextDueDate.Date > sub.EndDate.Value.Date)
                {
                    sub.IsActive = false;
                    break;
                }
            }
        }

        if (generatedCount > 0 || dueSubscriptions.Any())
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        return generatedCount;
    }

    private DateTime CalculateNextDueDate(DateTime current, RecurringTransactionFrequency frequency)
    {
        return frequency switch
        {
            RecurringTransactionFrequency.Daily => current.AddDays(1),
            RecurringTransactionFrequency.Weekly => current.AddDays(7),
            RecurringTransactionFrequency.Monthly => current.AddMonths(1),
            RecurringTransactionFrequency.Yearly => current.AddYears(1),
            _ => current.AddMonths(1) // Fallback
        };
    }
}
