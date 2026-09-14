using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Application.Common.Interfaces;

namespace PersonalFinanceApp.Application.Features.RecurringTransactions.Commands;

public record CancelRecurringTransactionCommand(Guid Id) : IRequest;

public class CancelRecurringTransactionCommandHandler : IRequestHandler<CancelRecurringTransactionCommand>
{
    private readonly IApplicationDbContext _context;

    public CancelRecurringTransactionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CancelRecurringTransactionCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _context.RecurringTransactions
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            
        if (subscription == null)
            throw new KeyNotFoundException($"Recurring transaction {request.Id} not found.");

        subscription.IsActive = false;
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}
