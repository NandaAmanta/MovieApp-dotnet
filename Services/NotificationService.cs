using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Dtos;
using MovieApp.Dtos.ModelInterfaces;
using MovieApp.Enums;
using MovieApp.Models;
using MovieApp.Requests.Authentication;
using MovieApp.Utils;

namespace MovieApp.Services;

public class NotificationService(MovieAppDataContext context)
{

    private readonly MovieAppDataContext _context = context;

    public async Task<Pagination<Notification>> Paginate(long userId, int page = 1, int perPage = 10)
    {
        var data = await _context.Notification
                    .Where(n => n.UserId == userId)
                    .OrderBy(b => b.Id)
                    .Skip((page - 1) * perPage)
                    .Take(perPage)
                    .ToListAsync<Notification>();
        var count = await _context.Movie.CountAsync();
        var totalPages = (int)Math.Ceiling(count / (double)perPage);
        return new Pagination<Notification>(data, page, totalPages, count);
    }

    public async Task<Notification> Detail(long id, long userId)
    {
        return await _context.Notification.SingleAsync<Notification>(n => n.Id == id && n.UserId == userId);
    }

}