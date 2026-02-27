namespace WinFormsApp1.UserControls
{
    partial class UC_DashboardLastComplexCard
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
            tlpComplexCard = new TableLayoutPanel();
            lblComplexName = new Label();
            lblComplexState = new Label();
            tlpComplexCard.SuspendLayout();
            SuspendLayout();
            // 
            // tlpComplexCard
            // 
            tlpComplexCard.ColumnCount = 2;
            tlpComplexCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 63F));
            tlpComplexCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 37F));
            tlpComplexCard.Controls.Add(lblComplexName, 0, 0);
            tlpComplexCard.Controls.Add(lblComplexState, 1, 0);
            tlpComplexCard.Dock = DockStyle.Fill;
            tlpComplexCard.Location = new Point(0, 0);
            tlpComplexCard.Name = "tlpComplexCard";
            tlpComplexCard.RowCount = 1;
            tlpComplexCard.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpComplexCard.Size = new Size(412, 112);
            tlpComplexCard.TabIndex = 0;
            // 
            // lblComplexName
            // 
            lblComplexName.AutoSize = true;
            lblComplexName.Dock = DockStyle.Fill;
            lblComplexName.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblComplexName.Location = new Point(3, 0);
            lblComplexName.Name = "lblComplexName";
            lblComplexName.Padding = new Padding(5);
            lblComplexName.Size = new Size(253, 112);
            lblComplexName.TabIndex = 0;
            lblComplexName.Text = "ComplexName";
            lblComplexName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblComplexState
            // 
            lblComplexState.AutoSize = true;
            lblComplexState.Dock = DockStyle.Fill;
            lblComplexState.ForeColor = SystemColors.ControlDarkDark;
            lblComplexState.Location = new Point(262, 0);
            lblComplexState.Name = "lblComplexState";
            lblComplexState.Size = new Size(147, 112);
            lblComplexState.TabIndex = 1;
            lblComplexState.Text = "State";
            lblComplexState.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // UC_DashboardLastComplexCard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(tlpComplexCard);
            Name = "UC_DashboardLastComplexCard";
            Size = new Size(412, 112);
            tlpComplexCard.ResumeLayout(false);
            tlpComplexCard.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpComplexCard;
        private Label lblComplexName;
        private Label lblComplexState;
    }
}
