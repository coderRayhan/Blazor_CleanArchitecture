using CleanArchitecture.Blazor.Application.Features.MenuSectionSubItems.DTOs;
using CleanArchitecture.Blazor.Application.Features.MenuSectionSubItems.Mappers;

namespace CleanArchitecture.Blazor.Application.Features.MenuSectionSubItems.Queries.GetByHref;

public class GetMenuSectionSubItemsByHrefQuery
    : IRequest<MenuSectionSubItemDto>
{
    public required string Href { get; set; }
}

public class GetMenuSectionSubItemsByHrefQueryHandler 
    : IRequestHandler<GetMenuSectionSubItemsByHrefQuery, MenuSectionSubItemDto>
{
    private readonly IApplicationDbContext _context;

    public GetMenuSectionSubItemsByHrefQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MenuSectionSubItemDto> Handle(GetMenuSectionSubItemsByHrefQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.MenuSectionSubItems
            .Include(e => e.MenuSectionItem)
            .ThenInclude(e => e.MenuSection)
            .ProjectTo()!
            .FirstOrDefaultAsync(e => e!.Href == request.Href, cancellationToken);
        return data!;
    }
}