using System.Collections;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Domain.Entities;
using NewsPortal.Infrastructure.Data;
using NewsPortal.Infrastructure.Interfaces;

namespace NewsPortal.Infrastructure.Repositories;

public class ArticleRepositoryImpl(
    ApplicationDbContext context
) : IArticleRepository
{
    public async Task<IEnumerable<Article>> GetArticlesAsync()
    {
        return await context.Articles.ToListAsync();
    }

    public async Task<Article> GetArticleByIdAsync(int id)
    {
        return await context.Articles.Include(a=>a.Author).FirstOrDefaultAsync(a=>a.Id==id) ?? throw new Exception();
    }

    public IQueryable<Article> GetQueryableArticles()
    {
        return context.Articles.AsQueryable();
    }

    public async Task<int> GetCount()
    {
        return await context.Articles.CountAsync();
    }


    public async Task<Article> AddArticleAsync(Article article,CancellationToken cancellationToken)
    {
         await context.Articles.AddAsync(article);
         return await context.SaveChangesAsync(cancellationToken) > 0 ? article : throw new Exception();
    }
}