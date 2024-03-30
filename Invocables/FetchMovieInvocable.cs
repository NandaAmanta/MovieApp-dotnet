

using Coravel.Invocable;
using MovieApp.Services;

namespace MovieApp.Invocables;

public class FetchMovieInvocable(MovieService movieService) : IInvocable
{
    private readonly MovieService _movieService = movieService;

    public async Task Invoke()
    {
        await _movieService.FetchMovieFromTMDB();
    }
}