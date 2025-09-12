using Microsoft.EntityFrameworkCore;
using NewsPortal.Application.DTOs;
using NewsPortal.Application.Interfaces;
using NewsPortal.Domain.Entities;
using NewsPortal.Infrastructure.Data;

namespace NewsPortal.Application.Services;

public class ArticleService
{
    private readonly ApplicationDbContext _db;
    private readonly UserService _userService;
    private readonly ICurrentUser _currentUserService;

    public ArticleService(ApplicationDbContext db, UserService userService, ICurrentUser currentUser)
    {
        _db = db;
        _userService = userService;
        _currentUserService = currentUser;
    }

    public async Task<Article> CreateArticle(CreateArticleDto dto)
    {
        var userId = _currentUserService.UserId?.ToString();

        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("User must be logged in to create an article.");
        var article = new Article
        {
            Title = dto.Title,
            Summary = dto.Summery,
            Content = dto.Content,
            CategoryId = dto.CategoryId,
            CoverImage = dto.CoverImage ?? string.Empty,
            ArticleTags = dto.TagIds.Select(tagId => new ArticleTag { TagId = tagId }).ToList(),
            IsPublished = false,
            PublishedAt = null,
            Slug = this.GenerateSlug(dto.Title),
            ViewCount = 0,
            CreatedAt = DateTime.UtcNow,
            AuthorId = _currentUserService.UserId ?? 1
        };
        await _db.Articles.AddAsync(article);
        await _db.SaveChangesAsync();
        return article;
    }

    public async Task<IEnumerable<Article>> GetArticlesAsync()
    {
        return await _db.Articles.ToListAsync();
    }

    public IQueryable<Article> GetArticlesByCategory()
    {
        return _db.Articles
            .Include(a => a.Category)
            .Include(a => a.Author);
    }

    public async Task<int> GetCount()
    {
        return await _db.Articles.CountAsync();
    }


    private string GenerateSlug(string title)
    {
        return title
            .ToLower()
            .Replace(" ", "-")
            .Replace(".", "")
            .Replace(",", "")
            .Replace(":", "")
            .Replace(";", "")
            .Replace("?", "")
            .Replace("!", "")
            .Replace("'", "")
            .Replace("\"", "")
            .Replace("/", "-")
            .Replace("\\", "-")
            .Replace("&", "and");
    }
}