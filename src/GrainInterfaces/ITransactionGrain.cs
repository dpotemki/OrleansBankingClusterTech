namespace GrainInterfaces
{
    public interface ITransactionGrain : IGrainWithGuidKey
    {
        Task<bool> Transfer(int fromUserId, int toUserId, decimal amount);
    }
}
