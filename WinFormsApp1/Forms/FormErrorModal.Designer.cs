namespace WinFormsApp1.Forms
{
    partial class FormErrorModal
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnAccept;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelContainer = new Panel();
            pictureBox1 = new PictureBox();
            btnAccept = new Button();
            lblMessage = new Label();
            lblTitle = new Label();
            panelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panelContainer
            // 
            panelContainer.BackColor = Color.WhiteSmoke;
            panelContainer.Controls.Add(pictureBox1);
            panelContainer.Controls.Add(btnAccept);
            panelContainer.Controls.Add(lblMessage);
            panelContainer.Controls.Add(lblTitle);
            panelContainer.Dock = DockStyle.Fill;
            panelContainer.Location = new Point(0, 0);
            panelContainer.Name = "panelContainer";
            panelContainer.Padding = new Padding(30);
            panelContainer.Size = new Size(300, 280);
            panelContainer.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.warning_triangle;
            pictureBox1.Location = new Point(90, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(125, 97);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // btnAccept
            // 
            btnAccept.BackColor = Color.Black;
            btnAccept.FlatAppearance.BorderSize = 0;
            btnAccept.FlatStyle = FlatStyle.Flat;
            btnAccept.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAccept.ForeColor = Color.White;
            btnAccept.Location = new Point(90, 220);
            btnAccept.Name = "btnAccept";
            btnAccept.Size = new Size(120, 35);
            btnAccept.TabIndex = 0;
            btnAccept.Text = "Aceptar";
            btnAccept.UseVisualStyleBackColor = false;
            btnAccept.Click += btnAccept_Click;
            // 
            // lblMessage
            // 
            lblMessage.Font = new Font("Segoe UI", 10F);
            lblMessage.Location = new Point(0, 140);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(300, 60);
            lblMessage.TabIndex = 1;
            lblMessage.Text = "Mensaje de error";
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(0, 100);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(300, 30);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "Error";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormErrorModal
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(300, 280);
            Controls.Add(panelContainer);
            Name = "FormErrorModal";
            ShowInTaskbar = false;
            panelContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }
        private PictureBox pictureBox1;
    }
}