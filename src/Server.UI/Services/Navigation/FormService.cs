using CleanArchitecture.Blazor.Server.UI.Models.NavigationMenu;

namespace CleanArchitecture.Blazor.Server.UI.Services.Navigation;

public class FormService
{
    public const string UsersUrl = "/identity/users";
    public const string RolesUrl = "/identity/roles";
    public const string ProfileUrl = "/user/profile";
    public const string PicklistUrl = "/system/picklistset";
    public const string AuditTrailsUrl = "/system/audittrails";
    public const string LogsUrl = "/system/logs";
    public const string HangfireJobsUrl = "/jobs";
    public const string MenuSectionUrl = "/system/MenuSections";
    public const string MenuSectionItemUrl = "/system/MenuSectionItems";
    public const string MenuItemUrl = "/system/menuItems";
    public const string LookupsUrl = "/pages/lookups";
    public const string LookupDetailsUrl = "/pages/lookupdetails";
    public const string DicomServerStatus = "/system/dicomServerStatus";
    public const string DicomStudyList = "/pages/dicom/study-list";

    public List<Form> GetFormsList()
    {
        var forms = new List<Form>
        {
            new Form("Users", UsersUrl),
            new Form("Roles", RolesUrl),
            new Form("Profile", ProfileUrl),
            new Form("PickList", PicklistUrl),
            new Form("Audit Trails", AuditTrailsUrl),
            new Form("Logs", LogsUrl),
            new Form("Jobs", HangfireJobsUrl),
            new Form("Menu Section", MenuSectionUrl),
            new Form("Menu Section Item", MenuSectionItemUrl),
            new Form("Menu Item", MenuItemUrl),
            new Form("Lookups", LookupsUrl),
            new Form("Lookup Details", LookupDetailsUrl),
            new Form("Dicom Server Status", DicomServerStatus),
            new Form("Dicom Study List", DicomStudyList)
        };
        return forms;
    }
}