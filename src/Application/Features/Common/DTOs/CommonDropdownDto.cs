using CleanArchitecture.Blazor.Application.Common;

namespace CleanArchitecture.Blazor.Application.Features.Common.DTOs;

public class CommonDropdownDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
}