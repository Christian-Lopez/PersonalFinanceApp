using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Application.Common.Interfaces;
using PersonalFinanceApp.Domain.Enums;

namespace PersonalFinanceApp.Application.Features.Reports.Queries.GetCategorySpending;

public record GetCategorySpendingQuery(DateTime StartDate, DateTime EndDate, Guid? AccountId = null) : IRequest<List<CategorySpendingDto>>;

public class CategorySpendingDto
{
    public Guid? CategoryId { get; init; }
    public string? CategoryName { get; init; }
    public string? ColorHex { get; init; }
    public decimal TotalAmount { get; init; }
}

public class GetCategorySpendingQueryHandler : IRequestHandler<GetCategorySpendingQuery, List<CategorySpendingDto>>
{
    private readonly IApplicationDbContext _context;

    public GetCategorySpendingQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategorySpendingDto>> Handle(GetCategorySpendingQuery request, CancellationToken cancellationToken)
    {
        var startDate = request.StartDate.ToUniversalTime();
        var endDate = request.EndDate.ToUniversalTime();

        var query = _context.Transactions
            .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate && t.Type == TransactionType.Expense);

        if (request.AccountId.HasValue)
        {
            query = query.Where(t => t.AccountId == request.AccountId.Value);
        }

        var spendingByCategory = await query
            .GroupBy(t => t.CategoryId)
            .Select(g => new 
            {
                CategoryId = g.Key,
                Total = g.Sum(t => t.Amount)
            })
            .ToListAsync(cancellationToken);
            
        // Fetch matching categories separately to map names and colors
        var categoryIds = spendingByCategory
            .Where(x => x.CategoryId.HasValue)
            .Select(x => x.CategoryId!.Value)
            .ToList();
            
        var categories = await _context.Categories
            .Where(c => categoryIds.Contains(c.Id))
            .ToDictionaryAsync(c => c.Id, c => c, cancellationToken);
            
        return spendingByCategory.Select(s => new CategorySpendingDto
        {
            CategoryId = s.CategoryId,
            CategoryName = s.CategoryId.HasValue && categories.TryGetValue(s.CategoryId.Value, out var cat) 
                ? cat.Name : "Uncategorized",
            ColorHex = s.CategoryId.HasValue && categories.TryGetValue(s.CategoryId.Value, out var cat2) 
                ? cat2.ColorHex : "#808080",
            TotalAmount = s.Total
        })
        .OrderByDescending(x => x.TotalAmount)
        .ToList();
    }
}
