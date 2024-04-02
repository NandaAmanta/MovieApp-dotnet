
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Dtos;
using MovieApp.Enums;
using MovieApp.Models;
using MovieApp.Queues;
using MovieApp.Requests.Order;

namespace MovieApp.Services;

public class OrderService(MovieAppDataContext context, NotificationQueue notificationQueue)
{
    private readonly MovieAppDataContext _context = context;
    private readonly NotificationQueue _notificationQueue = notificationQueue;

    public async Task<Pagination<Order>> Pagination(int page, int perPage, long? userId = null)
    {
        var data = await _context.Order
                    .Where(order => userId == null ? true : order.UserId == userId)
                    .OrderBy(b => b.Id)
                    .Skip((page - 1) * perPage)
                    .Take(perPage)
                    .ToListAsync();
        var count = await _context.Order
                    .Where(order => userId == null ? true : order.UserId == userId)
                    .CountAsync();
        var totalPages = (int)Math.Ceiling(count / (double)perPage);
        return new Pagination<Order>(data, page, totalPages, count);
    }

    public async Task<Order> Create(OrderCreationRequest data, long userId)
    {
        double totalPrice = 0;
        List<long> movieScheduleIds = data.OrderItems.Select(orderItem => orderItem.MovieScheduleId).ToList();
        List<MovieSchedule> movieSchedules = await _context.MovieSchedule
        .Where(movieSchedule => movieScheduleIds.Contains(movieSchedule.Id))
        .ToListAsync();

        Order order = new();
        order.User = await _context.User.Where(u => u.Id == userId).FirstAsync();
        order.PaymentMethod = data.PaymentMethod;
        order = _context.Order.Add(order).Entity;
        await _context.SaveChangesAsync();


        movieSchedules.ForEach(delegate (MovieSchedule movieSchedule)
        {
            var orderItemReq = data.OrderItems
            .Where(o => o.MovieScheduleId == movieSchedule.Id)
            .FirstOrDefault();
            Console.WriteLine("=========>" + orderItemReq.Qty);
            Console.WriteLine("=========>" + movieSchedule.Price);
            Console.WriteLine("=========>" + orderItemReq.Qty * movieSchedule.Price);
            double price = orderItemReq.Qty * movieSchedule.Price;
            OrderItem orderItem = new()
            {
                MovieScheduleId = movieSchedule.Id,
                Price = price,
                Order = order,
                Qty = orderItemReq.Qty
            };
            _context.OrderItem.Add(orderItem);
            totalPrice += price;
        });

        order.TotalItemPrice = totalPrice;
        order = _context.Order.Update(order).Entity;
        await _context.SaveChangesAsync();
        await this._notificationQueue.EnqueueNotifAsync(new Notification(){
            Title = "New Order Created",
            Message = "Your Order just created",
            User = await _context.User.SingleAsync(u=>u.Id == userId),
            Type = NotificationType.PERSONAL
        });
        
        return order;
    }

    public async Task<Order> Delete(long id)
    {
        Order order = await _context.Order.SingleAsync(o => o.Id == id);
        List<OrderItem> orderItems = await _context.OrderItem.Where(ot => ot.OrderId == id).ToListAsync();
        _context.Order.Remove(order);
        _context.OrderItem.RemoveRange(orderItems);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<Order> UpdateTotalPrice(long id)
    {
        Order order = await _context.Order.SingleAsync(o => o.Id == id);
        List<OrderItem> orderItems = await _context.OrderItem.Where(ot => ot.OrderId == id).ToListAsync();
        double totalPrice = 0;
        orderItems.ForEach(delegate (OrderItem orderItem)
        {
            totalPrice += orderItem.Price;
        });
        order.TotalItemPrice = totalPrice;
        order = _context.Order.Update(order).Entity;
        await _context.SaveChangesAsync();
        return order;
    }


}