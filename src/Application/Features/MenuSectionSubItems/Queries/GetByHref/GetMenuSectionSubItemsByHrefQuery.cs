using CleanArchitecture.Blazor.Application.Features.MenuSectionItems.DTOs;
using CleanArchitecture.Blazor.Application.Features.MenuSections.DTOs;
using CleanArchitecture.Blazor.Application.Features.MenuSectionSubItems.DTOs;
using CleanArchitecture.Blazor.Application.Features.MenuSectionSubItems.Mappers;
using Dapper;

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
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetMenuSectionSubItemsByHrefQueryHandler(IApplicationDbContext context, ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _context = context;
    }

    public async Task<MenuSectionSubItemDto> Handle(GetMenuSectionSubItemsByHrefQuery request, CancellationToken cancellationToken)
    {
        var sql = $"""
                   SELECT
                       MS.Id, MS.Title, MS.Roles MenuSectionRoles, MS.SerialNo MenuSectionSerialNo, 
                       MSI.Title MenuSectionItemTitle, MSI.Icon, MSI.Href MenuSectionItemHref, MSI.IsParent, MSI.PageStatus MenuSectionItemPageStatus, MSI.SerialNo MenuSectionItemSerialNo,
                       MSSI.Id MenuSectionSubItemId, MSSI.MenuSectionItemId, MSSI.Title MenuSectionSubItemTitle, MSSI.Href MenuSectionSubItemHref, MSSI.Roles MenuSectionSubItemRoles,
                       MSSI.PageStatus MenuSectionSubItemPageStatus, MSSI.SerialNo MenuSectionSubItemSerialNo
                   FROM MenuSections MS
                   LEFT JOIN MenuSectionItems MSI ON MS.Id = MSI.MenuSectionId
                   LEFT JOIN MenuSectionSubItems MSSI ON MSI.Id = MSSI.MenuSectionItemId
                   WHERE 1 = 1
                   AND MSSI.Href = '{request.Href}'
                   """;
        var connection = _sqlConnectionFactory.GetOpenConnection();
        var rows = await connection.QueryAsync<MenuItemDto>(sql);
        
        var result = rows
            .GroupBy(r => new { r.Id, r.Title, r.MenuSectionRoles })
            .Select(sectionGroup => new MenuSectionDto
            {
                Id = sectionGroup.Key.Id,
                Title = sectionGroup.Key.Title,
                Roles = sectionGroup.Key.MenuSectionRoles,
                SectionItems = sectionGroup
                    .GroupBy(r => new { r.MenuSectionItemTitle, r.Icon, r.MenuSectionItemHref, r.IsParent, r.MenuSectionItemPageStatus, r.MenuSectionItemSerialNo })
                    .Select(itemGroup => new MenuSectionItemDto
                    {
                        Title = itemGroup.Key.MenuSectionItemTitle,
                        Icon = itemGroup.Key.Icon,
                        Href = itemGroup.Key.MenuSectionItemHref,
                        IsParent = itemGroup.Key.IsParent,
                        PageStatus = itemGroup.Key.MenuSectionItemPageStatus,
                        SerialNo = itemGroup.Key.MenuSectionItemSerialNo,
                        MenuItems = itemGroup
                            .Where(r => r.MenuSectionSubItemId != null)
                            .Select(r => new MenuSectionSubItemDto
                            {
                                Id = r.MenuSectionSubItemId,
                                MenuSectionItemId = r.MenuSectionItemId,
                                Title = r.MenuSectionSubItemTitle,
                                Href = r.MenuSectionSubItemHref,
                                Roles = r.MenuSectionSubItemRoles,
                                PageStatus = r.MenuSectionSubItemPageStatus,
                                SerialNo = r.MenuSectionSubItemSerialNo
                            }).ToList()
                    }).ToList()
            })
            .ToList();

        var data = await _context.MenuSectionSubItems
            .Include(e => e.MenuSectionItem)
            .ThenInclude(e => e.MenuSection)
            .ProjectTo()!
            .FirstOrDefaultAsync(e => e!.Href == request.Href, cancellationToken);
        return data!;
    }
}