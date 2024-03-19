using ChannelManager.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ChannelManager.Domain.Services
{
    public class TelegramClient : ITelegramClient
    {
        private readonly ILogger<TelegramClient> _logger;
        private readonly TelegramBotClient _botClient;

        public TelegramClient(ILogger<TelegramClient> logger, IConfiguration configuration)
        {
            var telegramKey = configuration.GetSection("Channels:Telegram").Value;
            if (telegramKey is not null)
            {
                _logger = logger;
                _botClient = new TelegramBotClient(telegramKey);
                return;
            }

            throw new ArgumentException("Could not find telegram channel key");
        }
        public Task ListenForRegisters(CancellationToken cts)
        {
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = new UpdateType[]
                {
                    UpdateType.Message,
                    UpdateType.ChatJoinRequest
                }
            };

            _botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts
            );

            return Task.CompletedTask;
        }

        public async Task SendMessage(long chatId, string message, CancellationToken? stoppingToken = null)
        {
            if (stoppingToken is null)
            {
                await _botClient.SendTextMessageAsync(chatId: chatId, text: message);
                return;
            }

            await _botClient.SendTextMessageAsync(chatId: chatId, text: message, cancellationToken: stoppingToken.Value);
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message)
            {
                return;
            }

            if (message.Text is not { } messageText)
            {
                return;
            }

            if (message.Text.Equals("/start"))
            {
                var response = "Bem vindo ao Remind.me!\ndigite '/chatid' para obter o id de seu canal pessoal de notificações";
                await SendMessage(message.Chat.Id, response, cancellationToken);
            }

            if (message.Text.Equals("/chatid"))
            {
                _logger.Log(LogLevel.Information, $"New chat user registered under id: {message.Chat.Id}");
                await SendMessage(message.Chat.Id, $"Seu chat id individual é: {message.Chat.Id}", cancellationToken);
            }
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
