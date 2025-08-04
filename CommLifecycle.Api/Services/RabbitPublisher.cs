using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;

public class RabbitPublisher : IRabbitPublisher
{
  private readonly IConnection _connection;
  private readonly Lazy<Task<IChannel>> _channel;
  private readonly string _queueName;
  public RabbitPublisher(string queueName, IConnection connection)
  {
    _connection = connection;
    _channel = new(() => _connection.CreateChannelAsync());
    _queueName = queueName;
  }
  public async Task PublishAsync(string message)
  {
    var channel = await _channel.Value;
    await channel.QueueDeclareAsync(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
    var body = Encoding.UTF8.GetBytes(message);
    await channel.BasicPublishAsync(exchange: string.Empty, routingKey: _queueName, body: body);
  }
}