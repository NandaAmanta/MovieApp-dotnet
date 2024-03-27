using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Dtos;
using MovieApp.Models;
using MovieApp.Requests.Movie;
using MovieApp.Requests.MovieSchedule;

namespace MovieApp.Services;

public class MovieScheduleService(MovieAppDataContext context, IWebHostEnvironment environment)
{
    private IWebHostEnvironment _hostingEnvironment = environment;
    private readonly MovieAppDataContext _context = context;

    public async Task<Pagination<MovieSchedule>> Paginate(int page, int perPage, object? filters)
    {
        var data = await _context.MovieSchedule
                    .OrderBy(b => b.Id)
                    .Skip((page - 1) * perPage)
                    .Take(perPage)
                    .Include(data => data.Movie)
                    .Include(data => data.Studio)
                    .ToListAsync();
        var count = await _context.Movie.CountAsync();
        var totalPages = (int)Math.Ceiling(count / (double)perPage);
        return new Pagination<MovieSchedule>(data, page, totalPages, count);
    }

    public async Task<MovieSchedule> Create(MovieScheduleCreationRequest data)
    {
        MovieSchedule movieSchedule = new();
        movieSchedule.StartAt = DateTime.Parse(data.StartAt);
        movieSchedule.EndAt = DateTime.Parse(data.EndAt);
        movieSchedule.Price = data.Price;

        Studio? studio = await _context.Studio.FindAsync(data.StudioId);
        movieSchedule.Studio = studio;

        Movie? movie = await _context.Movie.FindAsync(data.MovieId);
        movieSchedule.Movie = movie;

        movieSchedule = _context.MovieSchedule.Add(movieSchedule).Entity;
        await _context.SaveChangesAsync();
        return movieSchedule;
    }

    public async Task<MovieSchedule> Detail(long id)
    {
        return await _context.MovieSchedule
                    .Include(data => data.Movie)
                    .Include(data => data.Studio)
                    .SingleAsync(data => data.Id == id);
    }

    public async Task<MovieSchedule> Update(long id, MovieScheduleUpdateRequest data)
    {
        MovieSchedule movieSchedule = await _context.MovieSchedule.SingleAsync(data => data.Id == id);
        movieSchedule.StartAt = DateTime.Parse(data.StartAt);
        movieSchedule.EndAt = DateTime.Parse(data.EndAt);
        movieSchedule.MovieId = data.MovieId;
        movieSchedule.StudioId = data.StudioId;
        movieSchedule.Price = data.Price;
        movieSchedule = _context.MovieSchedule.Update(movieSchedule).Entity;
        await _context.SaveChangesAsync();
        return movieSchedule;
    }

    public async Task<MovieSchedule> Delete(long id)
    {
        MovieSchedule data = await _context.MovieSchedule.SingleAsync(data => data.Id == id);
        _context.Remove(data);
        await _context.SaveChangesAsync();
        return data;
    }
}