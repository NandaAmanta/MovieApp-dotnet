using System.Net;
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
        long userId = (long)Convert.ToDouble(HttpContext.User.FindFirst("sub")?.Value);
        return new ApiResponser<Pagination<Notification>>(
            HttpStatusCode.Created,
            "registration success",
            await _notificationService.Paginate(userId, page, perPage));
    }


    [HttpGet]
    [Authorize]
    public async Task<ApiResponser<Pagination<Notification>>> Detail(int page = 1, int perPage = 10)
    {
        return new ApiResponser<Pagination<Notification>>(
            HttpStatusCode.Created,
            "registration success",
            await _notificationService.Paginate(page, perPage));
    }



}