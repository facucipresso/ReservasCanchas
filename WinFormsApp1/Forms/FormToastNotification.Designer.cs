namespace WinFormsApp1.Forms
{
    partial class FormToastNotification
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblMessage;
        private Panel panelIndicator;
        private System.Windows.Forms.Timer timerClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblMessage = new Label();
            panelIndicator = new Panel();
            timerClose = new System.Windows.Forms.Timer(components);

            SuspendLayout();

            // panelIndicator
            panelIndicator.Location = new Point(15, 15);
            panelIndicator.Size = new Size(30, 30);
            panelIndicator.BackColor = Color.SeaGreen;

            // lblMessage
            lblMessage.Location = new Point(60, 0);
            lblMessage.Size = new Size(340, 60);
            lblMessage.TextAlign = ContentAlignment.MiddleLeft;
            lblMessage.Font = new Font("Segoe UI", 10F);
            lblMessage.ForeColor = Color.Black;

            // timerClose
            timerClose.Interval = 3000;
            timerClose.Tick += timerClose_Tick;

            // FormToastNotification
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(420, 60);
            Controls.Add(panelIndicator);
            Controls.Add(lblMessage);
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            Name = "FormToastNotification";

            ResumeLayout(false);
        }
    }
}