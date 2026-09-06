using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Application.Common.Interfaces;

namespace PersonalFinanceApp.Application.Features.Tags.Commands.DeleteTag;

public record DeleteTagCommand(Guid Id) : IRequest;

public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public DeleteTagCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await _context.Tags
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
            ?? throw new ArgumentException($"Tag {request.Id} not found.");

        if (tag.UserId == null && !_currentUserService.IsAdmin)
        {
            throw new UnauthorizedAccessException("Only administrators can delete system tags.");
        }
        else if (tag.UserId != null && tag.UserId != _currentUserService.UserId && !_currentUserService.IsAdmin)
        {
            throw new UnauthorizedAccessException("You do not have permission to delete this tag.");
        }

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
