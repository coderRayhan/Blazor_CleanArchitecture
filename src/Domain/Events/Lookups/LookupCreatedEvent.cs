using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Blazor.Domain.Entities.Common;

namespace CleanArchitecture.Blazor.Domain.Events.Lookups;
public class LookupCreatedEvent : DomainEvent
{
    public LookupCreatedEvent(Lookup lookup)
    {
        Lookup = lookup;
    }

    public Lookup Lookup { get; }
}
