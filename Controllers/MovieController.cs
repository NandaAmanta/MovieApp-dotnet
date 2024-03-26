

using System.Net;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Dtos;
using MovieApp.Models;
using MovieApp.Services;
using MovieApp.Utils;

namespace MovieApp.Controllers;

public class MovieController(MovieService movieService) : ControllerBase
{

    private readonly MovieService _movieService = movieService;

    public async Task<ApiResponser<Pagination<Movie>>> pagination()
    {
        return new ApiResponser<Pagination<Movie>>(HttpStatusCode.OK, "Success get data", await _movieService.Paginate(1, 10, null));
    }

}