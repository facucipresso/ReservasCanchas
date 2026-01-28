namespace WinFormsApp1.Models.Complex
{
    partial class ComplexCard
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComplexCard));
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
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelName.Location = new Point(12, 12);
            labelName.MaximumSize = new Size(280, 0);
            labelName.Name = "labelName";
            labelName.Size = new Size(169, 31);
            labelName.TabIndex = 1;
            labelName.Text = "nameeeeeeeee";
            // 
            // labelAddress
            // 
            labelAddress.AutoSize = true;
            labelAddress.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelAddress.Location = new Point(38, 88);
            labelAddress.MaximumSize = new Size(280, 0);
            labelAddress.Name = "labelAddress";
            labelAddress.Size = new Size(169, 23);
            labelAddress.TabIndex = 2;
            labelAddress.Text = "Provincia - Localidad";
            // 
            // labelPhone
            // 
            labelPhone.AutoSize = true;
            labelPhone.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelPhone.Location = new Point(38, 121);
            labelPhone.MaximumSize = new Size(280, 0);
            labelPhone.Name = "labelPhone";
            labelPhone.Size = new Size(95, 23);
            labelPhone.TabIndex = 3;
            labelPhone.Text = "Phoneeeee";
            // 
            // buttonEnter
            // 
            buttonEnter.BackColor = Color.RoyalBlue;
            buttonEnter.FlatStyle = FlatStyle.Flat;
            buttonEnter.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonEnter.ForeColor = Color.LightGray;
            buttonEnter.Location = new Point(18, 233);
            buttonEnter.Name = "buttonEnter";
            buttonEnter.Size = new Size(320, 32);
            buttonEnter.TabIndex = 4;
            buttonEnter.Text = "Entrar";
            buttonEnter.UseVisualStyleBackColor = false;
            buttonEnter.Click += buttonEnter_Click;
            // 
            // labelState
            // 
            labelState.AutoSize = true;
            labelState.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelState.ForeColor = SystemColors.ActiveCaptionText;
            labelState.Location = new Point(12, 185);
            labelState.Name = "labelState";
            labelState.Size = new Size(97, 25);
            labelState.TabIndex = 5;
            labelState.Text = "Estado: ---";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 88);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(20, 21);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(12, 121);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(20, 21);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            // 
            // ComplexCard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(224, 224, 224);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(labelState);
            Controls.Add(buttonEnter);
            Controls.Add(labelPhone);
            Controls.Add(labelAddress);
            Controls.Add(labelName);
            Name = "ComplexCard";
            Size = new Size(354, 304);
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
