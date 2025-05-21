using EltafawkPlatform.Dto;
using EltafawkPlatform.Mapper;

using EltafawkPlatform.Models;
using EltafawkPlatform.Services;
using Microsoft.AspNetCore.Mvc;

namespace EltafawkPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InboxController : ControllerBase
    {
        private readonly InboxService _inboxService;

        public InboxController(InboxService inboxService)
        {
            _inboxService = inboxService;
        }

        [HttpGet("received/{userId}")]
        public async Task<IActionResult> GetReceived(string userId)
        {
            var messages = await _inboxService.GetReceivedMessagesAsync(userId);
            var dtos = messages.Select(m => m.ToDto()).ToList();
            return Ok(dtos);
        }

        [HttpGet("sent/{userId}")]
        public async Task<IActionResult> GetSent(string userId)
        {
            var messages = await _inboxService.GetSentMessagesAsync(userId);
            var dtos = messages.Select(m => m.ToDto()).ToList();
            return Ok(dtos);
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] InboxMessageDto dto)
        {
            var model = dto.ToModel();
            var result = await _inboxService.SendMessageAsync(model);
            return Ok(result.ToDto());
        }

        [HttpPost("{messageId}/reply")]
        public async Task<IActionResult> Reply(string messageId, [FromBody] ReplyDto replyDto)
        {
            var replyModel =  new ReplyModel
            {
                SenderId = replyDto.SenderId,
                Body = replyDto.Body,
                //SentAt = replyDto.SentAt
            };

            await _inboxService.AddReplyAsync(messageId, replyModel);
            return Ok();
        }

        [HttpPut("{messageId}/read")]
        public async Task<IActionResult> MarkRead(string messageId)
        {
            await _inboxService.MarkAsReadAsync(messageId);
            return Ok();
        }
    }
}
