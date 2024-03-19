using ChannelManager.Data.Entities;
using ChannelManager.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChannelManager.Data
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public DbSet<NotificationLog> NotificationLogs { get; set; }
        public DbSet<ChannelConfiguration> Configurations { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}
