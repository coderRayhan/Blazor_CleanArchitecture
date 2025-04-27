using CleanArchitecture.Blazor.Domain.Common.Entities;
using CleanArchitecture.Blazor.Domain.Common.Enums;

namespace CleanArchitecture.Blazor.Domain.Entities;
public class MenuSectionSubItem : BaseAuditableEntity
{
    public int MenuSectionItemId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Href { get; set; }
    public string[]? Roles { get; set; }
    public PageStatus PageStatus { get; set; } = PageStatus.Completed;
    public string? Target { get; set; }
    public int SerialNo { get; set; }
    public virtual MenuSectionItem MenuSectionItem { get; set; }
}
