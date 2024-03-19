
using ChannelManager.Domain.Interfaces;
using ChannelManager.Domain.Models;

namespace ChannelManager.Worker.WorkerServices
{
    public class SubscriberWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMessagingClient<Reminder> _reminderClient;

        public SubscriberWorker(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            using var scope = _serviceScopeFactory.CreateScope();
            _reminderClient = scope.ServiceProvider.GetService<IMessagingClient<Reminder>>()!;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _reminderClient.Consume(reminder =>
            {
                using var innerScope = _serviceScopeFactory.CreateScope();
                var notificationSender = innerScope.ServiceProvider.GetService<INotificationSenderService>()!;
                notificationSender.SendMessage(reminder);
            });
            return Task.CompletedTask;
        }
    }
}
