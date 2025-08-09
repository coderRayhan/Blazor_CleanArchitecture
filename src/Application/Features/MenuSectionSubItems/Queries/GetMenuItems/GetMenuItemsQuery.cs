using CleanArchitecture.Blazor.Application.Common.Interfaces.Contracts;
using CleanArchitecture.Blazor.Application.Common.Interfaces.Identity;
using CleanArchitecture.Blazor.Application.Features.MenuSectionItems.DTOs;
using CleanArchitecture.Blazor.Application.Features.MenuSections.DTOs;
using CleanArchitecture.Blazor.Application.Features.MenuSectionSubItems.DTOs;
using Dapper;

namespace CleanArchitecture.Blazor.Application.Features.MenuSectionSubItems.Queries.GetMenuItems;
public sealed record GetMenuItemsQuery : IQuery<List<MenuSectionDto>>;

internal sealed class GetMenuItemsQueryHandler(ISqlConnectionFactory sqlConnection, ICurrentUserContext currentUserContext)
    : IQueryHandler<GetMenuItemsQuery, List<MenuSectionDto>>
{
    public async Task<Result<List<MenuSectionDto>>> Handle(GetMenuItemsQuery request, CancellationToken cancellationToken)
    {
        var userId = currentUserContext.SessionInfo.UserId ?? string.Empty;
        var sql = $"""
                   WITH Menu_Role_Data AS
                   (
                       SELECT MSSI.Id MenuId, LTRIM(RTRIM(S.[value])) RoleName, R.Id RoleId
                       FROM MenuSectionSubItems MSSI
                       CROSS APPLY STRING_SPLIT(MSSI.Roles, ',') S
                       INNER JOIN AspNetRoles R ON LTRIM(RTRIM(s.[value])) = R.Name
                   )
                   SELECT DISTINCT
                     MS.Id, MS.Title, MS.Roles MenuSectionRoles, MS.SerialNo MenuSectionSerialNo, 
                     MSI.Title MenuSectionItemTitle, MSI.Icon, MSI.Href MenuSectionItemHref, MSI.IsParent, MSI.PageStatus MenuSectionItemPageStatus, MSI.SerialNo MenuSectionItemSerialNo,
                     MSSI.Id MenuSectionSubItemId, MSSI.MenuSectionItemId, MSSI.Title MenuSectionSubItemTitle, MSSI.Href MenuSectionSubItemHref, MSSI.Roles MenuSectionSubItemRoles,
                     MSSI.PageStatus MenuSectionSubItemPageStatus, MSSI.SerialNo MenuSectionSubItemSerialNo
                   FROM MenuSections MS
                   INNER JOIN MenuSectionItems MSI ON MS.Id = MSI.MenuSectionId
                   LEFT JOIN MenuSectionSubItems MSSI ON MSI.Id = MSSI.MenuSectionItemId
                   INNER JOIN Menu_Role_Data MRD ON MSSI.Id = MRD.MenuId
                   INNER JOIN AspNetUserRoles UR ON MRD.RoleId = UR.RoleId AND UR.UserId = '{userId}'
                   UNION ALL 
                   SELECT MS.Id, MS.Title, MS.Roles MenuSectionRoles, MS.SerialNo MenuSectionSerialNo,
                   MSI.Title MenuSectionItemTitle, MSI.Icon, MSI.Href MenuSectionItemHref, MSI.IsParent, MSI.PageStatus MenuSectionItemPageStatus, MSI.SerialNo MenuSectionItemSerialNo,
                   0 MenuSectionSubItemId, 0 MenuSectionItemId, null MenuSectionSubItemTitle, null MenuSectionSubItemHref, null MenuSectionSubItemRoles, null MenuSectionSubItemPageStatus,
                   null MenuSectionSubItemSerialNo
                   FROM MenuSectionItems MSI
                   INNER JOIN MenuSections MS ON MSI.MenuSectionId = MS.Id
                   WHERE MSI.IsParent = 0
                   """;
        
        var connection = sqlConnection.GetOpenConnection();
        
        var rows = connection.Query<MenuItemDto>(sql);
        var result = rows
            .GroupBy(r => new { r.Id, r.Title, r.MenuSectionRoles, r.MenuSectionSerialNo })
            .Select(sectionGroup => new MenuSectionDto
            {
                Id = sectionGroup.Key.Id,
                Title = sectionGroup.Key.Title,
                Roles = sectionGroup.Key.MenuSectionRoles,
                SerialNo = sectionGroup.Key.MenuSectionSerialNo,
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

        return Result<List<MenuSectionDto>>.Success([.. result]);
    }
}
