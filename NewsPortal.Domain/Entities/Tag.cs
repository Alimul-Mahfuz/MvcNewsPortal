namespace NewsPortal.Domain.Entities;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }

    public ICollection<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>();
}