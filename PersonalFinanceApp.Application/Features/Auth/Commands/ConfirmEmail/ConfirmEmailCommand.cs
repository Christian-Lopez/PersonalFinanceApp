using FluentValidation;
using MediatR;
using PersonalFinanceApp.Application.Common.Interfaces;
using PersonalFinanceApp.Application.Common.Models;

namespace PersonalFinanceApp.Application.Features.Auth.Commands.ConfirmEmail;

public record ConfirmEmailCommand(string Email, string Token) : IRequest<AuthResult>;

public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Token).NotEmpty();
    }
}

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, AuthResult>
{
    private readonly IIdentityService _identityService;

    public ConfirmEmailCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<AuthResult> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.ConfirmEmailAsync(request.Email, request.Token, cancellationToken);
    }
}
