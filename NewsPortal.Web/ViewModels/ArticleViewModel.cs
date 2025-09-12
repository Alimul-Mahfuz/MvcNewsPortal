using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NewsPortal.Domain.Entities;

namespace NewsPortal.Web.ViewModels;

public class ArticleViewModel
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; }
    [Required]
    [StringLength(2000, MinimumLength = 10)]
    public string Summery { get; set; }
    [Required]
    public string Content { get; set; }
    [Required]
    public IFormFile CoverImage { get; set; }

    [DisplayName("Category Name")]
    public int CategoryId { get; set; } = 0;
    public IEnumerable<Category> Categories { get; set; }= Enumerable.Empty<Category>();
    public IEnumerable<Tag> Tags { get; set; }= Enumerable.Empty<Tag>();
    public string[] SelectedTags { get; set; } = Array.Empty<string>();
}