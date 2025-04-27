
using CleanArchitecture.Blazor.Domain.Entities.Common;

namespace CleanArchitecture.Blazor.Domain.Events.Lookups;
public class LookupDeletedEvent : DomainEvent
{
    public LookupDeletedEvent(Lookup item) => Item = item;
    public Lookup Item { get; }
}
