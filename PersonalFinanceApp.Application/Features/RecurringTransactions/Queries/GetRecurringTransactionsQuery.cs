using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Application.Common.Interfaces;
using PersonalFinanceApp.Domain.Enums;

namespace PersonalFinanceApp.Application.Features.RecurringTransactions.Queries;

public record RecurringTransactionDto(
    Guid Id,
    Guid AccountId,
    string AccountName,
    Guid? CategoryId,
    string? CategoryName,
    decimal Amount,
    TransactionType Type,
    string Description,
    RecurringTransactionFrequency Frequency,
    DateTime StartDate,
    DateTime NextDueDate,
    bool IsActive);

public record GetRecurringTransactionsQuery : IRequest<List<RecurringTransactionDto>>;

public class GetRecurringTransactionsQueryHandler : IRequestHandler<GetRecurringTransactionsQuery, List<RecurringTransactionDto>>
{
    private readonly IApplicationDbContext _context;

    public GetRecurringTransactionsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<RecurringTransactionDto>> Handle(GetRecurringTransactionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.RecurringTransactions
            .Include(rt => rt.Account)
            .Include(rt => rt.Category)
            .OrderBy(rt => rt.NextDueDate)
            .Select(rt => new RecurringTransactionDto(
                rt.Id,
                rt.AccountId,
                rt.Account.Name,
                rt.CategoryId,
                rt.Category != null ? rt.Category.Name : null,
                rt.Amount,
                rt.Type,
                rt.Description,
                rt.Frequency,
                rt.StartDate,
                rt.NextDueDate,
                rt.IsActive
            ))
            .ToListAsync(cancellationToken);
    }
}
