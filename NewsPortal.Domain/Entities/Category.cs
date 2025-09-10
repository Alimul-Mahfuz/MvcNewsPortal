namespace NewsPortal.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }   // SEO-friendly URL
    public int? ParentCategoryId { get; set; }

    // Navigation (self reference)
    public Category ParentCategory { get; set; }
    public ICollection<Category> SubCategories { get; set; } = new List<Category>();
    public ICollection<Article> Articles { get; set; } = new List<Article>();
}