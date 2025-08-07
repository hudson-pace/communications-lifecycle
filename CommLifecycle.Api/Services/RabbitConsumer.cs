using System.Text;
using System.Text.Json;
using CommLifecycle.Api.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SharedModels.DTOs;
using SharedModels.Mappers;

public class RabbitConsumer : BackgroundService
{
  private readonly IConnection _connection;
  private readonly Lazy<Task<IChannel>> _channel;
  private readonly string _queueName = "messageQueue";
  private readonly ILogger<RabbitConsumer> _logger;
  private readonly RabbitPublisher _rabbitPublisher;
  private readonly ICommunicationService _communicationService;
  public RabbitConsumer(IConnection connection, ILogger<RabbitConsumer> logger, RabbitPublisher rabbitPublisher, ICommunicationService communicationService)
  {
    _connection = connection;
    _channel = new(() => _connection.CreateChannelAsync());
    _logger = logger;
    _rabbitPublisher = rabbitPublisher;
    _communicationService = communicationService;
  }
  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    var channel = await _channel.Value;
    await channel.QueueDeclareAsync(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
    var consumer = new AsyncEventingBasicConsumer(channel);

    Console.WriteLine("Waiting for messages...");
    consumer.ReceivedAsync += async (model, ea) =>
    {
      var body = ea.Body.ToArray();
      var json = Encoding.UTF8.GetString(body);
      StatusChangeMessageDto dto = JsonSerializer.Deserialize<StatusChangeMessageDto>(json);
      Console.WriteLine("Consumed by API.");
      await _communicationService.AppendCommunicationStatusChangeAsync(dto.CommunicationId, dto.ToCommunicationStatusChangeDto(), stoppingToken);
      await _rabbitPublisher.PublishAsync(dto.CommunicationId.ToString());
    };
    await channel.BasicConsumeAsync(queue: _queueName, autoAck: true, consumer: consumer, cancellationToken: stoppingToken);
  }
}