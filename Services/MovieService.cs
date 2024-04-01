
using System.Net.Http.Headers;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MovieApp.Configs;
using MovieApp.Data;
using MovieApp.Dtos;
using MovieApp.Dtos.ModelInterfaces;
using MovieApp.Dtos.Tmdb;
using MovieApp.Models;
using MovieApp.Requests.Movie;

namespace MovieApp.Services;

public class MovieService(MovieAppDataContext context, IWebHostEnvironment environment, IOptions<TmdbConfiguration> tmdbConfiguration)
{
    private IWebHostEnvironment _hostingEnvironment = environment;
    private TmdbConfiguration _tmdbConfiguration = tmdbConfiguration.Value;
    private readonly MovieAppDataContext _context = context;

    public async Task<Pagination<IMovie>> Paginate(int page, int perPage, object? filters)
    {
        var data = await _context.Movie
                    .OrderBy(b => b.Id)
                    .Skip((page - 1) * perPage)
                    .Take(perPage)
                    .ToListAsync<IMovie>();
        var count = await _context.Movie.CountAsync();
        var totalPages = (int)Math.Ceiling(count / (double)perPage);
        return new Pagination<IMovie>(data, page, totalPages, count);
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
        movie.PlayUntil = DateTime.Parse(data.PlayUntil);

        movie = _context.Movie.Add(movie).Entity;
        await _context.SaveChangesAsync();

        return movie;
    }

    public async Task<IMovie> Detail(long id)
    {
        return await _context.Movie.SingleAsync<IMovie>(movie => movie.Id == id);
    }

    public async Task<IMovie> Update(long id, MovieUpdateRequest data)
    {

        Movie movie = await _context.Movie.SingleAsync(movie => movie.Id == id);
        movie.Title = data.Title;
        movie.Overview = data.Overview;
        movie.PlayUntil = DateTime.Parse(data.PlayUntil);
        movie = _context.Movie.Update(movie).Entity;
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task<IMovie> Delete(long id)
    {
        Movie movie = await _context.Movie.SingleAsync(movie => movie.Id == id);
        _context.Remove(movie);
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task FetchMovieFromTMDB()
    {
        HttpClient client = new HttpClient();
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, _tmdbConfiguration.Url);
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tmdbConfiguration.Token);
        var response = await client.SendAsync(requestMessage);
        TmdbPagination<TmdbMovie> paginationResponse = await response.Content.ReadFromJsonAsync<TmdbPagination<TmdbMovie>>();
        paginationResponse.results.ForEach(async delegate (TmdbMovie tmdbMovie)
        {
            Movie? checkedMovie = await _context.Movie.SingleOrDefaultAsync(m => m.Title == tmdbMovie.Title);
            if (checkedMovie == null)
            {
                Movie movie = new()
                {
                    Title = tmdbMovie.Title,
                    Overview = tmdbMovie.Overview,
                    Poster = tmdbMovie.PosterPath,
                    PlayUntil = DateTime.Parse(paginationResponse.dates.Maximum)
                };
                _context.Movie.Add(movie);
            }
        });
        await _context.SaveChangesAsync();
    }
}