using CleanArchitecture.Blazor.Application.Common.Interfaces.Contracts;
using CleanArchitecture.Blazor.Application.Features.MenuSections.Mappers;
using CleanArchitecture.Blazor.Domain.Events;

namespace CleanArchitecture.Blazor.Application.Features.MenuSections.Commands.AddEdit;

public class AddEditMenuSectionCommand: ICommand<int>
{
      [Description("Id")]
      public int Id { get; set; }
          [Description("Title")]
    public string? Title {get;set;} 
    [Description("Roles")]
    public string[] Roles {get;set;}
    [Description("Serial Number")]
    public int SerialNo { get; set; }
}

public class AddEditMenuSectionCommandHandler : ICommandHandler<AddEditMenuSectionCommand, int>
{
    private readonly IApplicationDbContext _context;
    public AddEditMenuSectionCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<int>> Handle(AddEditMenuSectionCommand request, CancellationToken cancellationToken)
    {
        if (request.Id > 0)
        {
            var item = await _context.MenuSections.FindAsync(request.Id, cancellationToken);
            if (item == null)
            {
                return await Result<int>.FailureAsync($"MenuSection with id: [{request.Id}] not found.");
            }
            MenuSectionMapper.ApplyChangesFrom(request,item);
			// raise a update domain event
			item.AddDomainEvent(new MenuSectionUpdatedEvent(item));
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }
        else
        {
            var item = MenuSectionMapper.FromEditCommand(request);
            // raise a create domain event
			item.AddDomainEvent(new MenuSectionCreatedEvent(item));
            _context.MenuSections.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }
       
    }
}

