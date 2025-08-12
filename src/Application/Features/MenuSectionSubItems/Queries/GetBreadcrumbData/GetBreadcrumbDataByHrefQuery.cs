using CleanArchitecture.Blazor.Application.Features.MenuSectionSubItems.DTOs;
using Dapper;

namespace CleanArchitecture.Blazor.Application.Features.MenuSectionSubItems.Queries.GetBreadcrumbData;

public class GetBreadcrumbDataByHrefQuery : IRequest<MenuItemForBreadcrumbDto>
{
    public string Href { get; set; }
}

public class GetBreadcrumbDataByHrefQueryHandler
    : IRequestHandler<GetBreadcrumbDataByHrefQuery, MenuItemForBreadcrumbDto>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetBreadcrumbDataByHrefQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<MenuItemForBreadcrumbDto> Handle(GetBreadcrumbDataByHrefQuery request, CancellationToken cancellationToken)
    {
        var sql = $"""
                   SELECT MS.Title MenuSectionTitle,
                       MSI.Title MenuSectionItemTitle,
                       MSSI.Title MenuSectionSubItemTitle,
                       MSI.Href MenuSectionItemHref,
                       MSSI.Href
                   FROM MenuSections MS
                   INNER JOIN MenuSectionItems MSI ON MS.Id = MSI.MenuSectionId
                   INNER JOIN MenuSectionSubItems MSSI ON MSI.Id = MSSI.MenuSectionItemId
                   WHERE MSSI.Href = '{request.Href}'
                   """;
        var connection = _sqlConnectionFactory.GetOpenConnection();
        var data = await connection.QuerySingleAsync<MenuItemForBreadcrumbDto>(sql);
        return data;
    }
}