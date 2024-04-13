using ProducerService.Models;

namespace ProducerService.MessageService
{
    public interface IMessageService
    {
        Task<Message> SendMessage(Message message);
    }
}
