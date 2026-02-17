using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WinFormsApp1.Enum;

namespace WinFormsApp1.Forms
{
    public partial class FormToastNotification : Form
    {
        // 🔥 CLAVE: no roba foco
        protected override bool ShowWithoutActivation => true;

        public FormToastNotification(string message, NotificationType type)
        {
            InitializeComponent();

            lblMessage.Text = message;
            SetColor(type);
            MakeIndicatorCircular();

            timerClose.Start();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            PositionForm();
        }

        private void SetColor(NotificationType type)
        {
            Color color = type switch
            {
                NotificationType.Success => Color.SeaGreen,
                NotificationType.Error => Color.Firebrick,
                NotificationType.Warning => Color.DarkOrange,
                NotificationType.Info => Color.DodgerBlue,
                _ => Color.Gray
            };

            panelIndicator.BackColor = color;
        }

        private void PositionForm()
        {
            var screen = Screen.PrimaryScreen.WorkingArea;

            Location = new Point(
                screen.Right - Width - 20,
                screen.Top + 20
            );
        }

        private void timerClose_Tick(object sender, EventArgs e)
        {
            timerClose.Stop();
            Close();
        }

        private void MakeIndicatorCircular()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, panelIndicator.Width, panelIndicator.Height);
            panelIndicator.Region = new Region(path);
        }
    }
}