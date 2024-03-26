
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Dtos;
using MovieApp.Models;

namespace MovieApp.Services;

public class MovieService(MovieAppDataContext context)
{

    private readonly MovieAppDataContext _context = context;

    public async Task<Pagination<Movie>> Paginate(int page, int perPage, object? filters)
    {
        var data = await _context.Movie
                    .OrderBy(b => b.Id)
                    .Skip((page - 1) * perPage)
                    .Take(perPage)
                    .ToListAsync();
        var count = await _context.Movie.CountAsync();
        var totalPages = (int)Math.Ceiling(count / (double)perPage);
        return new Pagination<Movie>(data, page, totalPages, count);
    }
}