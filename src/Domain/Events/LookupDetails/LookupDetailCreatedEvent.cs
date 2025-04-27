using CleanArchitecture.Blazor.Domain.Entities.Common;

namespace CleanArchitecture.Blazor.Domain.Events.LookupDetails;
public class LookupDetailCreatedEvent : DomainEvent
{
    public LookupDetailCreatedEvent(LookupDetail lookupDetail)
    {
        LookupDetail = lookupDetail;
    }
    public LookupDetail LookupDetail { get; }
}
