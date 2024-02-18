using GrainInterfaces;
using System.Collections.Concurrent;

namespace Grains
{
    public class NotificationGrain : Grain, INotificationGrain
    {
        private ConcurrentQueue<string> notificationsQueue = new ConcurrentQueue<string>();

        public Task EnqueueNotification(string notification)
        {
            notificationsQueue.Enqueue(notification);
            return Task.CompletedTask;
        }

        public async Task ProcessNotifications()
        {
            while (notificationsQueue.Count > 0)
            {
                if(notificationsQueue.TryDequeue(out var notification))
                {
                    // Логика отправки уведомления
                    await SendNotificationAsync(notification);
                }
            }
        }

        public Task SendNotificationAsync(string notification)
        {
            Console.WriteLine($"Sending notification: {notification}");
            return Task.CompletedTask;
        }
    }


}
