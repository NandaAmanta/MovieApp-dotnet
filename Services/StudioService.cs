using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Dtos;
using MovieApp.Models;
using MovieApp.Requests.Studio;

namespace MovieApp.Services;

public class StudioService(MovieAppDataContext context)
{
    private readonly MovieAppDataContext _context = context;

    public async Task<Pagination<Studio>> Paginate(int page, int perPage, object? filters)
    {
        var data = await _context.Studio
                    .OrderBy(b => b.Id)
                    .Skip((page - 1) * perPage)
                    .Take(perPage)
                    .ToListAsync();
        var count = await _context.Studio.CountAsync();
        var totalPages = (int)Math.Ceiling(count / (double)perPage);
        return new Pagination<Studio>(data, page, totalPages, count);
    }

    public async Task<Studio> Create(StudioCreationRequest data)
    {

        Studio studio = new();
        studio.SeatCapacity = data.SeatCapacity;
        studio.StudioNumber = data.StudioNumber;
        studio = _context.Studio.Add(studio).Entity;
        await _context.SaveChangesAsync();

        return studio;
    }

    public async Task<Studio> Detail(long id)
    {
        return await _context.Studio.SingleAsync(data => data.Id == id);
    }

    public async Task<Studio> Update(long id, StudioUpdateRequest data)
    {

        Studio studio = await _context.Studio.SingleAsync(studio => studio.Id == id);
        studio.SeatCapacity = data.SeatCapacity;
        studio.StudioNumber = data.StudioNumber;
        studio = _context.Studio.Update(studio).Entity;
        await _context.SaveChangesAsync();
        return studio;
    }

    public async Task<Studio> Delete(long id)
    {
        Studio data = await _context.Studio.SingleAsync(data => data.Id == id);
        _context.Remove(data);
        await _context.SaveChangesAsync();
        return data;
    }
}