namespace MtgInventoryApp.Services;

public enum ToastLevel { Info, Success, Warning, Error }

public class ToastMessage
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Message { get; init; } = "";
    public ToastLevel Level { get; init; }
}

public class ToastService
{
    private readonly List<ToastMessage> _toasts = new();
    public IReadOnlyList<ToastMessage> Toasts => _toasts;

    public event Action? OnChange;

    public void Show(string message, ToastLevel level = ToastLevel.Error)
    {
        var toast = new ToastMessage { Message = message, Level = level };
        _toasts.Add(toast);
        OnChange?.Invoke();
        _ = RemoveAfterDelay(toast.Id);
    }

    public void Remove(Guid id)
    {
        _toasts.RemoveAll(t => t.Id == id);
        OnChange?.Invoke();
    }

    private async Task RemoveAfterDelay(Guid id)
    {
        await Task.Delay(5000);
        Remove(id);
    }
}
