using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities.Common;
public class LookupDetail : BaseAuditableEntity
{
    public int LookupId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string NameBN { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? LookupDetailId { get; set; }
    public bool IsActive { get; set; }
    public virtual Lookup Lookup { get; set; }

}
