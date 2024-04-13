using ProducerService.Models;
using RedisBroker;
using System.Text.Json;

namespace ProducerService.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly RedisMessageBroker _redisMessageBroker;

        public MessageService(RedisMessageBroker redisMessageBroker)
        {
            _redisMessageBroker = redisMessageBroker;
        }
        public async Task<Message> SendMessage(Message message)
        {
            var res = await _redisMessageBroker.Publish("MessageChannel", JsonSerializer.Serialize(message));
            if(res is true)
            {
                return message;
            }
            return null;
        }
    }
}
