using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class RabbitConsumer : BackgroundService
{
  private readonly IConnection _connection;
  private readonly Lazy<Task<IChannel>> _channel;
  private readonly string _queueName = "messageQueue";
  private readonly ILogger<RabbitConsumer> _logger;
  private readonly RabbitPublisher _rabbitPublisher;
  public RabbitConsumer(IConnection connection, ILogger<RabbitConsumer> logger, RabbitPublisher rabbitPublisher)
  {
    _connection = connection;
    _channel = new(() => _connection.CreateChannelAsync());
    _logger = logger;
    _rabbitPublisher = rabbitPublisher;
  }
  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    var channel = await _channel.Value;
    await channel.QueueDeclareAsync(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
    var consumer = new AsyncEventingBasicConsumer(channel);

    Console.WriteLine("Waiting for messages...");
    _logger.LogInformation("Waiting for messages...2");
    consumer.ReceivedAsync += async (model, ea) =>
    {
      var body = ea.Body.ToArray();
      var message = Encoding.UTF8.GetString(body);
      Console.WriteLine(message + ": Consumed by API.");
      _logger.LogInformation(message + ": Consumed by API.2");
      await _rabbitPublisher.PublishAsync(message);
    };
    await channel.BasicConsumeAsync(queue: _queueName, autoAck: true, consumer: consumer, cancellationToken: stoppingToken);
  }
}