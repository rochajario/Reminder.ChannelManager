namespace ChannelManager.Data.Entities
{
    public class ChannelConfiguration : BaseEntity
    {
        public Guid UserId { get; set; }
        public long TelegramChatId { get; set; }
    }
}
