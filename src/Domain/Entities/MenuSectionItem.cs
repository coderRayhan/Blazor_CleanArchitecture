using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Blazor.Domain.Common.Entities;
using CleanArchitecture.Blazor.Domain.Common.Enums;

namespace CleanArchitecture.Blazor.Domain.Entities;
public class MenuSectionItem : BaseAuditableEntity
{
    public int MenuSectionId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public string? Href { get; set; }
    public string? Target { get; set; }
    public string[]? Roles { get; set; }
    public PageStatus PageStatus { get; set; } = PageStatus.Completed;
    public bool IsParent { get; set; }
    public int SerialNo { get; set; }
    public virtual MenuSection MenuSection { get; set; }
}
