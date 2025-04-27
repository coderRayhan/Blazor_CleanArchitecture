using Dapper;

namespace CleanArchitecture.Blazor.Infrastructure.Services;
public class DropdownService : IDropdownService
{
    private readonly ISqlConnectionFactory _sqlConnection;

    public DropdownService(ISqlConnectionFactory sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }

    public event Func<Task>? OnChange;
    public List<SelectListDto> DataSource { get; private set; } = [];

    public async void Initialize(string sql, object parameters)
    {
        var connection = _sqlConnection.GetOpenConnection();

        var selectList = await connection.QueryAsync<SelectListDto>(sql, parameters);
        this.DataSource = selectList.ToList() ?? [];
    }

    public async void Refresh(string sql, object parameters)
    {
        var connection = _sqlConnection.GetOpenConnection();

        var selectList = await connection.QueryAsync<SelectListDto>(sql, parameters);
        this.DataSource = selectList.ToList() ?? [];
        OnChange?.Invoke();
    }
}
