namespace WinFormsApp1
{
    partial class FormLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            LblBienvenida = new Label();
            textBoxUserName = new TextBox();
            textBoxPassword = new TextBox();
            buttonIngresar = new Button();
            labelMsjBienvenida = new Label();
            panel1 = new Panel();
            pictureBox3 = new PictureBox();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            linkLabel1 = new LinkLabel();
            label1 = new Label();
            label2 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // LblBienvenida
            // 
            LblBienvenida.AutoSize = true;
            LblBienvenida.Font = new Font("Calibri", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LblBienvenida.ForeColor = Color.DimGray;
            LblBienvenida.Location = new Point(430, 9);
            LblBienvenida.Name = "LblBienvenida";
            LblBienvenida.Size = new Size(106, 41);
            LblBienvenida.TabIndex = 1;
            LblBienvenida.Text = "LOGIN";
            // 
            // textBoxUserName
            // 
            textBoxUserName.BackColor = Color.FromArgb(15, 15, 15);
            textBoxUserName.BorderStyle = BorderStyle.None;
            textBoxUserName.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxUserName.ForeColor = Color.DimGray;
            textBoxUserName.Location = new Point(284, 83);
            textBoxUserName.Name = "textBoxUserName";
            textBoxUserName.Size = new Size(458, 25);
            textBoxUserName.TabIndex = 1;
            textBoxUserName.Text = "USUARIO";
            textBoxUserName.Enter += textBoxUserName_Enter;
            textBoxUserName.Leave += textBoxUserName_Leave;
            // 
            // textBoxPassword
            // 
            textBoxPassword.BackColor = Color.FromArgb(15, 15, 15);
            textBoxPassword.BorderStyle = BorderStyle.None;
            textBoxPassword.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxPassword.ForeColor = Color.DimGray;
            textBoxPassword.Location = new Point(284, 140);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.Size = new Size(458, 25);
            textBoxPassword.TabIndex = 2;
            textBoxPassword.Text = "CONTRASEÑA";
            textBoxPassword.Enter += textBoxPassword_Enter;
            textBoxPassword.Leave += textBoxPassword_Leave;
            // 
            // buttonIngresar
            // 
            buttonIngresar.BackColor = Color.FromArgb(40, 40, 40);
            buttonIngresar.FlatAppearance.BorderSize = 0;
            buttonIngresar.FlatAppearance.MouseDownBackColor = Color.FromArgb(28, 28, 28);
            buttonIngresar.FlatAppearance.MouseOverBackColor = Color.FromArgb(64, 64, 64);
            buttonIngresar.FlatStyle = FlatStyle.Flat;
            buttonIngresar.ForeColor = Color.LightGray;
            buttonIngresar.Location = new Point(284, 199);
            buttonIngresar.Name = "buttonIngresar";
            buttonIngresar.Size = new Size(458, 40);
            buttonIngresar.TabIndex = 3;
            buttonIngresar.Text = "INGRESAR";
            buttonIngresar.UseVisualStyleBackColor = false;
            buttonIngresar.Click += buttonIngresar_Click;
            // 
            // labelMsjBienvenida
            // 
            labelMsjBienvenida.AutoSize = true;
            labelMsjBienvenida.ForeColor = Color.FromArgb(15, 15, 15);
            labelMsjBienvenida.Location = new Point(257, 266);
            labelMsjBienvenida.Name = "labelMsjBienvenida";
            labelMsjBienvenida.Size = new Size(162, 20);
            labelMsjBienvenida.TabIndex = 4;
            labelMsjBienvenida.Text = "Mensaje de bienvenida";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 122, 204);
            panel1.Controls.Add(pictureBox3);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(250, 330);
            panel1.TabIndex = 9;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.logo_canchaYa;
            pictureBox3.Location = new Point(3, 46);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(247, 240);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 0;
            pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(753, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(18, 18);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(729, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(18, 18);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 11;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.ForeColor = Color.FromArgb(15, 15, 15);
            linkLabel1.LinkColor = Color.FromArgb(15, 15, 15);
            linkLabel1.Location = new Point(515, 268);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(76, 20);
            linkLabel1.TabIndex = 0;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "linkLabel1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.DimGray;
            label1.Location = new Point(273, 109);
            label1.Name = "label1";
            label1.Size = new Size(483, 20);
            label1.TabIndex = 12;
            label1.Text = "_______________________________________________________________________________";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.DimGray;
            label2.Location = new Point(273, 166);
            label2.Name = "label2";
            label2.Size = new Size(483, 20);
            label2.TabIndex = 12;
            label2.Text = "_______________________________________________________________________________";
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 15, 15);
            ClientSize = new Size(780, 330);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(linkLabel1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            Controls.Add(labelMsjBienvenida);
            Controls.Add(buttonIngresar);
            Controls.Add(textBoxPassword);
            Controls.Add(textBoxUserName);
            Controls.Add(LblBienvenida);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormLogin";
            Opacity = 0.9D;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login Super User";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label LblBienvenida;
        private TextBox textBoxUserName;
        private TextBox textBoxPassword;
        private Button buttonIngresar;
        private Label labelMsjBienvenida;
        private Panel panel1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private LinkLabel linkLabel1;
        private Label label1;
        private Label label2;
    }
}
