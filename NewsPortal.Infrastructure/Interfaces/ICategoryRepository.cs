using NewsPortal.Domain.Entities;

namespace NewsPortal.Infrastructure.Interfaces;

public interface ICategoryRepository
{
    public Task<Category> GetCategoryByIdAsync(int id);
    public Task<IEnumerable<Category>> GetCategoriesAsync();
}