using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Application.Common.Interfaces;
public interface IDropdownService
{
    event Func<Task>? OnChange;
    List<SelectListDto> DataSource { get; }
    void Initialize(string sql, object parameters);
    void Refresh(string sql, object parameters);
}
