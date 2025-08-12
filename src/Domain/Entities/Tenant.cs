using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;
internal class Tenant : IEntity<string>
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Name { get; set; }
    public string? ShortCode { get; set; }
    public string? HeaderText { get; set; }
    public string? Description { get; set; }
}
