using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Application.Common.Interfaces;

namespace PersonalFinanceApp.Application.Features.Transactions.Queries.GetTransactions;

public record GetTransactionsQuery(Guid AccountId) : IRequest<List<TransactionDto>>;

public class TransactionDto
{
    public Guid Id { get; init; }
    public decimal Amount { get; init; }
    public string Type { get; init; } = null!;
    public DateTime TransactionDate { get; init; }
    public string? Description { get; init; }
    public Guid? CategoryId { get; init; }
    public List<TransactionTagDto> Tags { get; init; } = [];
}

public class TransactionTagDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
}

public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, List<TransactionDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetTransactionsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<List<TransactionDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

        var accountExists = await _context.Accounts.AnyAsync(a => a.Id == request.AccountId, cancellationToken);
        if (!accountExists)
            throw new ArgumentException($"Account {request.AccountId} not found.");

        return await _context.Transactions
            .Where(t => t.AccountId == request.AccountId)
            .OrderByDescending(t => t.TransactionDate)
            .Select(t => new TransactionDto
            {
                Id = t.Id,
                Amount = t.Amount,
                Type = t.Type.ToString(),
                TransactionDate = t.TransactionDate,
                Description = t.Description,
                CategoryId = t.CategoryId,
                Tags = t.Tags.Select(tag => new TransactionTagDto
                {
                    Id = tag.Id,
                    Name = tag.Name
                }).ToList()
            })
            .ToListAsync(cancellationToken);
    }
}
