using NewsPortal.Domain.Entities;
using NewsPortal.Infrastructure.Data;

namespace NewsPortal.Infrastructure.Seeder;

public class TagSeeder
{
    public static void SeedTags(ApplicationDbContext db)
    {
        if(db.Tags.Any()) return;
        
        db.Tags.AddRange(
            new Tag { Name = "Crime", Slug = "crime" },
            new Tag { Name = "Politics", Slug = "politics" },
            new Tag { Name = "Technology", Slug = "technology" },
            new Tag { Name = "Entertainment", Slug = "entertainment" },
            new Tag { Name = "Sports", Slug = "sports" }
        );
        
        db.SaveChanges();

    }
}