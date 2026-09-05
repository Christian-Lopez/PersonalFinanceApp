using FluentValidation;
using MediatR;
using PersonalFinanceApp.Application.Common.Interfaces;
using PersonalFinanceApp.Domain.Entities;
using PersonalFinanceApp.Domain.Enums;

namespace PersonalFinanceApp.Application.Features.Accounts.Commands.CreateAccount;

// 1. The Command (Request) - returns the Guid of the created account
public record CreateAccountCommand(
    string Name,
    AccountType Type,
    string Currency,
    decimal InitialBalance
) : IRequest<Guid>;

// 2. The Validator - runs validation rules before the handler executes
public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Account name is required.")
            .MaximumLength(100).WithMessage("Account name cannot exceed 100 characters.");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency code is required.")
            .Length(3).WithMessage("Currency must be a 3-character ISO code (e.g., USD, EUR).");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("A valid account type must be selected.");
    }
}

// 3. The Handler - handles the business logic and persistence
public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateAccountCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId
            ?? throw new UnauthorizedAccessException("Current user could not be identified.");

        var account = new Account(
            userId,
            request.Name,
            request.Type,
            request.InitialBalance,
            request.Currency
        );

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync(cancellationToken);

        return account.Id;
    }
}