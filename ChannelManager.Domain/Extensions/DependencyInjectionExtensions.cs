using ChannelManager.Domain.Interfaces;
using ChannelManager.Domain.Models;
using ChannelManager.Domain.Services;
using ChannelManager.Domain.Services.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace ChannelManager.Domain.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection LoadDomainServices(this IServiceCollection services)
        {
            return services
                .AddScoped<ITelegramClient, TelegramClient>()
                .AddScoped<IChannelConfigurationService, ChannelConfigurationService>()
                .AddScoped<INotificationSenderService, NotificationSenderService>()
                .AddScoped<IMessagingClient<Reminder>, ReminderMessagingClient>();
        }
    }
}
