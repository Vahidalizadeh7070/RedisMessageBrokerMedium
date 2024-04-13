using ConsumerService.DBConfig;
using ConsumerService.Services;
using Microsoft.Extensions.Options;
using RedisBroker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Register Redis as a message broker
// Best practice: Use DI in the RedisBroker class library to have a clean code section inside the Program.cs file
builder.Services.Configure<RedisConfig>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddSingleton<RedisMessageBroker>(sb =>
{
    var config = sb.GetRequiredService<IOptions<RedisConfig>>().Value;
    return new RedisMessageBroker(config.RedisCacheURL);
});

builder.Services.AddSingleton<ConsumerServices>();

var app = builder.Build();

var messageBroker = app.Services.GetRequiredService<RedisMessageBroker>();
var consumerService = app.Services.GetRequiredService<ConsumerServices>();

await messageBroker.Subscribe("MessageChannel", (channel, message) =>
{
    consumerService.HandleMessage(channel, message);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
