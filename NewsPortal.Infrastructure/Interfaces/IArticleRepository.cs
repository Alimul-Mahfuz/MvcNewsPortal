using System.Collections;
using NewsPortal.Domain.Entities;

namespace NewsPortal.Infrastructure.Interfaces;

public interface IArticleRepository
{
    public Task<IEnumerable<Article>> GetArticlesAsync();
    public Task<Article> GetArticleByIdAsync(int id);
    public IQueryable<Article> GetQueryableArticles();
    public Task<int> GetCount();
    public Task<Article> AddArticleAsync(Article article,CancellationToken cancellationToken = default);
}