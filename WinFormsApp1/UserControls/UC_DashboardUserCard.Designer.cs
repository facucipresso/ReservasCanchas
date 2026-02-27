namespace WinFormsApp1.UserControls
{
    partial class UC_DashboardUserCard
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
            tlpUsers = new TableLayoutPanel();
            lblUserName = new Label();
            lblUserRol = new Label();
            tlpUsers.SuspendLayout();
            SuspendLayout();
            // 
            // tlpUsers
            // 
            tlpUsers.ColumnCount = 2;
            tlpUsers.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tlpUsers.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tlpUsers.Controls.Add(lblUserName, 0, 0);
            tlpUsers.Controls.Add(lblUserRol, 1, 0);
            tlpUsers.Dock = DockStyle.Fill;
            tlpUsers.Location = new Point(0, 0);
            tlpUsers.Name = "tlpUsers";
            tlpUsers.RowCount = 1;
            tlpUsers.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpUsers.Size = new Size(362, 98);
            tlpUsers.TabIndex = 0;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Dock = DockStyle.Fill;
            lblUserName.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUserName.Location = new Point(3, 0);
            lblUserName.Name = "lblUserName";
            lblUserName.Padding = new Padding(5);
            lblUserName.Size = new Size(211, 98);
            lblUserName.TabIndex = 0;
            lblUserName.Text = "User Name";
            lblUserName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblUserRol
            // 
            lblUserRol.AutoSize = true;
            lblUserRol.Dock = DockStyle.Fill;
            lblUserRol.ForeColor = SystemColors.ControlDarkDark;
            lblUserRol.Location = new Point(220, 0);
            lblUserRol.Name = "lblUserRol";
            lblUserRol.Size = new Size(139, 98);
            lblUserRol.TabIndex = 1;
            lblUserRol.Text = "Rol";
            lblUserRol.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // UC_DashboardUserCard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(tlpUsers);
            Name = "UC_DashboardUserCard";
            Size = new Size(362, 98);
            tlpUsers.ResumeLayout(false);
            tlpUsers.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpUsers;
        private Label lblUserName;
        private Label lblUserRol;
    }
}
