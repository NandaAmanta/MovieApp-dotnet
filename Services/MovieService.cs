
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Dtos;
using MovieApp.Models;
using MovieApp.Requests.Movie;

namespace MovieApp.Services;

public class MovieService(MovieAppDataContext context, IWebHostEnvironment environment)
{
    private IWebHostEnvironment _hostingEnvironment = environment;
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

    public async Task<Movie> Create(MovieCreationRequest data)
    {
        var file = data.PosterFile;
        string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "posters");
        string? filePath = null;
        if (file.Length > 0)
        {
            filePath = Path.Combine(uploads, file.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        Movie movie = new();
        movie.Title = data.Title;
        movie.Overview = data.Overview;
        movie.Poster = filePath;

        movie = _context.Movie.Add(movie).Entity;
        await _context.SaveChangesAsync();

        return movie;
    }

    public async Task<Movie> Detail(long id)
    {
        return await _context.Movie.SingleAsync(movie => movie.Id == id);
    }

    public async Task<Movie> Update(long id, MovieUpdateRequest data){
     
        Movie movie = await _context.Movie.SingleAsync(movie => movie.Id == id);
        movie.Title = data.Title;
        movie.Overview = data.Overview;
        movie = _context.Movie.Update(movie).Entity;
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task<Movie> Delete(long id)
    {
        Movie movie = await _context.Movie.SingleAsync(movie => movie.Id == id);
        _context.Remove(movie);
        await _context.SaveChangesAsync();
        return movie;
    }
}