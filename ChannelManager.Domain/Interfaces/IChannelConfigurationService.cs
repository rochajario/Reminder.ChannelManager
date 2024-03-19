using ChannelManager.Data.Entities;

namespace ChannelManager.Domain.Interfaces
{
    public interface IChannelConfigurationService
    {
        Guid SetChatId(Guid userId, long telegramChatId);
        ChannelConfiguration? Read(Guid userId);
        void Delete(Guid userId);
    }
}
