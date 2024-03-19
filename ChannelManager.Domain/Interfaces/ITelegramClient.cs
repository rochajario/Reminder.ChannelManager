namespace ChannelManager.Domain.Interfaces
{
    public interface ITelegramClient
    {
        Task ListenForRegisters(CancellationToken cts);
        Task SendMessage(long chatId, string message, CancellationToken? stoppingToken = null);
    }
}
