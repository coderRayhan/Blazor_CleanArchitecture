namespace CleanArchitecture.Blazor.Application.Features.MenuSectionSubItems.DTOs;

public class MenuItemDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string[] MenuSectionRoles { get; set; }
    public int MenuSectionSerialNo { get; set; }

    public string MenuSectionItemTitle { get; set; }
    public string Icon { get; set; }
    public string MenuSectionItemHref { get; set; }
    public bool IsParent { get; set; }
    public PageStatus MenuSectionItemPageStatus { get; set; }
    public int MenuSectionItemSerialNo { get; set; }

    public int MenuSectionSubItemId { get; set; }
    public int MenuSectionItemId { get; set; }
    public string MenuSectionSubItemTitle { get; set; }
    public string MenuSectionSubItemHref { get; set; }
    public string[] MenuSectionSubItemRoles { get; set; }
    public PageStatus MenuSectionSubItemPageStatus { get; set; }
    public int MenuSectionSubItemSerialNo { get; set; }
}