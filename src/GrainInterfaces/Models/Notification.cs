namespace GrainInterfaces.Models
{
    [Serializable]

    public class Notification
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public required string MessageText { get; set; }
    }
}
