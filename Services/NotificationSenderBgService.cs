

using System.Collections.Concurrent;
using MovieApp.Data;
using MovieApp.Models;
using MovieApp.Queues;

namespace MovieApp.Services;

public class NotificationSenderBgService(IServiceProvider serviceProvider, NotificationQueue queue) : BackgroundService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly NotificationQueue _queue = queue;

    // private readonly MovieAppDataContext _ctx = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<MovieAppDataContext>()
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000);
            using var scope = _serviceProvider.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<MovieAppDataContext>();
            Notification? notification = await this._queue.DequeueNotifAsync();
            if (notification != null)
            {
                ctx.Notification.Add(notification);
                await ctx.SaveChangesAsync();
            }
        }
    }

}