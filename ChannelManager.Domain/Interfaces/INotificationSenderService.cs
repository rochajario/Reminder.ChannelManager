using ChannelManager.Domain.Models;

namespace ChannelManager.Domain.Interfaces
{
    public interface INotificationSenderService
    {
        void SendMessage(Reminder reminder);
    }
}
