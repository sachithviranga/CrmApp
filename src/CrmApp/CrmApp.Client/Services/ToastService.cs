using System;
using System.Timers;

namespace CrmApp.Client.Services
{
    public class ToastService
    {
        public event Action<string, ToastLevel>? OnShow;
        public event Action? OnHide;

        private System.Timers.Timer? _timer;

        public void ShowToast(string message, ToastLevel level = ToastLevel.Info, int duration = 3000)
        {
            OnShow?.Invoke(message, level);

            _timer?.Stop();
            _timer = new System.Timers.Timer(duration);
            _timer.Elapsed += (_, _) =>
            {
                HideToast();
                _timer?.Dispose();
                _timer = null;
            };
            _timer.Start();
        }

        public void ShowSuccess(string message) => ShowToast(message, ToastLevel.Success);
        public void ShowError(string message) => ShowToast(message, ToastLevel.Error);
        public void ShowInfo(string message) => ShowToast(message, ToastLevel.Info);

        public void HideToast()
        {
            OnHide?.Invoke();
        }
    }

    public enum ToastLevel
    {
        Success,
        Error,
        Info
    }
}
