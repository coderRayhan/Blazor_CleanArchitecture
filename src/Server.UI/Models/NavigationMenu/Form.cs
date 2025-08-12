namespace CleanArchitecture.Blazor.Server.UI.Models.NavigationMenu;

public class Form
{
    public Form()
    {
        
    }
    public Form(string name, string url)
    {
        Name = name;
        Url = url;
    }

    public string Name { get; set; }
    public string Url { get; set; }
}