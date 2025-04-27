namespace CleanArchitecture.Blazor.Server.UI.Services.Breadcrumb;

public interface IBreadcrumbService
{
    Task<List<BreadcrumbItem>> GetBreadcrumbs(string href);
}