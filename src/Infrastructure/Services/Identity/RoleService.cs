using CleanArchitecture.Blazor.Domain.Identity;

namespace CleanArchitecture.Blazor.Infrastructure.Services.Identity;

public class RoleService : IRoleService
{
    private RoleManager<ApplicationRole> _roleManager;

    public RoleService(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }
    public List<string> GetAllRoles()
    {
        return _roleManager.Roles.Select(x => x.Name).ToList();
    }
}