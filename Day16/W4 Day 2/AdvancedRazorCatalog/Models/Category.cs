namespace AdvancedRazorCatalog.Models;

public class Category
{
    public string Name { get; set; } = string.Empty;
    public string Slug => Name.Trim().ToLowerInvariant().Replace(' ', '-');
}
