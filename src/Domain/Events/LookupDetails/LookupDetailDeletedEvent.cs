using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Domain.Events.LookupDetails;
public class LookupDetailDeletedEvent : DomainEvent
{
    public int LookupDetailId { get; }
    public LookupDetailDeletedEvent(int lookupDetailId)
    {
        LookupDetailId = lookupDetailId;
    }
}
