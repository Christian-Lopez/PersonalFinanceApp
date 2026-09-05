using PersonalFinanceApp.Domain.Common;

namespace PersonalFinanceApp.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string ColorHex { get; private set; } = "#FFFFFF";
    public string? Icon { get; private set; }
    public Guid? ParentCategoryId { get; private set; }

    // Navigation properties for EF Core
    public Category? ParentCategory { get; private set; }
    
    private readonly List<Category> _subCategories = [];
    public IReadOnlyCollection<Category> SubCategories => _subCategories.AsReadOnly();

    private Category() { }

    public Category(
        string name, 
        string colorHex = "#FFFFFF", 
        string? icon = null, 
        Guid? parentCategoryId = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty.", nameof(name));

        Name = name.Trim();
        ColorHex = colorHex;
        Icon = icon?.Trim();
        ParentCategoryId = parentCategoryId;
    }

    public void UpdateDetails(string name, string colorHex, string? icon)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty.", nameof(name));

        Name = name.Trim();
        ColorHex = colorHex;
        Icon = icon?.Trim();
        SetUpdated();
    }

    public void MoveToParent(Guid? newParentId)
    {
        if (newParentId == Id)
            throw new InvalidOperationException("A category cannot be its own parent.");

        ParentCategoryId = newParentId;
        SetUpdated();
    }
}