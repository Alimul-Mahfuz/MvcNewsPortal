using NewsPortal.Domain.Entities;
using NewsPortal.Infrastructure.Data;

namespace NewsPortal.Infrastructure.Seeder;

public class CategorySeeder
{
    public static void SeedCategories(ApplicationDbContext db)
    {
        if (db.Categories.Any()) return;

        var categories = new List<Category>
        {
            new Category { Name = "General", Slug = "general" },
            new Category { Name = "Technology", Slug = "technology" },
            new Category { Name = "Sports", Slug = "sports" },
            new Category { Name = "Entertainment", Slug = "entertainment" },
            new Category { Name = "Politics", Slug = "politics" }
        };

        db.Categories.AddRange(categories);
        db.SaveChanges();

        var entertainment = db.Categories.First(c => c.Name == "Entertainment");

        var subcategories = new List<Category>
        {
            new Category { Name = "National", Slug = "national", ParentCategoryId = entertainment.Id },
            new Category { Name = "International", Slug = "international", ParentCategoryId = entertainment.Id }
        };

        db.Categories.AddRange(subcategories);
        db.SaveChanges();
    }

}