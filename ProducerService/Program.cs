using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProducerService.DBConfig;
using ProducerService.MessageService;
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

// Register Message service
builder.Services.AddScoped<IMessageService, MessageService>();

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
