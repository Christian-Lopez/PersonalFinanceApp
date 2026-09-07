using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Application.Common.Interfaces;
using PersonalFinanceApp.Domain.Enums;
using PersonalFinanceApp.Application.Features.Reports.Queries.GetMonthlySummary;

namespace PersonalFinanceApp.Application.Features.Reports.Queries.GetTrend;

public record GetTrendQuery(int Months = 6, Guid? AccountId = null) : IRequest<List<MonthlySummaryDto>>;

public class GetTrendQueryHandler : IRequestHandler<GetTrendQuery, List<MonthlySummaryDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTrendQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MonthlySummaryDto>> Handle(GetTrendQuery request, CancellationToken cancellationToken)
    {
        var endDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1, 0, 0, 0, DateTimeKind.Utc).AddMonths(1);
        var startDate = endDate.AddMonths(-request.Months);

        var query = _context.Transactions
            .Where(t => t.TransactionDate >= startDate && t.TransactionDate < endDate);

        if (request.AccountId.HasValue)
        {
            query = query.Where(t => t.AccountId == request.AccountId.Value);
        }

        var transactions = await query
            .Select(t => new { t.TransactionDate, t.Type, t.Amount })
            .ToListAsync(cancellationToken);

        var results = new List<MonthlySummaryDto>();

        for (int i = request.Months - 1; i >= 0; i--)
        {
            var targetMonth = endDate.AddMonths(-1 - i);
            var year = targetMonth.Year;
            var month = targetMonth.Month;

            var monthData = transactions
                .Where(t => t.TransactionDate.Year == year && t.TransactionDate.Month == month)
                .ToList();

            var income = monthData.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
            var expense = monthData.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);

            results.Add(new MonthlySummaryDto
            {
                Year = year,
                Month = month,
                TotalIncome = income,
                TotalExpense = expense
            });
        }

        return results;
    }
}
