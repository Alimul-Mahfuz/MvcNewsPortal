using Microsoft.EntityFrameworkCore;
using NewsPortal.Domain.Entities;
using NewsPortal.Infrastructure.Data;
using NewsPortal.Infrastructure.Interfaces;

namespace NewsPortal.Infrastructure.Repositories;

public class CategoryRepositoryImpl:ICategoryRepository
{
    
    private readonly ApplicationDbContext _context;
    public CategoryRepositoryImpl(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id) ?? throw new Exception();
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }
}