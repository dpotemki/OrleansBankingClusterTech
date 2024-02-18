namespace ApiClient.Models
{
    public class TransferRequest
    {
        public Guid RequestId { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public decimal Amount { get; set; }
    }
}
