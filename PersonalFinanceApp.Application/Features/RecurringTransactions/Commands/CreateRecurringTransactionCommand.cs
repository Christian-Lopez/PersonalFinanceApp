using FluentValidation;
using MediatR;
using PersonalFinanceApp.Application.Common.Interfaces;
using PersonalFinanceApp.Domain.Entities;
using PersonalFinanceApp.Domain.Enums;

namespace PersonalFinanceApp.Application.Features.RecurringTransactions.Commands;

public record CreateRecurringTransactionCommand(
    Guid AccountId,
    decimal Amount,
    TransactionType Type,
    string Description,
    RecurringTransactionFrequency Frequency,
    DateTime StartDate,
    Guid? CategoryId) : IRequest<Guid>;

public class CreateRecurringTransactionCommandValidator : AbstractValidator<CreateRecurringTransactionCommand>
{
    public CreateRecurringTransactionCommandValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty();
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.Description).MaximumLength(500);
        RuleFor(x => x.Frequency).IsInEnum();
        RuleFor(x => x.Type).IsInEnum();
    }
}

public class CreateRecurringTransactionCommandHandler : IRequestHandler<CreateRecurringTransactionCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateRecurringTransactionCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateRecurringTransactionCommand request, CancellationToken cancellationToken)
    {
        var recurringTx = new RecurringTransaction(
            _currentUserService.UserId!,
            request.AccountId,
            request.Amount,
            request.Type,
            request.Description,
            request.Frequency,
            request.StartDate,
            request.CategoryId
        );

        _context.RecurringTransactions.Add(recurringTx);
        await _context.SaveChangesAsync(cancellationToken);

        return recurringTx.Id;
    }
}
