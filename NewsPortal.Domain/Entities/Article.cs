namespace NewsPortal.Domain.Entities;

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Summary { get; set; }
    public string Content { get; set; }
    public string CoverImage { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public int AuthorId { get; set; } 

    public bool IsPublished { get; set; } = false;
    public int ViewCount { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? PublishedAt { get; set; }

    public ICollection<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public User Author { get; set; }
}