using ChannelManager.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace ChannelManager.Domain.Services.Messaging
{
    public sealed class ReminderMessagingClient : BaseMessagingClient<Reminder>
    {
        private const string QUEUE_NAME = "Reminder_Througput";
        public ReminderMessagingClient(IConfiguration configuration) : base(configuration)
        {
        }

        protected override string GetQueueName()
        {
            return QUEUE_NAME;
        }
    }
}
