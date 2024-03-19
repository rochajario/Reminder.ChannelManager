using ChannelManager.Data;
using ChannelManager.Data.Entities;
using ChannelManager.Data.Interfaces;
using ChannelManager.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChannelManager.Domain.Services
{
    public class ChannelConfigurationService : IChannelConfigurationService
    {
        private readonly ApplicationContext _context;

        public ChannelConfigurationService(IApplicationContext context)
        {
            _context = (ApplicationContext)context;
        }

        public void Delete(Guid userId)
        {
            var config = Read(userId);
            if (config is not null)
            {
                _context.Configurations.Remove(config);
                _context.SaveChanges();
            }
        }

        public ChannelConfiguration? Read(Guid userId)
        {
            return _context.Configurations.AsNoTracking().SingleOrDefault(c => c.UserId == userId);
        }

        public Guid SetChatId(Guid userId, long telegramChatId)
        {
            var channelConfiguration = new ChannelConfiguration
            {
                UserId = userId,
                TelegramChatId = telegramChatId
            };

            if (Read(userId) is not null)
            {
                var configToUpdate = _context.Configurations.Single(x => x.UserId.Equals(userId));

                configToUpdate.UserId = userId;
                configToUpdate.TelegramChatId = telegramChatId;
                _context.SaveChanges();
                return configToUpdate.Id;
            }

            var id = _context.Configurations.Add(channelConfiguration).Entity.Id;
            _context.SaveChanges();
            return id;
        }
    }
}
