using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Application.Common.Interfaces;

namespace PersonalFinanceApp.Application.Features.Transactions.Commands.DeleteTransaction;

public record DeleteTransactionCommand(Guid Id) : IRequest;

public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTransactionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _context.Transactions
            .Include(t => t.Account)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
            ?? throw new ArgumentException($"Transaction {request.Id} not found.");

        // Reverse transaction effect
        transaction.Account.ReverseTransaction(transaction.Type, transaction.Amount);

        _context.Transactions.Remove(transaction);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
