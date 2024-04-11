namespace Wonderfood.Worker.Interfaces;


public interface ISender
{
    Task SendMessageAsync<T>(T serviceBusMessage, string queueName);
}
