

using System.Collections.Concurrent;
using MovieApp.Data;
using MovieApp.Models;
using MovieApp.Queues;

namespace MovieApp.Services;

public class NotificationSenderBgService(IServiceProvider serviceProvider, NotificationQueue queue) : BackgroundService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly NotificationQueue _queue = queue;
    // private readonly ILogger _logger = logger;
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<MovieAppDataContext>();
            Notification? notification = await this._queue.DequeueNotifAsync();
            if (notification != null)
            {
                ctx.Notification.Add(notification);
                await ctx.SaveChangesAsync();
                // this._logger.LogInformation("new notification created");
            }
            await Task.Delay(1000);
        }
    }

}