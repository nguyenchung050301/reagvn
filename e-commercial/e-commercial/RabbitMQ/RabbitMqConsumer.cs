using System.Text.Json;
using e_commercial.DTOs.Request.User;
using e_commercial.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class RabbitMqConsumer
{
    private readonly IChannel _channel;
    private readonly AsyncEventingBasicConsumer _consumer;
   // private readonly IServiceProvider _serviceProvider;
    public RabbitMqConsumer(IChannel channel, IServiceProvider serviceProvider)
    {
        _channel = channel;
        _consumer = new AsyncEventingBasicConsumer(_channel);
      //  _serviceProvider = serviceProvider;
      //  RegisterEventHandler<UserCreateDTO>("register-mail-queue", HandleMessage);
    }

    // private async Task HandleMessage(UserCreateDTO dto)
    // {
    //     System.Console.WriteLine("HANDLERRR");
    //     using var scope = _serviceProvider.CreateScope();
    //     var mailService = scope.ServiceProvider.GetRequiredService<MailService>();
    //     await mailService.SendMessageRegister(dto);
    // }

    public void RegisterEventHandler<TMessage>(string queueName, Func<TMessage, Task> messageHandler)
    {
        _channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        _consumer.ReceivedAsync += async (sender, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = JsonSerializer.Deserialize<TMessage>(body);

                await messageHandler(message); //function with TMessage type parameter => output Task

                await _channel.BasicAckAsync(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                await _channel.BasicNackAsync(ea.DeliveryTag, false, true);
            }
        };

        _channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: _consumer);
    }
}