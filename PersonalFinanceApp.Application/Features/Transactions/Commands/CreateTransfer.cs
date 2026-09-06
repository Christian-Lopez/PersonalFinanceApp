using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Application.Common.Interfaces;
using PersonalFinanceApp.Domain.Entities;
using PersonalFinanceApp.Domain.Enums;

namespace PersonalFinanceApp.Application.Features.Transactions.Commands.CreateTransfer;

public record CreateTransferCommand(
    Guid FromAccountId,
    Guid ToAccountId,
    decimal Amount,
    DateTime TransactionDate,
    string? Description
) : IRequest<bool>;

public class CreateTransferCommandValidator : AbstractValidator<CreateTransferCommand>
{
    public CreateTransferCommandValidator()
    {
        RuleFor(x => x.FromAccountId).NotEmpty();
        RuleFor(x => x.ToAccountId).NotEmpty().NotEqual(x => x.FromAccountId).WithMessage("Destination account must be different from source account.");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Transfer amount must be greater than zero.");
        RuleFor(x => x.TransactionDate).NotEmpty();
    }
}

public class CreateTransferCommandHandler : IRequestHandler<CreateTransferCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateTransferCommandHandler(
        IApplicationDbContext context, 
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

        var fromAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == request.FromAccountId, cancellationToken)
            ?? throw new ArgumentException($"Source account not found.");
            
        var toAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == request.ToAccountId, cancellationToken)
            ?? throw new ArgumentException($"Destination account not found.");

        string description = string.IsNullOrWhiteSpace(request.Description) ? "Transfer" : request.Description.Trim();

        // 1. Expense from Source
        var outTransaction = new Transaction(
            userId,
            request.FromAccountId,
            request.Amount,
            TransactionType.Transfer,
            request.TransactionDate,
            $"{description} (To {toAccount.Name})",
            null
        );
        fromAccount.ApplyTransaction(TransactionType.Expense, request.Amount);

        // 2. Income to Destination
        var inTransaction = new Transaction(
            userId,
            request.ToAccountId,
            request.Amount,
            TransactionType.Transfer,
            request.TransactionDate,
            $"{description} (From {fromAccount.Name})",
            null
        );
        toAccount.ApplyTransaction(TransactionType.Income, request.Amount);

        _context.Transactions.Add(outTransaction);
        _context.Transactions.Add(inTransaction);
        
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
