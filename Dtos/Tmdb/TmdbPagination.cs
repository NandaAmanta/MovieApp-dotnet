

namespace MovieApp.Dtos.Tmdb;

public class TmdbPagination<T>
{
    public TmdbPlayUntil? dates { get; set; }
    public List<TmdbMovie> results { get; set; } = [];
}