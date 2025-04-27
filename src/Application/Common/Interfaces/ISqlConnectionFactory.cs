namespace CleanArchitecture.Blazor.Application.Common.Interfaces;
public interface ISqlConnectionFactory
{
    IDbConnection GetOpenConnection();
    IDbConnection CreateNewConnection();
    string GetConnectionString();
    IDbTransaction BeginTransaction();
    void CommitTransaction(IDbTransaction transaction);
    void RollbackTransaction(IDbTransaction transaction);
}
