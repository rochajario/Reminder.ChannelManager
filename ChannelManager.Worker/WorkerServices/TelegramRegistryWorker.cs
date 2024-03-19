using ChannelManager.Domain.Interfaces;

namespace ChannelManager.Worker.WorkerServices
{
    public class TelegramRegistryWorker : BackgroundService
    {
        private readonly ITelegramClient _telegram;

        public TelegramRegistryWorker(IServiceScopeFactory serviceScopeFactory)
        {
            using var scope = serviceScopeFactory.CreateScope();
            _telegram = scope.ServiceProvider.GetService<ITelegramClient>()!;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            await _telegram.ListenForRegisters(stoppingToken);
        }
    }
}