public class PageEventDispatcher
{
  private readonly List<Func<string, Task>> _subscribers = new();
  public void Subscribe(Func<string, Task> handler)
  {
    _subscribers.Add(handler);
  }
  public void UnSubscribe(Func<string, Task> handler)
  {
    _subscribers.Remove(handler);
  }
  public async Task DispatchAsync(string eventData)
  {
    foreach (var handler in _subscribers)
    {
      await handler.Invoke(eventData);
    }
  }
}