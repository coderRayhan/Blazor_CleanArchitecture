

using CleanArchitecture.Blazor.Application.Features.Lookups.Caching;


namespace CleanArchitecture.Blazor.Application.Features.Lookups.Commands.Delete;

public class DeleteLookupCommand:  IRequest<Result<int>>
{
  public int[] Id {  get; }
  public DeleteLookupCommand(int[] id)
  {
    Id = id;
  }
}

public class DeleteLookupCommandHandler : 
             IRequestHandler<DeleteLookupCommand, Result<int>>

{
    private readonly IApplicationDbContext _context;
    public DeleteLookupCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<int>> Handle(DeleteLookupCommand request, CancellationToken cancellationToken)
    {
        var items = await _context.Lookups.Where(x=>request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
        foreach (var item in items)
        {
		    // raise a delete domain event
			//item.AddDomainEvent(new LookupDeletedEvent(item));
            _context.Lookups.Remove(item);
        }
        var result = await _context.SaveChangesAsync(cancellationToken);
        return await Result<int>.SuccessAsync(result);
    }

}

