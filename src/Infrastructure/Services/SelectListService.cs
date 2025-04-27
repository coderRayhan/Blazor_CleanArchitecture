using Dapper;

namespace CleanArchitecture.Blazor.Infrastructure.Services;
public class SelectListService<T> : ISelectListService<T> 
    where T : class
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public SelectListService(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<T>> GetSelectList(string sql, object parameters)
    {
        var connection = _connectionFactory.GetOpenConnection();

        var list = await connection.QueryAsync<T>(sql, parameters);

        return list.AsEnumerable();
    }
}
