namespace CleanArchitecture.Blazor.Application.Features.MenuSectionSubItems.DTOs;

public class MenuItemForBreadcrumbDto
{
    public string MenuSectionTitle { get; set; }
    public string MenuSectionItemTitle { get; set; }
    public string MenuSectionSubItemTitle { get; set; }
    public string MenuSectionItemHref { get; set; }
    public string Href { get; set; }
}