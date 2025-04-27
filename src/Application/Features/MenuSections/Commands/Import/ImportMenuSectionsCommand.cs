
using CleanArchitecture.Blazor.Application.Features.MenuSections.DTOs;
using CleanArchitecture.Blazor.Application.Features.MenuSections.Caching;
using CleanArchitecture.Blazor.Application.Features.MenuSections.Mappers;
using CleanArchitecture.Blazor.Application.Common.Interfaces.Contracts;

namespace CleanArchitecture.Blazor.Application.Features.MenuSections.Commands.Import;

    public class ImportMenuSectionsCommand: ICommand<int>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public ImportMenuSectionsCommand(string fileName,byte[] data)
        {
           FileName = fileName;
           Data = data;
        }
    }
    public record class CreateMenuSectionsTemplateCommand : ICommand<byte[]>
    {
 
    }

    public class ImportMenuSectionsCommandHandler : 
                 ICommandHandler<CreateMenuSectionsTemplateCommand, byte[]>,
                 IRequestHandler<ImportMenuSectionsCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IStringLocalizer<ImportMenuSectionsCommandHandler> _localizer;
        private readonly IExcelService _excelService;
        private readonly MenuSectionDto _dto = new();

        public ImportMenuSectionsCommandHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ImportMenuSectionsCommandHandler> localizer)
        {
            _context = context;
            _localizer = localizer;
            _excelService = excelService;
        }
        #nullable disable warnings
        public async Task<Result<int>> Handle(ImportMenuSectionsCommand request, CancellationToken cancellationToken)
        {

           var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, MenuSectionDto, object?>>
            {
                { _localizer[_dto.GetMemberDescription(x=>x.Title)], (row, item) => item.Title = row[_localizer[_dto.GetMemberDescription(x=>x.Title)]].ToString() }, 

            }, _localizer[_dto.GetClassDescription()]);
            if (result.Succeeded && result.Data is not null)
            {
                foreach (var dto in result.Data)
                {
                    var exists = await _context.MenuSections.AnyAsync(x => x.Title == dto.Title, cancellationToken);
                    if (!exists)
                    {
                        var item = MenuSectionMapper.FromDto(dto);
                        // add create domain events if this entity implement the IHasDomainEvent interface
				        // item.AddDomainEvent(new MenuSectionCreatedEvent(item));
                        await _context.MenuSections.AddAsync(item, cancellationToken);
                    }
                 }
                 await _context.SaveChangesAsync(cancellationToken);
                 return await Result<int>.SuccessAsync(result.Data.Count());
           }
           else
           {
               return await Result<int>.FailureAsync(result.Errors);
           }
        }
        public async Task<Result<byte[]>> Handle(CreateMenuSectionsTemplateCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implement ImportMenuSectionsCommandHandler method 
            var fields = new string[] {
                   // TODO: Define the fields that should be generate in the template, for example:
                   _localizer[_dto.GetMemberDescription(x=>x.Title)], 

                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);
        }
    }

