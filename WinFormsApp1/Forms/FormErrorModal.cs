using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinFormsApp1.Forms
{
    public partial class FormErrorModal : Form
    {
        private readonly string _message;

        public FormErrorModal(string message)
        {
            InitializeComponent();
            _message = message;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            lblMessage.Text = _message;

            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.White;

            ApplyRoundedCorners();
            //MakeIconCircular();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ApplyRoundedCorners()
        {
            var path = new GraphicsPath();
            int radius = 20;

            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(Width - radius, Height - radius, radius, radius, 0, 90);
            path.AddArc(0, Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();

            Region = new Region(path);
        }

        

    }
}
