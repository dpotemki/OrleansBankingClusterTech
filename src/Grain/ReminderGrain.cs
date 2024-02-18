using GrainInterfaces;
using Orleans.Runtime;

namespace Grains
{
    public class ReminderGrain : Grain, IReminderGrain
    {
        public async Task ReceiveReminder(string reminderName, TickStatus status)
        {
            if (reminderName == "NotificationReminder")
            {
                await SendAllRemindersAsync();
            }
        }

        public Task SendAllRemindersAsync()
        {
            
            var notificationGrain = GrainFactory.GetGrain<INotificationGrain>("");
            notificationGrain.ProcessNotifications();
            return Task.CompletedTask;
        }
      
      
    }


}
