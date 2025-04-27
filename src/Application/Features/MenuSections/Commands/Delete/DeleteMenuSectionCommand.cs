
using CleanArchitecture.Blazor.Application.Common.Interfaces.Contracts;
using CleanArchitecture.Blazor.Application.Features.MenuSections.Caching;
using CleanArchitecture.Blazor.Domain.Events;


namespace CleanArchitecture.Blazor.Application.Features.MenuSections.Commands.Delete;

public class DeleteMenuSectionCommand:  ICommand<int>
{
  public int[] Id {  get; }
  public DeleteMenuSectionCommand(int[] id)
  {
    Id = id;
  }
}

public class DeleteMenuSectionCommandHandler : 
             ICommandHandler<DeleteMenuSectionCommand, int>

{
    private readonly IApplicationDbContext _context;
    public DeleteMenuSectionCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<int>> Handle(DeleteMenuSectionCommand request, CancellationToken cancellationToken)
    {
        var items = await _context.MenuSections.Where(x=>request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
        foreach (var item in items)
        {
		    // raise a delete domain event
			item.AddDomainEvent(new MenuSectionDeletedEvent(item));
            _context.MenuSections.Remove(item);
        }
        var result = await _context.SaveChangesAsync(cancellationToken);
        return await Result<int>.SuccessAsync(result);
    }

}

