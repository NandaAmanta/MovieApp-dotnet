

using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Dtos;
using MovieApp.Models;
using MovieApp.Requests.Order;
using MovieApp.Services;
using MovieApp.Utils;

namespace MovieApp.Controllers;

[Route("api/v1/orders")]
[ApiController]
public class OrderController(OrderService orderService) : ControllerBase
{
    private readonly OrderService _orderService = orderService;

    [Authorize]
    [HttpGet]
    public async Task<ApiResponser<Pagination<Order>>> Pagination(int page = 1, int perPage = 10)
    {
        long userId = (long)Convert.ToDouble(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        return new ApiResponser<Pagination<Order>>(HttpStatusCode.OK, "Success", await _orderService.Pagination(page, perPage, userId));
    }

    [Authorize]
    [HttpPost]
    public async Task<ApiResponser<Order>> Create(OrderCreationRequest request)
    {
        long userId = (long)Convert.ToDouble(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        return new ApiResponser<Order>(HttpStatusCode.OK, "Success", await _orderService.Create(request, userId));
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ApiResponser<Order>> Delete(long id)
    {
        return new ApiResponser<Order>(HttpStatusCode.OK, "Success", await _orderService.Delete(id));
    }
}