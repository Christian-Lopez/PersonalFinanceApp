using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Application.Common.Interfaces;

namespace PersonalFinanceApp.Application.Features.Tags.Queries.GetTags;

public record GetTagsQuery() : IRequest<List<TagDto>>;

public class TagDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public bool IsSystem { get; init; }
}

public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, List<TagDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTagsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TagDto>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Tags
            .OrderBy(t => t.Name)
            .Select(t => new TagDto
            {
                Id = t.Id,
                Name = t.Name,
                IsSystem = t.UserId == null
            })
            .ToListAsync(cancellationToken);
    }
}
