namespace GrainInterfaces
{
    public interface IReminderGrain : IGrainWithIntegerKey, IRemindable
    {
        Task SendAllRemindersAsync();
    }
}
