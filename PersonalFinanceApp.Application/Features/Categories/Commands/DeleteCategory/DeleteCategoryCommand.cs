using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Application.Common.Interfaces;

namespace PersonalFinanceApp.Application.Features.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(Guid Id) : IRequest;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public DeleteCategoryCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
            ?? throw new ArgumentException($"Category {request.Id} not found.");

        if (category.UserId == null && !_currentUserService.IsAdmin)
        {
            throw new UnauthorizedAccessException("Only administrators can delete system categories.");
        }
        else if (category.UserId != null && category.UserId != _currentUserService.UserId && !_currentUserService.IsAdmin)
        {
            throw new UnauthorizedAccessException("You do not have permission to delete this category.");
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
