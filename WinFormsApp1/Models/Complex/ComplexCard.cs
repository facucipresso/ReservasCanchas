using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinFormsApp1.Models.Complex
{
    public partial class ComplexCard : UserControl
    {
        private ComplexSuperAdminResponseDTO _data;

        public event Action<int?, string?, string?> EnterClicked;

        public ComplexCard()
        {
            InitializeComponent();
            SetupDefaultStyles();
        }

        private void SetupDefaultStyles()
        {
            this.Padding = new Padding(10);
            this.Margin = new Padding(10);
            this.Width = 350;
            this.TabStop = false;

            ApplyRoundedCorners(12);
        }

        public void SetData(ComplexSuperAdminResponseDTO dto)
        {
            _data = dto;

            labelName.Text = dto.Name;
            labelAddress.Text = $"{dto.Province} - {dto.Locality}";
            labelPhone.Text = $"{dto.Street} {dto.Number} · {dto.Phone}";
            labelState.Text = $"Estado: {dto.ComplexState}";

            if (dto.ComplexState.Equals("Pendiente"))
                labelState.ForeColor = Color.Orange;
            else if (dto.ComplexState.Equals("Habilitado"))
                labelState.ForeColor = Color.Green;
            else
                labelState.ForeColor = Color.Red;

            ReorganizeLayout();
        }

        private void ReorganizeLayout()
        {
            int currentY = labelName.Bottom + 15;

            pictureBox1.Top = currentY + 3;
            labelAddress.Top = currentY;

            currentY = labelAddress.Bottom + 10;

            pictureBox2.Top = currentY + 3;
            labelPhone.Top = currentY;

            currentY = labelPhone.Bottom + 15;

            labelState.Top = currentY;

            currentY = labelState.Bottom + 20;

            buttonEnter.Top = currentY;
            buttonEnter.Left = 15;
            buttonEnter.Width = this.Width - 30;

            this.Height = buttonEnter.Bottom + 20;

            ApplyRoundedCorners(12);
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            EnterClicked?.Invoke(_data?.Id, _data?.NameUser, _data?.LastNameUser);
        }

        private void ApplyRoundedCorners(int radius)
        {
            var path = new GraphicsPath();
            int diameter = radius * 2;

            path.StartFigure();
            path.AddArc(0, 0, diameter, diameter, 180, 90);
            path.AddArc(Width - diameter, 0, diameter, diameter, 270, 90);
            path.AddArc(Width - diameter, Height - diameter, diameter, diameter, 0, 90);
            path.AddArc(0, Height - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            this.Region = new Region(path);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ApplyRoundedCorners(12);
        }
    }
}


