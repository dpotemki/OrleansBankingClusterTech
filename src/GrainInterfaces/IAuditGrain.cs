namespace GrainInterfaces
{
    public interface IAuditGrain : IGrainWithGuidKey
    {
        Task RecordTransaction(int fromUserId, int toUserId, decimal amount, bool success);
    }
}
