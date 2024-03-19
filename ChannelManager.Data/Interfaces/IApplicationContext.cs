using ChannelManager.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChannelManager.Data.Interfaces
{
    public interface IApplicationContext
    {
        DbSet<ChannelConfiguration> Configurations { get; set; }
        DbSet<NotificationLog> NotificationLogs { get; set; }
    }
}
