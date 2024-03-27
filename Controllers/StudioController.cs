

using System.Net;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Dtos;
using MovieApp.Models;
using MovieApp.Requests.Movie;
using MovieApp.Requests.Studio;
using MovieApp.Services;
using MovieApp.Utils;

namespace MovieApp.Controllers;

[ApiController]
[Route("api/v1/studios")]
public class StudioController(StudioService studioService) : ControllerBase
{
    private readonly StudioService _studioService = studioService;

    [HttpGet]
    public async Task<ApiResponser<Pagination<Studio>>> Pagination(int pageIndex = 1, int pageSize = 10)
    {
        return new ApiResponser<Pagination<Studio>>(
            HttpStatusCode.OK,
            "Success get data",
            await _studioService.Paginate(pageIndex, pageSize, null));
    }

    [HttpPost]
    public async Task<ApiResponser<Studio>> Create([FromBody] StudioCreationRequest request)
    {
        Studio data = await _studioService.Create(request);
        return new ApiResponser<Studio>(HttpStatusCode.Created, "Success created new data", data);
    }

    [HttpGet("{id}")]
    public async Task<ApiResponser<Studio>> Detail(long id)
    {
        Studio data = await _studioService.Detail(id);
        return new ApiResponser<Studio>(HttpStatusCode.OK, "Success get detail data", data);
    }

    [HttpPut("{id}")]
    public async Task<ApiResponser<Studio>> Update(long id, StudioUpdateRequest request)
    {
        Studio data = await _studioService.Update(id, request);
        return new ApiResponser<Studio>(HttpStatusCode.OK, "Success Update data", data);
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponser<Studio>> Delete(long id)
    {
        Studio studio = await _studioService.Delete(id);
        return new ApiResponser<Studio>(HttpStatusCode.OK, "Success Deleted Data", studio);
    }
}