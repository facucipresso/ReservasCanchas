using WinFormsApp1.Enum;
using WinFormsApp1.Forms;

namespace WinFormsApp1.Services
{
    public static class Notifier
    {
        private static FormToastNotification? _currentToast;

        public static void Show(string message, NotificationType type)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            _currentToast?.Close();

            _currentToast = new FormToastNotification(message, type);
            _currentToast.Show();
        }
    }
}