using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;

public class RabbitPublisher : BackgroundService
{
  private readonly IConnection _connection;
  private IChannel? _channel;
  private readonly TaskCompletionSource _channelReady = new();
  private readonly string _queueName = "messageQueue2";
  public RabbitPublisher(IConnection connection)
  {
    _connection = connection;

  }
  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    _channel = await _connection.CreateChannelAsync();
    await _channel.QueueDeclareAsync(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
    _channelReady.SetResult();
  }
  public async Task PublishAsync(string message)
  {
    await _channelReady.Task;
    Console.WriteLine($"API Pub: {message}");
    if (_channel is not null)
    {
      var body = Encoding.UTF8.GetBytes(message);
      await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: _queueName, body: body);
    }
    else
    {
      Console.WriteLine("Channel is null.");
    }
  }
  public async Task PublishReloadAlert(int communicationId)
  {
    await PublishAsync(communicationId.ToString());
  }
}