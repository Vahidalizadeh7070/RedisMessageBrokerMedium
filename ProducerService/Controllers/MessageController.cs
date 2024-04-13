using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProducerService.MessageService;
using ProducerService.Models;

namespace ProducerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly ILogger<MessageController> _logger;

        public MessageController(IMessageService messageService, ILogger<MessageController> logger)
        {
            _messageService = messageService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Message message)
        {
            var res = await _messageService.SendMessage(message);
            _logger.LogInformation($"{DateTime.Now.ToShortDateString()} : {res.Body}");
            return Ok(res);
        }
    }
}
