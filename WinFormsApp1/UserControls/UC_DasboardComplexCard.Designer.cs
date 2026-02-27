namespace WinFormsApp1.UserControls
{
    partial class UC_DasboardComplexCard
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
            tlpSuperiorComplexName = new TableLayoutPanel();
            lblComplexName = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            lblUserName = new Label();
            lblRating = new Label();
            tlpSuperiorComplexName.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tlpSuperiorComplexName
            // 
            tlpSuperiorComplexName.ColumnCount = 1;
            tlpSuperiorComplexName.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpSuperiorComplexName.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tlpSuperiorComplexName.Controls.Add(lblComplexName, 0, 0);
            tlpSuperiorComplexName.Dock = DockStyle.Top;
            tlpSuperiorComplexName.Location = new Point(0, 0);
            tlpSuperiorComplexName.Name = "tlpSuperiorComplexName";
            tlpSuperiorComplexName.RowCount = 1;
            tlpSuperiorComplexName.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpSuperiorComplexName.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpSuperiorComplexName.Size = new Size(400, 46);
            tlpSuperiorComplexName.TabIndex = 0;
            // 
            // lblComplexName
            // 
            lblComplexName.AutoSize = true;
            lblComplexName.Dock = DockStyle.Fill;
            lblComplexName.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblComplexName.Location = new Point(3, 0);
            lblComplexName.Name = "lblComplexName";
            lblComplexName.Size = new Size(394, 46);
            lblComplexName.TabIndex = 0;
            lblComplexName.Text = "label1";
            lblComplexName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel1.Controls.Add(lblUserName, 0, 0);
            tableLayoutPanel1.Controls.Add(lblRating, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 46);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(400, 48);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Dock = DockStyle.Fill;
            lblUserName.ForeColor = SystemColors.ControlDarkDark;
            lblUserName.Location = new Point(3, 0);
            lblUserName.Name = "lblUserName";
            lblUserName.Padding = new Padding(5);
            lblUserName.Size = new Size(274, 48);
            lblUserName.TabIndex = 0;
            lblUserName.Text = "label1";
            lblUserName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblRating
            // 
            lblRating.AutoSize = true;
            lblRating.Dock = DockStyle.Fill;
            lblRating.ForeColor = SystemColors.ControlDarkDark;
            lblRating.Location = new Point(283, 0);
            lblRating.Name = "lblRating";
            lblRating.Size = new Size(114, 48);
            lblRating.TabIndex = 1;
            lblRating.Text = "★★★★★";
            lblRating.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // UC_DasboardComplexCard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(tableLayoutPanel1);
            Controls.Add(tlpSuperiorComplexName);
            Name = "UC_DasboardComplexCard";
            Size = new Size(400, 94);
            tlpSuperiorComplexName.ResumeLayout(false);
            tlpSuperiorComplexName.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpSuperiorComplexName;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblComplexName;
        private Label lblUserName;
        private Label lblRating;
    }
}
