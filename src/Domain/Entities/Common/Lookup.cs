using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Blazor.Domain.Common.Entities;
using CleanArchitecture.Blazor.Domain.Common.Enums;

namespace CleanArchitecture.Blazor.Domain.Entities.Common;
public class Lookup : BaseAuditableEntity
{
    [StringLength(100)]
    public string Code { get; set; }

    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(100)]
    public string NameBN { get; set; }

    public int? ParentId { get; set; }

    [StringLength(50)]
    public bool IsActive { get; set; }
    public DevCode DevCode { get; set; }

}
