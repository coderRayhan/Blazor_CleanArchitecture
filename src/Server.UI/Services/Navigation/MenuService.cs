using CleanArchitecture.Blazor.Application.Features.MenuSections.DTOs;
using CleanArchitecture.Blazor.Application.Features.MenuSectionSubItems.Queries.GetMenuItems;
using CleanArchitecture.Blazor.Infrastructure.Constants.Role;
using CleanArchitecture.Blazor.Server.UI.Models.NavigationMenu;
using MediatR;

namespace CleanArchitecture.Blazor.Server.UI.Services.Navigation;

public class MenuService : IMenuService
{
    private readonly IMediator _mediator;

    public MenuService(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public IEnumerable<MenuSectionDto> Features => GetMenus();

    private List<MenuSectionDto> GetMenus()
    {
        var list = _mediator.Send(new GetMenuItemsQuery()).Result.Data;
        return list.OrderBy(e => e.SerialNo).ToList();
    }
}