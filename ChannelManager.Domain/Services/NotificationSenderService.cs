using ChannelManager.Domain.Interfaces;
using ChannelManager.Domain.Models;

namespace ChannelManager.Domain.Services
{
    public class NotificationSenderService : INotificationSenderService
    {
        private readonly ITelegramClient _telelegramClient;
        private readonly IChannelConfigurationService _channelConfigurationService;

        public NotificationSenderService(ITelegramClient telelegramClient, IChannelConfigurationService channelConfigurationService)
        {
            _telelegramClient = telelegramClient;
            _channelConfigurationService = channelConfigurationService;
        }

        public void SendMessage(Reminder reminder)
        {

            var channelConfig = _channelConfigurationService.Read(reminder.UserId);
            if (channelConfig is not null)
            {
                _telelegramClient.SendMessage(channelConfig.TelegramChatId, reminder.Content);
            }
        }
    }
}
