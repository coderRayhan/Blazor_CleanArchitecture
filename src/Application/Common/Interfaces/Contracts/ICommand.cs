namespace CleanArchitecture.Blazor.Application.Common.Interfaces.Contracts;
public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
