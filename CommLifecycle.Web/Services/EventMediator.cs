using System.Collections.Concurrent;

public class EventMediator
{
  private readonly ConcurrentDictionary<string, Func<string, Task>> _subscribers = new();

  public void Subscribe(string key, Func<string, Task> handler)
  {
    _subscribers[key] = handler;
  }

  public void Unsubscribe(string key)
  {
    _subscribers.TryRemove(key, out _);
  }

  public async Task BroadcastAsync(string message)
  {
    foreach (var handler in _subscribers.Values)
    {
      await handler.Invoke(message);
    }
  }
}