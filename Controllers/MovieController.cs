

using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Dtos;
using MovieApp.Models;
using MovieApp.Requests.Movie;
using MovieApp.Services;
using MovieApp.Utils;
using MovieApp.Attributes;
using MovieApp.Dtos.ModelInterfaces;

namespace MovieApp.Controllers;

[ApiController]
[Route("api/v1/movies")]
public class MovieController(MovieService movieService) : ControllerBase
{
    private readonly MovieService _movieService = movieService;

    [HttpGet]
    public async Task<ApiResponser<Pagination<IMovie>>> Pagination(int pageIndex = 1, int pageSize = 10)
    {
        return new ApiResponser<Pagination<IMovie>>(
            HttpStatusCode.OK,
            "Success get data",
            await _movieService.Paginate(pageIndex, pageSize, null));
    }

    [HttpPost]
    [AdminAuthorize]
    public async Task<ApiResponser<IMovie>> Create([FromForm] MovieCreationRequest request)
    {
        IMovie movie = await _movieService.Create(request);
        return new ApiResponser<IMovie>(HttpStatusCode.Created, "Success created new data", movie);
    }

    [HttpGet("{id}")]
    public async Task<ApiResponser<IMovie>> Detail(long id)
    {
        IMovie movie = await _movieService.Detail(id);
        return new ApiResponser<IMovie>(HttpStatusCode.OK, "Success get detail data", movie);
    }

    [HttpPut("{id}")]

    [AdminAuthorize]
    public async Task<ApiResponser<IMovie>> Update(long id, MovieUpdateRequest request)
    {
        IMovie movie = await _movieService.Update(id, request);
        return new ApiResponser<IMovie>(HttpStatusCode.OK, "Success Update data", movie);
    }

    [HttpDelete("{id}")]
    [AdminAuthorize]
    public async Task<ApiResponser<IMovie>> Delete(long id)
    {
        IMovie movie = await _movieService.Delete(id);
        return new ApiResponser<IMovie>(HttpStatusCode.OK, "Success Deleted Data", movie);
    }
}