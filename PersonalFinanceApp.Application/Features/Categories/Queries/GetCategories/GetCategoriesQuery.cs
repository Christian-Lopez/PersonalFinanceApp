using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Application.Common.Interfaces;

namespace PersonalFinanceApp.Application.Features.Categories.Queries.GetCategories;

public record GetCategoriesQuery() : IRequest<List<CategoryDto>>;

public class CategoryDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string ColorHex { get; init; } = null!;
    public string? Icon { get; init; }
    public Guid? ParentCategoryId { get; init; }
    public bool IsSystem { get; init; }
}

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
{
    private readonly IApplicationDbContext _context;

    public GetCategoriesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Categories
            .OrderBy(c => c.Name)
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                ColorHex = c.ColorHex,
                Icon = c.Icon,
                ParentCategoryId = c.ParentCategoryId,
                IsSystem = c.UserId == null
            })
            .ToListAsync(cancellationToken);
    }
}
