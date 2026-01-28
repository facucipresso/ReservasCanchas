namespace WinFormsApp1.UserControls
{
    partial class UC_DashboardCard
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
            pictureIcon = new PictureBox();
            lbValue = new Label();
            lblTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureIcon).BeginInit();
            SuspendLayout();
            // 
            // pictureIcon
            // 
            pictureIcon.Location = new Point(15, 25);
            pictureIcon.Name = "pictureIcon";
            pictureIcon.Size = new Size(40, 40);
            pictureIcon.SizeMode = PictureBoxSizeMode.Zoom;
            pictureIcon.TabIndex = 0;
            pictureIcon.TabStop = false;
            // 
            // lbValue
            // 
            lbValue.AutoSize = true;
            lbValue.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbValue.Location = new Point(70, 15);
            lbValue.Name = "lbValue";
            lbValue.Size = new Size(74, 46);
            lbValue.TabIndex = 1;
            lbValue.Text = "\"0\"";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.Gray;
            lblTitle.Location = new Point(72, 55);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(53, 23);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "Titulo";
            // 
            // UC_DashboardCard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            //BackColor = Color.FromArgb(245, 245, 245);
            //BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(lblTitle);
            Controls.Add(lbValue);
            Controls.Add(pictureIcon);
            Margin = new Padding(10);
            Name = "UC_DashboardCard";
            Size = new Size(285, 88);
            ((System.ComponentModel.ISupportInitialize)pictureIcon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureIcon;
        private Label lbValue;
        private Label lblTitle;
    }
}
