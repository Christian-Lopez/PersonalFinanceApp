using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Application.Common.Interfaces;
using PersonalFinanceApp.Domain.Entities;
using PersonalFinanceApp.Domain.Enums;

namespace PersonalFinanceApp.Application.Features.Transactions.Commands.CreateTransaction;

public record CreateTransactionCommand(
    Guid AccountId,
    decimal Amount,
    TransactionType Type,
    DateTime TransactionDate,
    string? Description,
    Guid? CategoryId
) : IRequest<Guid>;

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty();
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero.");
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.TransactionDate).NotEmpty();
    }
}

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateTransactionCommandHandler(
        IApplicationDbContext context, 
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId 
            ?? throw new UnauthorizedAccessException();

        // The query filter automatically ensures the user owns this account
        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.Id == request.AccountId, cancellationToken)
            ?? throw new ArgumentException($"Account {request.AccountId} not found.");

        var transaction = new Transaction(
            userId,
            request.AccountId,
            request.Amount,
            request.Type,
            request.TransactionDate,
            request.Description,
            request.CategoryId
        );

        // Synchronize balance atomically within the same transaction scope
        account.ApplyTransaction(request.Type, request.Amount);

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(cancellationToken);

        return transaction.Id;
    }
}