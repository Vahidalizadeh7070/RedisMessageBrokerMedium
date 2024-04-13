using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisBroker
{
    public class RedisMessageBroker
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly ISubscriber _subscriber;
        public RedisMessageBroker(string connectionString)
        {
            _redis = ConnectionMultiplexer.Connect(connectionString);
            _subscriber = _redis.GetSubscriber();
        }

        // Publish message
        public async Task<bool> Publish(string channel, string message)
        {
            try
            {
                await _subscriber.PublishAsync(RedisChannel.Literal(channel), message);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Subscribe message
        public async Task<bool> Subscribe(string channel, Action<RedisChannel, RedisValue> handle)
        {
            try
            {
                await _subscriber.SubscribeAsync(RedisChannel.Literal(channel), handle);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
