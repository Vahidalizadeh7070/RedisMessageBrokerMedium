using ConsumerService.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace ConsumerService.Services
{
    public class ConsumerServices
    {
        private readonly ILogger<ConsumerServices> _logger;

        public ConsumerServices(ILogger<ConsumerServices> logger)
        {
            _logger = logger;
        }

        public void HandleMessage(RedisChannel redisChannel, RedisValue message)
        {
            var messageRes = JsonSerializer.Deserialize<Message>(message);
            _logger.Log(LogLevel.Information, messageRes.Body);
        }
    }
}
