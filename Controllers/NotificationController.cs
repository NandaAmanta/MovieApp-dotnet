using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Dtos;
using MovieApp.Dtos.ModelInterfaces;
using MovieApp.Models;
using MovieApp.Requests.Authentication;
using MovieApp.Services;
using MovieApp.Utils;

namespace MovieApp.Controllers;

[Route("api/v1/notifications")]
[ApiController]
public class NotificationController(NotificationService notificationService) : ControllerBase
{
    private readonly NotificationService _notificationService = notificationService;

    [HttpGet]
    [Authorize]
    public async Task<ApiResponser<Pagination<Notification>>> Pagination(int page = 1, int perPage = 10)
    {
        long userId = (long)Convert.ToDouble(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        return new ApiResponser<Pagination<Notification>>(
            HttpStatusCode.Created,
            "success",
            await _notificationService.Paginate(userId, page, perPage));
    }


    [HttpGet("{id}")]
    [Authorize]
    public async Task<ApiResponser<Notification>> Detail(long id)
    {
        long userId = (long)Convert.ToDouble(HttpContext.User.FindFirst("sub")?.Value);
        return new ApiResponser<Notification>(
            HttpStatusCode.Created,
            "success",
            await _notificationService.Detail(id, userId));
    }



}