namespace CommLifecycle.Web.Services;

public class ToastService
{
  public event Action<string, string>? OnShow;

  public void ShowError(string message, string title = "Error")
  {
    OnShow?.Invoke(title, message);
  }
}