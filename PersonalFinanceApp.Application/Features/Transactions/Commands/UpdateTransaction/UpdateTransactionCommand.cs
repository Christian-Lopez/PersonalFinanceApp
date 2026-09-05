using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Application.Common.Interfaces;
using PersonalFinanceApp.Domain.Enums;

namespace PersonalFinanceApp.Application.Features.Transactions.Commands.UpdateTransaction;

public record UpdateTransactionCommand(
    Guid Id,
    decimal Amount,
    TransactionType Type,
    DateTime TransactionDate,
    string? Description,
    Guid? CategoryId
) : IRequest;

public class UpdateTransactionCommandValidator : AbstractValidator<UpdateTransactionCommand>
{
    public UpdateTransactionCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero.");
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.TransactionDate).NotEmpty();
    }
}

public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTransactionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _context.Transactions
            .Include(t => t.Account)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
            ?? throw new ArgumentException($"Transaction {request.Id} not found.");

        // Reverse old transaction effect
        transaction.Account.ReverseTransaction(transaction.Type, transaction.Amount);

        // Apply new transaction effect
        transaction.Account.ApplyTransaction(request.Type, request.Amount);

        // Update transaction entity
        transaction.Update(
            request.Amount,
            request.Type,
            request.TransactionDate,
            request.Description,
            request.CategoryId
        );

        await _context.SaveChangesAsync(cancellationToken);
    }
}
