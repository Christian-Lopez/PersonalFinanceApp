using FluentValidation;
using MediatR;
using PersonalFinanceApp.Application.Common.Interfaces;
using PersonalFinanceApp.Domain.Entities;

namespace PersonalFinanceApp.Application.Features.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(
    string Name,
    string ColorHex,
    string? Icon,
    Guid? ParentCategoryId,
    bool IsSystem = false
) : IRequest<Guid>;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.ColorHex).NotEmpty();
    }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateCategoryCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

        // If user requests a system category, verify they are an admin
        string? targetUserId = currentUserId;
        if (request.IsSystem)
        {
            if (!_currentUserService.IsAdmin)
                throw new UnauthorizedAccessException("Only administrators can create system categories.");
            
            targetUserId = null;
        }

        var category = new Category(
            request.Name,
            targetUserId,
            request.ColorHex,
            request.Icon,
            request.ParentCategoryId
        );

        _context.Categories.Add(category);
        await _context.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
