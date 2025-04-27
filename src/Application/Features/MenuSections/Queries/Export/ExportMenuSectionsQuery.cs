
using CleanArchitecture.Blazor.Application.Features.MenuSections.DTOs;
using CleanArchitecture.Blazor.Application.Features.MenuSections.Mappers;
using CleanArchitecture.Blazor.Application.Features.MenuSections.Caching;
using CleanArchitecture.Blazor.Application.Features.MenuSections.Specifications;
using CleanArchitecture.Blazor.Application.Common.Interfaces.Contracts;

namespace CleanArchitecture.Blazor.Application.Features.MenuSections.Queries.Export;

public class ExportMenuSectionsQuery : MenuSectionAdvancedFilter, IQuery<byte[]>
{
      public MenuSectionAdvancedSpecification Specification => new MenuSectionAdvancedSpecification(this);
    public override string ToString()
    {
        return $"Listview:{ListView}:{CurrentUser?.UserId}-{LocalTimezoneOffset.TotalHours}, Search:{Keyword}, {OrderBy}, {SortDirection}";
    }
}
    
public class ExportMenuSectionsQueryHandler :
         IQueryHandler<ExportMenuSectionsQuery, byte[]>
{
        private readonly IApplicationDbContext _context;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportMenuSectionsQueryHandler> _localizer;
        private readonly MenuSectionDto _dto = new();
        public ExportMenuSectionsQueryHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ExportMenuSectionsQueryHandler> localizer
            )
        {
            _context = context;
            _excelService = excelService;
            _localizer = localizer;
        }
        #nullable disable warnings
        public async Task<Result<byte[]>> Handle(ExportMenuSectionsQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.MenuSections.ApplySpecification(request.Specification)
                       .OrderBy($"{request.OrderBy} {request.SortDirection}")
                       .ProjectTo()
                       .AsNoTracking()
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<MenuSectionDto, object?>>()
                {
                    // TODO: Define the fields that should be exported, for example:
                    {_localizer[_dto.GetMemberDescription(x=>x.Title)],item => item.Title}, 

                }
                , _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);
        }
}
