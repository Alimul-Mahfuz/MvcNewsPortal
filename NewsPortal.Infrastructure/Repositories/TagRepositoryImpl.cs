using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Domain.Entities;
using NewsPortal.Infrastructure.Data;
using NewsPortal.Infrastructure.Interfaces;

namespace NewsPortal.Infrastructure.Repositories;

public class TagRepositoryImpl:ITagRepository
{
    private readonly ApplicationDbContext _context;
    public TagRepositoryImpl(ApplicationDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<Tag> GetTagByIdAsync(int id)
    {
        return await _context.Tags.FindAsync(id) ?? throw new Exception();
    }

    public async Task<IEnumerable<Tag>> GetTagsAsync()
    {
        return await _context.Tags.ToListAsync();
    }
}