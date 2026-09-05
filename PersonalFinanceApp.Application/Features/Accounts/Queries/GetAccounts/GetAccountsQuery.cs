using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Application.Common.Interfaces;
using PersonalFinanceApp.Domain.Enums;

namespace PersonalFinanceApp.Application.Features.Accounts.Queries.GetAccounts;

// 1. Output DTO
public record AccountDto(
    Guid Id,
    string Name,
    AccountType Type,
    decimal Balance,
    string Currency,
    bool IsActive,
    DateTime CreatedAtUtc
);

// 2. The Query
public record GetAccountsQuery : IRequest<IReadOnlyList<AccountDto>>;

// 3. The Query Handler
public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, IReadOnlyList<AccountDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAccountsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<AccountDto>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Accounts
            .AsNoTracking()
            .OrderBy(a => a.Name)
            .Select(a => new AccountDto(
                a.Id,
                a.Name,
                a.Type,
                a.CurrentBalance,
                a.Currency,
                a.IsActive,
                a.CreatedAtUtc
            ))
            .ToListAsync(cancellationToken);
    }
}