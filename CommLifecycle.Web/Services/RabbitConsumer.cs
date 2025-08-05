using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class RabbitConsumer : BackgroundService
{
  private readonly IConnection _connection;
  private readonly Lazy<Task<IChannel>> _channel;
  private readonly string _queueName = "messageQueue2";
  private readonly RabbitPublisher _rabbitPublisher;
  public RabbitConsumer(IConnection connection, RabbitPublisher rabbitPublisher)
  {
    _connection = connection;
    _channel = new(() => _connection.CreateChannelAsync());
    _rabbitPublisher = rabbitPublisher;
  }
  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    var channel = await _channel.Value;
    await channel.QueueDeclareAsync(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
    var consumer = new AsyncEventingBasicConsumer(channel);
    consumer.ReceivedAsync += async (model, ea) =>
    {
      var body = ea.Body.ToArray();
      var message = Encoding.UTF8.GetString(body);
      Console.WriteLine(message + ": CONSUMED BY WEB");
      // await _rabbitPublisher.PublishAsync(message);
    };
    await channel.BasicConsumeAsync(queue: _queueName, autoAck: true, consumer: consumer, cancellationToken: stoppingToken);
  }
}