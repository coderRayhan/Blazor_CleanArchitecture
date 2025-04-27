using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Application.Common.Interfaces;
public interface ISelectListService<T> 
    where T : class
{
    Task<IEnumerable<T>> GetSelectList(string sql, object parameters);
}
