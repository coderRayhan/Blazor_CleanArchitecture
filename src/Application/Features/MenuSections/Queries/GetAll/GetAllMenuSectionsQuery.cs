
using CleanArchitecture.Blazor.Application.Features.MenuSections.DTOs;
using CleanArchitecture.Blazor.Application.Features.MenuSections.Mappers;
using CleanArchitecture.Blazor.Application.Features.MenuSections.Caching;

namespace CleanArchitecture.Blazor.Application.Features.MenuSections.Queries.GetAll;

public class GetAllMenuSectionsQuery : IRequest<IEnumerable<MenuSectionDto>>
{
}

public class GetAllMenuSectionsQueryHandler :
     IRequestHandler<GetAllMenuSectionsQuery, IEnumerable<MenuSectionDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllMenuSectionsQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MenuSectionDto>> Handle(GetAllMenuSectionsQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.MenuSections.ProjectTo()
                                                .AsNoTracking()
                                                .ToListAsync(cancellationToken);
        return data;
    }
}


