using ChannelManager.Api.Utils;
using ChannelManager.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChannelManager.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        private readonly IChannelConfigurationService _channelConfigurationService;

        public ChannelController(IChannelConfigurationService channelConfigurationService)
        {
            _channelConfigurationService = channelConfigurationService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var userId = HttpContext.UserId();
            return Ok(_channelConfigurationService.Read(userId));
        }

        [HttpPost]
        public IActionResult Configure(long telegramChatId)
        {
            var userId = HttpContext.UserId();
            return Ok(_channelConfigurationService.SetChatId(userId, telegramChatId));
        }
    }
}
