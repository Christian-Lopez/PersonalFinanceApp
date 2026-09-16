using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Application.Common.Interfaces;
using PersonalFinanceApp.Domain.Enums;

namespace PersonalFinanceApp.Application.Features.Reports.Queries.GetMonthlySummary;

public record GetMonthlySummaryQuery(DateTime StartDate, DateTime EndDate, Guid? AccountId = null) : IRequest<MonthlySummaryDto>;

public class MonthlySummaryDto
{
    public int Year { get; init; }
    public int Month { get; init; }
    public decimal TotalIncome { get; init; }
    public decimal TotalExpense { get; init; }
    public decimal NetSavings => TotalIncome - TotalExpense;
}

public class GetMonthlySummaryQueryHandler : IRequestHandler<GetMonthlySummaryQuery, MonthlySummaryDto>
{
    private readonly IApplicationDbContext _context;

    public GetMonthlySummaryQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MonthlySummaryDto> Handle(GetMonthlySummaryQuery request, CancellationToken cancellationToken)
    {
        var startDate = request.StartDate.ToUniversalTime();
        var endDate = request.EndDate.ToUniversalTime();

        var query = _context.Transactions
            .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate);

        if (request.AccountId.HasValue)
        {
            query = query.Where(t => t.AccountId == request.AccountId.Value);
        }

        // Note: EF Core Query Filter automatically scopes this to the current authenticated user.
        var totals = await query
            .GroupBy(t => t.Type)
            .Select(g => new { Type = g.Key, Total = g.Sum(t => t.Amount) })
            .ToListAsync(cancellationToken);

        var income = totals.FirstOrDefault(t => t.Type == TransactionType.Income)?.Total ?? 0m;
        var expense = totals.FirstOrDefault(t => t.Type == TransactionType.Expense)?.Total ?? 0m;

        return new MonthlySummaryDto
        {
            Year = startDate.Year,
            Month = startDate.Month,
            TotalIncome = income,
            TotalExpense = expense
        };
    }
}
