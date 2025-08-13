﻿using CleanArchitecture.Blazor.Application.Features.Loggers.DTOs;
using CleanArchitecture.Blazor.Domain.Entities.Management;

namespace CleanArchitecture.Blazor.Application.Features.Loggers.Mappers;
[Mapper]
public static partial class LogMapper
{
    public static partial LogDto ToDto(Logger logger);
    public static partial IQueryable<LogDto> ProjectTo(this IQueryable<Logger> q);
}
