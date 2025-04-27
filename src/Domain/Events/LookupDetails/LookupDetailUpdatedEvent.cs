using CleanArchitecture.Blazor.Domain.Entities.Common;

namespace CleanArchitecture.Blazor.Domain.Events.LookupDetails;
public class LookupDetailUpdatedEvent : DomainEvent
{
    public LookupDetailUpdatedEvent(LookupDetail lookupDetail)
    {
        LookupDetail = lookupDetail;
    }
    public LookupDetail LookupDetail { get; }
}
