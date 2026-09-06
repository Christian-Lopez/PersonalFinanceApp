using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Application.Common.Interfaces;

namespace PersonalFinanceApp.Application.Features.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(
    Guid Id,
    string Name,
    string ColorHex,
    string? Icon,
    Guid? ParentCategoryId
) : IRequest;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.ColorHex).NotEmpty();
    }
}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateCategoryCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
            ?? throw new ArgumentException($"Category {request.Id} not found.");

        // Security check: Only the owner or an Admin can modify a category.
        // Global categories (UserId == null) can ONLY be modified by Admins.
        if (category.UserId == null && !_currentUserService.IsAdmin)
        {
            throw new UnauthorizedAccessException("Only administrators can modify system categories.");
        }
        else if (category.UserId != null && category.UserId != _currentUserService.UserId && !_currentUserService.IsAdmin)
        {
            throw new UnauthorizedAccessException("You do not have permission to modify this category.");
        }

        category.UpdateDetails(request.Name, request.ColorHex, request.Icon);
        
        if (category.ParentCategoryId != request.ParentCategoryId)
        {
            category.MoveToParent(request.ParentCategoryId);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
