namespace ChannelManager.Data.Entities
{
    public class NotificationLog : BaseEntity
    {
        public Guid NotificationId { get; set; }
        public Guid UserId { get; set; }
        public string Reminder { get; set; } = string.Empty;
    }
}
