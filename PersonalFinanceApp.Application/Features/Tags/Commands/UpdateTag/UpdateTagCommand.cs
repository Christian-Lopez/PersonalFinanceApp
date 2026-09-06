using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Application.Common.Interfaces;

namespace PersonalFinanceApp.Application.Features.Tags.Commands.UpdateTag;

public record UpdateTagCommand(
    Guid Id,
    string Name
) : IRequest;

public class UpdateTagCommandValidator : AbstractValidator<UpdateTagCommand>
{
    public UpdateTagCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
    }
}

public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateTagCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await _context.Tags
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
            ?? throw new ArgumentException($"Tag {request.Id} not found.");

        if (tag.UserId == null && !_currentUserService.IsAdmin)
        {
            throw new UnauthorizedAccessException("Only administrators can modify system tags.");
        }
        else if (tag.UserId != null && tag.UserId != _currentUserService.UserId && !_currentUserService.IsAdmin)
        {
            throw new UnauthorizedAccessException("You do not have permission to modify this tag.");
        }

        tag.Update(request.Name);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
