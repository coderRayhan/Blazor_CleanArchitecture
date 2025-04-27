// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Dapper;

namespace CleanArchitecture.Blazor.Application.Common.Models;

public class PaginatedData<T>
{
    public PaginatedData(IEnumerable<T> items, int total, int pageIndex, int pageSize)
    {
        Items = items;
        TotalItems = total;
        CurrentPage = pageIndex;
        TotalPages = (int)Math.Ceiling(total / (double)pageSize);
    }

    public int CurrentPage { get; }
    public int TotalItems { get; private set; }
    public int TotalPages { get; }
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
    public IEnumerable<T> Items { get; set; }

    public static async Task<PaginatedData<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedData<T>(items, count, pageIndex, pageSize);
    }

    public static async Task<PaginatedData<T>> CreateAsync(
        IDbConnection connection, 
        string sql,
        PaginationFilter filter,
        object? parameters = default,
        CancellationToken cancellationToken = default)
    {
        var sd = filter.SortDirection == "Ascending" ? "ASC" : "DESC";
        sql = $"""{sql} ORDER BY {filter.OrderBy} {sd} OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY""";

        #region Parameters
        var param = new DynamicParameters(parameters);
        param.Add("Offset", (filter.PageNumber - 1) * filter.PageSize);
        param.Add("Pagesize", filter.PageSize);
        #endregion

        var items = await connection.QueryAsync<T>(sql, param);

        var count = await connection.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM ({sql}) AS CountQuery", param);

        return new PaginatedData<T>(items.AsList(), count, filter.PageNumber, filter.PageSize);
    }
}