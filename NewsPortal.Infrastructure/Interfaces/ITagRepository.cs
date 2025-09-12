using NewsPortal.Domain.Entities;

namespace NewsPortal.Infrastructure.Interfaces;

public interface ITagRepository
{
    Task<Tag> GetTagByIdAsync(int id);
    Task<IEnumerable<Tag>> GetTagsAsync();
}