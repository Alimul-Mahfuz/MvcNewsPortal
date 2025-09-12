namespace NewsPortal.Application.DTOs;

public class CreateArticleDto
{
   
    public string Title { get; set; }

    public string Summery { get; set; }

    public string Content { get; set; }
    public string? CoverImage { get; set; }
    public int CategoryId { get; set; }
    public List<int> TagIds { get; set; } = new List<int>();
    
}