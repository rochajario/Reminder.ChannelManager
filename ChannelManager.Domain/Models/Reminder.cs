
namespace ChannelManager.Domain.Models
{
    public class Reminder
    {
        public Guid UserId { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
