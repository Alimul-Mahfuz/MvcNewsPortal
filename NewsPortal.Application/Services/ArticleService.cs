using Microsoft.EntityFrameworkCore;
using NewsPortal.Application.DTOs;
using NewsPortal.Application.Interfaces;
using NewsPortal.Domain.Entities;
using NewsPortal.Infrastructure.Data;
using NewsPortal.Infrastructure.Interfaces;

namespace NewsPortal.Application.Services;

public class ArticleService
{
    private readonly ICurrentUser _currentUserService;
    private readonly IArticleRepository _articleRepository;
    private readonly ApplicationDbContext _db;

    public ArticleService(ICurrentUser currentUser,IArticleRepository articleRepository,ApplicationDbContext dbContext)
    {
        _currentUserService = currentUser;
        _articleRepository = articleRepository;
        _db = dbContext;
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
        await this._articleRepository.AddArticleAsync(article);
        return article;
    }

    public async Task<IEnumerable<Article>> GetArticlesAsync()
    {
        return await _articleRepository.GetArticlesAsync();
    }

    public Task<Article> GetArticleByIdAsync(int id)
    {
        return _articleRepository.GetArticleByIdAsync(id);
    }


    public IQueryable<Article> GetArticlesByCategory(int? categoryId=null)
    {
       
        var query= _articleRepository.GetQueryableArticles();
        query=query.Include(a=>a.Category)
            .Include(a=>a.Author)
            .Include(a=>a.ArticleTags)
            .ThenInclude(at=>at.Tag);

        if (categoryId.HasValue)
        {
            query=query.Where(a=>a.CategoryId==categoryId);
        }
        return query;
        
        
    }



    public async Task<int> GetCount()
    {
        return await _articleRepository.GetCount();
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