namespace CleanArchitecture.Blazor.Application.Common.Interfaces.Contracts;
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}

public interface IQueryHandler<TQuery> : IRequestHandler<TQuery, Result>
    where TQuery : IQuery
{
}
