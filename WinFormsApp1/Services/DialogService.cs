using System.Windows.Forms;
using WinFormsApp1.Forms;

namespace WinFormsApp1.Services
{
    public static class DialogService
    {
        public static void ShowError(Form? owner, string message)
        {
            using var modal = new FormErrorModal(message);

            if (owner != null)
            {
                modal.StartPosition = FormStartPosition.CenterParent;
                modal.ShowDialog(owner);
            }
            else
            {
                // Fallback inteligente
                modal.StartPosition = FormStartPosition.Manual;

                var screen = Screen.FromPoint(Cursor.Position);
                var area = screen.WorkingArea;

                modal.Location = new Point(
                    area.Left + (area.Width - modal.Width) / 2,
                    area.Top + (area.Height - modal.Height) / 2
                );

                modal.ShowDialog();
            }
        }
    }
}
