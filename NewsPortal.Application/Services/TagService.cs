using NewsPortal.Domain.Entities;
using NewsPortal.Infrastructure.Interfaces;

namespace NewsPortal.Application.Services;

public class TagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<Tag> GetTagByIdAsync(int id)
    {
        return await _tagRepository.GetTagByIdAsync(id);
    }

    public async Task<IEnumerable<Tag>> GetTagsAsync()
    {
        return await _tagRepository.GetTagsAsync();
    }
}