
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Dtos;
using MovieApp.Models;
using MovieApp.Requests.Order;

namespace MovieApp.Services;

public class OrderService(MovieAppDataContext context)
{
    private readonly MovieAppDataContext _context = context;

    public async Task<Pagination<Order>> pagination(int page, int perPage, long? userId = null)
    {
        var data = await _context.Order
                    .Where(order => order.UserId == userId)
                    .OrderBy(b => b.Id)
                    .Skip((page - 1) * perPage)
                    .Take(perPage)
                    .ToListAsync();
        var count = await _context.Movie.CountAsync();
        var totalPages = (int)Math.Ceiling(count / (double)perPage);
        return new Pagination<Order>(data, page, totalPages, count);
    }

    public async Task<Order> Create(OrderCreationRequest data)
    {
        double totalPrice = 0;
        List<long> movieScheduleIds = data.OrderItems.Select(orderItem => orderItem.MovieScheduleId).ToList();
        List<MovieSchedule> movieSchedules = await _context.MovieSchedule
        .Where(movieSchedule => movieScheduleIds.Contains(movieSchedule.Id))
        .ToListAsync();

        Order order = new();

        movieSchedules.ForEach(delegate (MovieSchedule movieSchedule)
        {
            var orderItemReq = data.OrderItems
            .Where(o => o.MovieScheduleId == movieSchedule.Id)
            .FirstOrDefault();
            double price = orderItemReq.Qyt * movieSchedule.Price;
            OrderItem orderItem = new()
            {
                MovieScheduleId = movieSchedule.Id,
                Price = price,
                Order = order,
                Qty = orderItemReq.Qyt
            };
            _context.OrderItem.Add(orderItem);
            totalPrice += price;
        });

        order.TotalItemPrice = totalPrice;
        order.PaymentMethod = data.PaymentMethod;
        order = _context.Order.Add(order).Entity;
        await _context.SaveChangesAsync();
        return order;
    }

}