using System.Collections.Concurrent;
using MovieApp.Models;

namespace MovieApp.Queues;

public class NotificationQueue
{
    private readonly ConcurrentQueue<Notification> _queue = new ConcurrentQueue<Notification>();

    public Task EnqueueNotifAsync(Notification message)
    {
        _queue.Enqueue(message);
        return Task.CompletedTask;
    }

    public Task<Notification> DequeueNotifAsync()
    {
        _queue.TryDequeue(out var message);
        return Task.FromResult(message);
    }
}