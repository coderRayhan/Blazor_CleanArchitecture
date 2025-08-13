using System.Reflection;
using System.Text.Json;
using CleanArchitecture.Blazor.Domain.Entities.Management;
using CleanArchitecture.Blazor.Domain.Identity;
using CleanArchitecture.Blazor.Infrastructure.Constants.ClaimTypes;
using CleanArchitecture.Blazor.Infrastructure.Constants.Role;
using CleanArchitecture.Blazor.Infrastructure.Constants.User;
using CleanArchitecture.Blazor.Infrastructure.PermissionSet;

namespace CleanArchitecture.Blazor.Infrastructure.Persistence;

public class ApplicationDbContextInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger,
        ApplicationDbContext context, UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsRelational())
                await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await SeedRolesAsync();
            await SeedUsersAsync();
            await SeedMenuSectionsFromJsonFileAsync();
            await SeedDataAsync();
            _context.ChangeTracker.Clear();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }

    private static IEnumerable<string> GetAllPermissions()
    {
        var allPermissions = new List<string>();
        var modules = typeof(Permissions).GetNestedTypes();

        foreach (var module in modules)
        {
            var moduleName = string.Empty;
            var moduleDescription = string.Empty;

            var fields = module.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            foreach (var fi in fields)
            {
                var propertyValue = fi.GetValue(null);

                if (propertyValue is not null)
                    allPermissions.Add((string)propertyValue);
            }
        }

        return allPermissions;
    }

    private async Task SeedRolesAsync()
    {
        var superAdminRoleName = RoleName.SuperAdmin;
        var adminRoleName = RoleName.Admin;
        var userRoleName = RoleName.Users;
        var studentRoleName = RoleName.Student;

        if (await _roleManager.RoleExistsAsync(superAdminRoleName)) return;
        if (await _roleManager.RoleExistsAsync(adminRoleName)) return;
        if (await _roleManager.RoleExistsAsync(userRoleName)) return;
        if (await _roleManager.RoleExistsAsync(studentRoleName)) return;

        _logger.LogInformation("Seeding roles...");
        var superAdministratorRole = new ApplicationRole(superAdminRoleName)
        {
            Description = "Super Admin Group"
        };
        var administratorRole = new ApplicationRole(adminRoleName)
        {
            Description = "Admin Group"
        };
        var userRole = new ApplicationRole(userRoleName)
        {
            Description = "User Group"
        };
        var studentRole = new ApplicationRole(studentRoleName)
        {
            Description = "Student Group"
        };

        await _roleManager.CreateAsync(superAdministratorRole);
        await _roleManager.CreateAsync(administratorRole);
        await _roleManager.CreateAsync(userRole);
        await _roleManager.CreateAsync(studentRole);

        var permissions = GetAllPermissions();

        foreach (var permission in permissions)
        {
            var claim = new Claim(ApplicationClaimTypes.Permission, permission);
            await _roleManager.AddClaimAsync(superAdministratorRole, claim);

            if (permission.StartsWith("Permissions.MenuSections") || permission.StartsWith("Permissions.MenuSectionItems") || permission.StartsWith("Permissions.MenuSectionSubItems"))
            {
                await _roleManager.AddClaimAsync(administratorRole, claim);
            }
        }
    }

    private async Task SeedUsersAsync()
    {
        if (await _userManager.Users.AnyAsync()) return;

        _logger.LogInformation("Seeding users...");
        var adminUser = new ApplicationUser
        {
            UserName = UserName.Admin,
            Provider = "Local",
            IsActive = true,
            DisplayName = UserName.Admin,
            Email = "admin@example.com",
            EmailConfirmed = true,
            ProfilePictureDataUrl = "https://s.gravatar.com/avatar/78be68221020124c23c665ac54e07074?s=80",
            LanguageCode="en-US",
            TimeZoneId= "Asia/Shanghai",
            TwoFactorEnabled = false
        };
        
        var superAdminUser = new ApplicationUser
        {
            UserName = UserName.SuperAdmin,
            Provider = "Local",
            IsActive = true,
            DisplayName = UserName.SuperAdmin,
            Email = "superAdmin@example.com",
            EmailConfirmed = true,
            ProfilePictureDataUrl = "https://s.gravatar.com/avatar/78be68221020124c23c665ac54e07074?s=80",
            LanguageCode="en-US",
            TimeZoneId= "Asia/Shanghai",
            TwoFactorEnabled = false
        };

        var demoUser = new ApplicationUser
        {
            UserName = UserName.Demo,
            IsActive = true,
            Provider = "Local",
            DisplayName = UserName.Demo,
            Email = "demo@example.com",
            EmailConfirmed = true,
            LanguageCode = "de-DE",
            TimeZoneId = "Europe/Berlin",
            ProfilePictureDataUrl = "https://s.gravatar.com/avatar/ea753b0b0f357a41491408307ade445e?s=80"
        };

        await _userManager.CreateAsync(superAdminUser, UserName.SuperAdminDefaultPassword);
        await _userManager.AddToRoleAsync(superAdminUser, RoleName.SuperAdmin);
        
        await _userManager.CreateAsync(adminUser, UserName.AdminDefaultPassword);
        await _userManager.AddToRoleAsync(adminUser, RoleName.Admin);

        await _userManager.CreateAsync(demoUser, UserName.AdminDefaultPassword);
        await _userManager.AddToRoleAsync(demoUser, RoleName.Users);
    }

    private async Task<List<MenuSection>> SeedMenuSectionsFromJsonFileAsync()
    {
        if (await _context.MenuSections.AnyAsync())
            return [];

        var srcFolderPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName;

        var infrastructureAssembly = typeof(ApplicationDbContextInitializer).Assembly;
        var jsonFilePath = Path.Combine(srcFolderPath, "Infrastructure", "Persistence", "Data", "MenuSectionData.json");

        if (!File.Exists(jsonFilePath))
        {
            _logger.LogWarning("Menu section data file not found in this path");
            return [];
        }
        
        //Read json data
        var jsonData = await File.ReadAllTextAsync(jsonFilePath);
        
        //Deserialize json data to menu section list
        var menuSections = JsonSerializer.Deserialize<List<MenuSection>>(jsonData, new JsonSerializerOptions(){PropertyNameCaseInsensitive=true});

        if (menuSections is null || menuSections.Count == 0)
        {
            _logger.LogWarning("Menu section data file is empty");
            return [];
        }
        
        //Add menu sections to the context
        await _context.MenuSections.AddRangeAsync(menuSections);
        
        await _context.SaveChangesAsync();

        _logger.LogInformation("Seeded {Count} menu sections from json file", menuSections.Count);
        return menuSections;
    }

    private async Task SeedDataAsync()
    {
        if (!await _context.PicklistSets.AnyAsync())
        {

            _logger.LogInformation("Seeding key values...");
            var keyValues = new[]
            {
                new PicklistSet
                {
                    Name = Picklist.Status,
                    Value = "initialization",
                    Text = "Initialization",
                    Description = "Status of workflow"
                },
                new PicklistSet
                {
                    Name = Picklist.Status,
                    Value = "processing",
                    Text = "Processing",
                    Description = "Status of workflow"
                },
                new PicklistSet
                {
                    Name = Picklist.Status,
                    Value = "pending",
                    Text = "Pending",
                    Description = "Status of workflow"
                },
                new PicklistSet
                {
                    Name = Picklist.Status,
                    Value = "done",
                    Text = "Done",
                    Description = "Status of workflow"
                },
                new PicklistSet
                {
                    Name = Picklist.Brand,
                    Value = "Apple",
                    Text = "Apple",
                    Description = "Brand of production"
                },
                new PicklistSet
                {
                    Name = Picklist.Brand,
                    Value = "Google",
                    Text = "Google",
                    Description = "Brand of production"
                },
                new PicklistSet
                {
                    Name = Picklist.Brand,
                    Value = "Microsoft",
                    Text = "Microsoft",
                    Description = "Brand of production"
                },
                new PicklistSet
                {
                    Name = Picklist.Unit,
                    Value = "EA",
                    Text = "EA",
                    Description = "Unit of product"
                },
                new PicklistSet
                {
                    Name = Picklist.Unit,
                    Value = "KM",
                    Text = "KM",
                    Description = "Unit of product"
                },
                new PicklistSet
                {
                    Name = Picklist.Unit,
                    Value = "PC",
                    Text = "PC",
                    Description = "Unit of product"
                },
                new PicklistSet
                {
                    Name = Picklist.Unit,
                    Value = "L",
                    Text = "L",
                    Description = "Unit of product"
                }
            };

            await _context.PicklistSets.AddRangeAsync(keyValues);
            await _context.SaveChangesAsync();
        }

        if (!await _context.Products.AnyAsync())
        {

            _logger.LogInformation("Seeding products...");
            var products = new[]
            {
                new Product
                {
                    Brand = "Apple",
                    Name = "IPhone 13 Pro",
                    Description =
                    "Apple iPhone 13 Pro smartphone. Announced Sep 2021. Features 6.1″ display, Apple A15 Bionic chipset, 3095 mAh battery, 1024 GB storage.",
                    Unit = "EA",
                    Price = 999.98m
                },
                new Product
                {
                    Brand = "Sony",
                    Name = "WH-1000XM4",
                    Description = "Sony WH-1000XM4 Wireless Noise-Canceling Over-Ear Headphones. Features industry-leading noise cancellation, up to 30 hours of battery life, touch sensor controls.",
                    Unit = "EA",
                    Price = 349.99m
                },
                new Product
                {
                    Brand = "Nintendo",
                    Name = "Switch OLED Model",
                    Description = "Nintendo Switch OLED Model console. Released October 2021. Features 7″ OLED screen, 64GB internal storage, enhanced audio, dock with wired LAN port.",
                    Unit = "EA",
                    Price = 349.99m
                },
                new Product
                {
                    Brand = "Apple",
                    Name = "MacBook Air M1",
                    Description = "Apple MacBook Air with M1 chip. Features 13.3″ Retina display, Apple M1 chip with 8‑core CPU, 8GB RAM, 256GB SSD storage, up to 18 hours of battery life.",
                    Unit = "EA",
                    Price = 999.99m
                }

            };

            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();
        }
    }
}