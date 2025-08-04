public interface IRabbitPublisher
{
  Task PublishAsync(string message);
}