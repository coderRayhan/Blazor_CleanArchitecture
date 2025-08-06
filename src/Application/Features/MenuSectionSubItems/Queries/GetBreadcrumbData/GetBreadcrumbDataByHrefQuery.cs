using CleanArchitecture.Blazor.Application.Features.MenuSectionSubItems.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.MenuSectionSubItems.Queries.GetBreadcrumbData;

public class GetBreadcrumbDataByHrefQuery : IRequest<MenuItemForBreadcrumbDto>
{
    public string Href { get; set; }
}

public class GetBreadcrumbDataByHrefQueryHandler
    : IRequestHandler<GetBreadcrumbDataByHrefQuery, MenuItemForBreadcrumbDto>
{
    public Task<MenuItemForBreadcrumbDto> Handle(GetBreadcrumbDataByHrefQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}