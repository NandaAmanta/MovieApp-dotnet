

using System.Net;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Dtos;
using MovieApp.Models;
using MovieApp.Requests.Tags;
using MovieApp.Services;
using MovieApp.Utils;

namespace MovieApp.Controllers;

[ApiController]
[Route("api/v1/tags")]
public class TagController(TagService tagService) : ControllerBase
{
    private readonly TagService _tagService = tagService;

    [HttpGet]
    public async Task<ApiResponser<Pagination<Tag>>> Pagination(int pageIndex = 1, int pageSize = 10)
    {
        return new ApiResponser<Pagination<Tag>>(
            HttpStatusCode.OK,
            "Success get data",
            await _tagService.Paginate(pageIndex, pageSize, null));
    }

    [HttpPost]
    public async Task<ApiResponser<Tag>> Create([FromBody] TagCreationRequest request)
    {
        var creationRequestValidator = new TagCreationValidator();
        creationRequestValidator.ValidateAndThrow(request);
        Tag data = await _tagService.Create(request);
        return new ApiResponser<Tag>(HttpStatusCode.Created, "Success created new data", data);
    }

    [HttpGet("{id}")]
    public async Task<ApiResponser<Tag>> Detail(long id)
    {
        Tag data = await _tagService.Detail(id);
        return new ApiResponser<Tag>(HttpStatusCode.OK, "Success get detail data", data);
    }

    [HttpPut("{id}")]
    public async Task<ApiResponser<Tag>> Update(long id, TagUpdateRequest request)
    {
        Tag data = await _tagService.Update(id, request);
        return new ApiResponser<Tag>(HttpStatusCode.OK, "Success Update data", data);
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponser<Tag>> Delete(long id)
    {
        Tag tag = await _tagService.Delete(id);
        return new ApiResponser<Tag>(HttpStatusCode.OK, "Success Deleted Data", tag);
    }
}