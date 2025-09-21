namespace CrmApp.Client.Services
{
    public class ToastService
    {
        public event Func<string, ToastLevel, Task>? OnShow;
        public event Func<Task>? OnHide;

        public async Task ShowToastAsync(string message, ToastLevel level = ToastLevel.Info, int duration = 3000)
        {
            if (OnShow != null)
            {
                await OnShow.Invoke(message, level);

                await Task.Delay(duration);

                if (OnHide != null)
                {
                    await OnHide.Invoke();
                }
            }
        }

        public Task ShowSuccess(string message, int duration = 3000) => ShowToastAsync(message, ToastLevel.Success, duration);
        public Task ShowError(string message, int duration = 3000) => ShowToastAsync(message, ToastLevel.Error, duration);
        public Task ShowInfo(string message, int duration = 3000) => ShowToastAsync(message, ToastLevel.Info, duration);
    }

    public enum ToastLevel
    {
        Success,
        Error,
        Info
    }
}
