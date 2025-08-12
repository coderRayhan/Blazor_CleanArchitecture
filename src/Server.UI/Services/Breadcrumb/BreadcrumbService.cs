using CleanArchitecture.Blazor.Application.Features.MenuSectionSubItems.Queries.GetBreadcrumbData;
using CleanArchitecture.Blazor.Application.Features.MenuSectionSubItems.Queries.GetByHref;
using MediatR;

namespace CleanArchitecture.Blazor.Server.UI.Services.Breadcrumb;

public class BreadcrumbService : IBreadcrumbService
{
    private readonly IMediator _mediator;

    public BreadcrumbService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<List<BreadcrumbItem>> GetBreadcrumbs(string href)
    {
        var res = await _mediator.Send(new GetBreadcrumbDataByHrefQuery() { Href = href });
        List<BreadcrumbItem> breadcrumbs = [];
        breadcrumbs.Add(new BreadcrumbItem("", "/", icon:"<path d=\"M0 0h24v24H0z\" fill=\"none\"/><path d=\"M10 20v-6h4v6h5v-8h3L12 3 2 12h3v8z\"/>"));
        breadcrumbs.Add(new BreadcrumbItem(res.MenuSectionTitle, null, disabled: true));
        breadcrumbs.Add(new BreadcrumbItem(res.MenuSectionItemTitle, res.MenuSectionItemHref, disabled: true));
        breadcrumbs.Add(new BreadcrumbItem(res.MenuSectionSubItemTitle, res.Href));
        return breadcrumbs;
    }
}