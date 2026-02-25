namespace WinFormsApp1.Models.Complex
{
    partial class ComplexCard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(ComplexCard));

            labelName = new Label();
            labelAddress = new Label();
            labelPhone = new Label();
            buttonEnter = new Button();
            labelState = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();

            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();

            // labelName
            labelName.AutoSize = true;
            labelName.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            labelName.Location = new Point(12, 12);
            labelName.MaximumSize = new Size(280, 0);
            labelName.Text = "Nombre Complejo";

            // labelAddress
            labelAddress.AutoSize = true;
            labelAddress.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            labelAddress.Location = new Point(38, 70);
            labelAddress.MaximumSize = new Size(280, 0);
            labelAddress.Text = "Provincia - Localidad";

            // labelPhone
            labelPhone.AutoSize = true;
            labelPhone.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            labelPhone.Location = new Point(38, 100);
            labelPhone.MaximumSize = new Size(280, 0);
            labelPhone.Text = "Dirección · Teléfono";

            // labelState
            labelState.AutoSize = true;
            labelState.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            labelState.Location = new Point(12, 140);
            labelState.Text = "Estado: ---";

            // buttonEnter
            buttonEnter.BackColor = Color.RoyalBlue;
            buttonEnter.FlatStyle = FlatStyle.Flat;
            buttonEnter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonEnter.ForeColor = Color.White;
            buttonEnter.Size = new Size(320, 32);
            buttonEnter.Text = "Entrar";
            buttonEnter.UseVisualStyleBackColor = false;
            buttonEnter.Click += buttonEnter_Click;

            // pictureBox1
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 72);
            pictureBox1.Size = new Size(20, 20);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            // pictureBox2
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(12, 102);
            pictureBox2.Size = new Size(20, 20);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            // ComplexCard
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(labelState);
            Controls.Add(buttonEnter);
            Controls.Add(labelPhone);
            Controls.Add(labelAddress);
            Controls.Add(labelName);
            Name = "ComplexCard";
            Size = new Size(350, 220);

            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelName;
        private Label labelAddress;
        private Label labelPhone;
        private Button buttonEnter;
        private Label labelState;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
    }
}
