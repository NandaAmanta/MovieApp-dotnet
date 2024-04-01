using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Dtos;
using MovieApp.Models;
using MovieApp.Requests.Tags;

namespace MovieApp.Services;

public class TagService(MovieAppDataContext context)
{
    private readonly MovieAppDataContext _context = context;

    public async Task<Pagination<Tag>> Paginate(int page, int perPage, object? filters)
    {
        var data = await _context.Tag
                    .OrderBy(b => b.Id)
                    .Skip((page - 1) * perPage)
                    .Take(perPage)
                    .ToListAsync();
        var count = await _context.Studio.CountAsync();
        var totalPages = (int)Math.Ceiling(count / (double)perPage);
        return new Pagination<Tag>(data, page, totalPages, count);
    }

    public async Task<Tag> Create(TagCreationRequest data)
    {
        Tag tag = new()
        {
            Name = data.Name
        };
        tag = _context.Tag.Add(tag).Entity;
        await _context.SaveChangesAsync();
        return tag;
    }

    public async Task<Tag> Detail(long id)
    {
        return await _context.Tag.SingleAsync(data => data.Id == id);
    }

    public async Task<Tag> Update(long id, TagUpdateRequest data)
    {
        Tag tag = await _context.Tag.SingleAsync(studio => studio.Id == id);
        tag.Name = data.Name;
        tag = _context.Tag.Update(tag).Entity;
        await _context.SaveChangesAsync();
        return tag;
    }

    public async Task<Tag> Delete(long id)
    {
        Tag data = await _context.Tag.SingleAsync(data => data.Id == id);
        _context.Remove(data);
        await _context.SaveChangesAsync();
        return data;
    }
}