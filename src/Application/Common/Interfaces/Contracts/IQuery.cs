using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Application.Common.Interfaces.Contracts;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}

public interface IQuery : IRequest<Result>
{
}
