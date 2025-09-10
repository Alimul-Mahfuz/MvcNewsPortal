namespace NewsPortal.Domain.Entities;

public class Comment
{
    public int Id { get; set; }
    public int ArticleId { get; set; }
    public Article Article { get; set; }

    public string UserId { get; set; }   // optional: link to Identity user
    public string Content { get; set; }

    public int? ParentCommentId { get; set; }
    public Comment ParentComment { get; set; }
    public ICollection<Comment> Replies { get; set; } = new List<Comment>();

    public bool IsApproved { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}