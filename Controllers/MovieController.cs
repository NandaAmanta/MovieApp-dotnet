

using System.Net;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Dtos;
using MovieApp.Models;
using MovieApp.Requests.Movie;
using MovieApp.Services;
using MovieApp.Utils;

namespace MovieApp.Controllers;

[ApiController]
[Route("api/v1/movies")]
public class MovieController(MovieService movieService) : ControllerBase
{
    private readonly MovieService _movieService = movieService;

    [HttpGet]
    public async Task<ApiResponser<Pagination<Movie>>> Pagination(int pageIndex = 1, int pageSize = 10)
    {
        return new ApiResponser<Pagination<Movie>>(
            HttpStatusCode.OK,
            "Success get data",
            await _movieService.Paginate(pageIndex, pageSize, null));
    }

    [HttpPost]
    public async Task<ApiResponser<Movie>> Create([FromForm] MovieCreationRequest request)
    {
        Movie movie = await _movieService.Create(request);
        return new ApiResponser<Movie>(HttpStatusCode.Created, "Success created new data", movie);
    }

    [HttpGet("{id}")]
    public async Task<ApiResponser<Movie>> Detail(long id)
    {
        Movie movie = await _movieService.Detail(id);
        return new ApiResponser<Movie>(HttpStatusCode.OK, "Success get detail data", movie);
    }

    [HttpPut("{id}")]
    public async Task<ApiResponser<Movie>> Update(long id, MovieUpdateRequest request)
    {
        Movie movie = await _movieService.Update(id, request);
        return new ApiResponser<Movie>(HttpStatusCode.OK, "Success Update data", movie);
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponser<Movie>> Delete(long id)
    {
        Movie movie = await _movieService.Delete(id);
        return new ApiResponser<Movie>(HttpStatusCode.OK, "Success Deleted Data", movie);
    }
}