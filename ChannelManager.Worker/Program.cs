using ChannelManager.Data.Extensions;
using ChannelManager.Domain.Extensions;
using ChannelManager.Worker.WorkerServices;

namespace ChannelManager.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var connectionString = hostContext.Configuration.GetConnectionString("Database")!;
                    services.LoadDomainServices();
                    services.LoadDatabaseContext(connectionString);

                    services.AddHostedService<TelegramRegistryWorker>();
                    services.AddHostedService<SubscriberWorker>();
                })
                .Build();

            host.Run();
        }
    }
}