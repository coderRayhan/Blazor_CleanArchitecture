
using Dapper;

namespace CleanArchitecture.Blazor.Application.Features.Common.Queries;
public record GetSelectListQuery(
    string Sql, 
    object Parameters) 
    : IRequest<IEnumerable<SelectListDto>>;

internal sealed class GetSelectListQueryCommand(
    ISqlConnectionFactory sqlConnection) : IRequestHandler<GetSelectListQuery, IEnumerable<SelectListDto>>
{
    public async Task<IEnumerable<SelectListDto>>Handle(GetSelectListQuery request, CancellationToken cancellationToken)
    {
        var connection = sqlConnection.GetOpenConnection();

        var selectList = await connection
            .QueryAsync<SelectListDto>(request.Sql, request.Parameters);

        return selectList;
    }
}