

using System.Net;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Dtos;
using MovieApp.Models;
using MovieApp.Requests.Movie;
using MovieApp.Requests.MovieSchedule;
using MovieApp.Requests.Studio;
using MovieApp.Services;
using MovieApp.Utils;

namespace MovieApp.Controllers;

[ApiController]
[Route("api/v1/movie-schedules")]
public class MovieScheduleController(MovieScheduleService movieScheduleService) : ControllerBase
{
    private readonly MovieScheduleService _movieScheduleService = movieScheduleService;

    [HttpGet]
    public async Task<ApiResponser<Pagination<MovieSchedule>>> Pagination(int pageIndex = 1, int pageSize = 10)
    {
        return new ApiResponser<Pagination<MovieSchedule>>(
            HttpStatusCode.OK,
            "Success get data",
            await _movieScheduleService.Paginate(pageIndex, pageSize, null));
    }

    [HttpPost]
    public async Task<ApiResponser<MovieSchedule>> Create([FromBody] MovieScheduleCreationRequest request)
    {
        MovieSchedule data = await _movieScheduleService.Create(request);
        return new ApiResponser<MovieSchedule>(HttpStatusCode.Created, "Success created new data", data);
    }

    [HttpGet("{id}")]
    public async Task<ApiResponser<MovieSchedule>> Detail(long id)
    {
        MovieSchedule data = await _movieScheduleService.Detail(id);
        return new ApiResponser<MovieSchedule>(HttpStatusCode.OK, "Success get detail data", data);
    }

    [HttpPut("{id}")]
    public async Task<ApiResponser<MovieSchedule>> Update(long id, MovieScheduleUpdateRequest request)
    {
        MovieSchedule data = await _movieScheduleService.Update(id, request);
        return new ApiResponser<MovieSchedule>(HttpStatusCode.OK, "Success Update data", data);
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponser<MovieSchedule>> Delete(long id)
    {
        MovieSchedule data = await _movieScheduleService.Delete(id);
        return new ApiResponser<MovieSchedule>(HttpStatusCode.OK, "Success Deleted Data", data);
    }
}