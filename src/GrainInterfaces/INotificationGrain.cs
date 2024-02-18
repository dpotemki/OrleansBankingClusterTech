namespace GrainInterfaces
{
    public interface INotificationGrain : IGrainWithStringKey
    {
        Task EnqueueNotification(string notification);
        Task ProcessNotifications();
        Task SendNotificationAsync(string notification);
    }
}
