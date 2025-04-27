using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;
public class MenuSection : BaseAuditableEntity
{
    public string Title { get; set; } = string.Empty;
    public string[]? Roles { get; set; }
    public int SerialNo { get; set; }
}
