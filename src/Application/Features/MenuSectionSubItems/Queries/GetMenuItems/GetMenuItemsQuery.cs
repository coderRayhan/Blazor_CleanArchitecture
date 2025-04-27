using CleanArchitecture.Blazor.Application.Common.Interfaces.Contracts;
using CleanArchitecture.Blazor.Application.Features.MenuSectionItems.DTOs;
using CleanArchitecture.Blazor.Application.Features.MenuSections.DTOs;
using CleanArchitecture.Blazor.Application.Features.MenuSectionSubItems.DTOs;
using Dapper;

namespace CleanArchitecture.Blazor.Application.Features.MenuSectionSubItems.Queries.GetMenuItems;
public sealed record GetMenuItemsQuery : IQuery<List<MenuSectionDto>>;

internal sealed class GetMenuItemsQueryHandler(ISqlConnectionFactory sqlConnection)
    : IQueryHandler<GetMenuItemsQuery, List<MenuSectionDto>>
{
    public async Task<Result<List<MenuSectionDto>>> Handle(GetMenuItemsQuery request, CancellationToken cancellationToken)
    {
        var sql = """
            SELECT Id, Title, Roles, SerialNo FROM MenuSections;
            SELECT Id, MenuSectionId, Title, Icon, Href, Target, Roles, PageStatus, IsParent, SerialNo FROM MenuSectionItems;
            SELECT Id, MenuSectionItemId, Title, Href, Roles, PageStatus, Target, SerialNo FROM MenuSectionSubItems;
            """;
        var connection = sqlConnection.GetOpenConnection();

        using var datasets = connection.QueryMultiple(sql);

        var menuSections = (await datasets.ReadAsync<MenuSectionDto>()).ToList();
        var menuSectionItems = (await datasets.ReadAsync<MenuSectionItemDto>()).ToList();
        var menuSectionSubItems = (await datasets.ReadAsync<MenuSectionSubItemDto>()).ToList();

        var menuSectionItemsLookup = menuSectionItems.ToLookup(item => item.MenuSectionId);
        var menuSectionSubItemsLookup = menuSectionSubItems.ToLookup(subItem => subItem.MenuSectionItemId);

        foreach (var section in menuSections)
        {
            section.SectionItems = [.. menuSectionItemsLookup[section.Id].OrderBy(x => x.SerialNo)];

            foreach (var item in section.SectionItems)
            {
                item.MenuItems = [.. menuSectionSubItemsLookup[item.Id].OrderBy(x => x.SerialNo)];
            }
        }

        return Result<List<MenuSectionDto>>.Success([.. menuSections.OrderBy(x => x.SerialNo)]);
    }
}
