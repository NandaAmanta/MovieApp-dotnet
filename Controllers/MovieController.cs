

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
    public async Task<ApiResponser<Pagination<Movie>>> pagination(int pageIndex = 1, int pageSize = 10)
    {
        return new ApiResponser<Pagination<Movie>>(
            HttpStatusCode.OK,
            "Success get data",
            await _movieService.Paginate(pageIndex, pageSize, null));
    }

    [HttpPost]
    public async Task<ApiResponser<Movie>> create([FromForm] MovieCreationRequest request)
    {
        Movie movie = await _movieService.Create(request);
        return new ApiResponser<Movie>(HttpStatusCode.Created, "Success created new data", movie);
    }
}