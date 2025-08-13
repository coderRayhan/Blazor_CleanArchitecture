
using CleanArchitecture.Blazor.Domain.Entities.Common;
using CleanArchitecture.Blazor.Domain.Entities.Management;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CleanArchitecture.Blazor.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Logger> Loggers { get; set; }
    DbSet<AuditTrail> AuditTrails { get; set; }
    DbSet<PicklistSet> PicklistSets { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<MenuSection> MenuSections { get; set; }
    DbSet<MenuSectionItem> MenuSectionItems { get; set; }
    DbSet<MenuSectionSubItem> MenuSectionSubItems { get; set; }

    DbSet<Lookup> Lookups { get; set; }
    DbSet<LookupDetail> LookupDetails { get; set; }
    DbSet<Tenant> Tenants { get; set; }
    ChangeTracker ChangeTracker { get; }

    DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}