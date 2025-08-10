namespace CleanArchitecture.Blazor.Application.Common.Constants;
public static class SelectListConstants
{
    public const string GetMenuSectionSelectList = $"""
        SELECT Id, Title Name
        FROM dbo.MenuSections MS
        WHERE 1 = 1
        """;

    public const string GetMenuSectionItemSelectList = $"""
        SELECT Id, Title Name
        FROM dbo.MenuSectionItems MSI
        WHERE 1 = 1
        """;

    public const string GetLookupSelectList = $"""
        SELECT Id, Name
        FROM dbo.Lookups 
        WHERE 1 = 1
        """;
    
    public const string GetLookupDetailSelectList = $"""
        SELECT Id, Name
        FROM dbo.LookupDetails 
        WHERE 1 = 1
        """;

    public const string GetLookupDetailSelectListByLookupId = $"""
        SELECT Id, Name 
        FROM dbo.LookupDetails
        WHERE 1 = 1
        AND LookupId = @LookupId
        """;
}
