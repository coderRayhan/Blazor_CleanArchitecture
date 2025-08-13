namespace CleanArchitecture.Blazor.Application.Features.Tenants.Caching;

public class TenantCacheKey
{
    public const string GetAllCacheKey = "all-Tenants";
    public const string TenantsCacheKey = "all-TenantsCacheKey";
    public static string GetPaginationCacheKey(string parameters)
    {
        return $"TenantsWithPaginationQuery,{parameters}";
    }
    public static IEnumerable<string>? Tags => new string[] { "tenant" };
    public static void Refresh()
    {
        FusionCacheFactory.RemoveByTags(Tags);
    }
}