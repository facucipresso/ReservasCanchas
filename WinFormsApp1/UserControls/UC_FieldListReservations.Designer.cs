namespace WinFormsApp1.UserControls
{
    partial class UC_FieldListReservations
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
            panelTop = new Panel();
            btnVolver = new Button();
            btnCerrar = new Button();
            labelFieldListReservations = new Label();
            panelContent = new Panel();
            flowLayoutPanelFieldReservations = new FlowLayoutPanel();
            labelFieldReservationsEmpty = new Label();
            dgvFieldListReservations = new DataGridView();
            panelTop.SuspendLayout();
            panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFieldListReservations).BeginInit();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.Controls.Add(btnVolver);
            panelTop.Controls.Add(btnCerrar);
            panelTop.Controls.Add(labelFieldListReservations);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(774, 60);
            panelTop.TabIndex = 0;
            // 
            // btnVolver
            // 
            btnVolver.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVolver.Location = new Point(536, 20);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(94, 29);
            btnVolver.TabIndex = 1;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            // 
            // btnCerrar
            // 
            btnCerrar.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCerrar.Location = new Point(651, 20);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(94, 29);
            btnCerrar.TabIndex = 1;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            // 
            // labelFieldListReservations
            // 
            labelFieldListReservations.AutoSize = true;
            labelFieldListReservations.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            labelFieldListReservations.Location = new Point(24, 16);
            labelFieldListReservations.Name = "labelFieldListReservations";
            labelFieldListReservations.Size = new Size(241, 31);
            labelFieldListReservations.TabIndex = 0;
            labelFieldListReservations.Text = "Reservas de la cancha";
            // 
            // panelContent
            // 
            panelContent.Controls.Add(flowLayoutPanelFieldReservations);
            panelContent.Controls.Add(dgvFieldListReservations);
            panelContent.Controls.Add(labelFieldReservationsEmpty);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(0, 60);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(774, 841);
            panelContent.TabIndex = 1;
            // 
            // flowLayoutPanelFieldReservations
            // 
            flowLayoutPanelFieldReservations.AutoScroll = true;
            flowLayoutPanelFieldReservations.Dock = DockStyle.Fill;
            flowLayoutPanelFieldReservations.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanelFieldReservations.Location = new Point(0, 0);
            flowLayoutPanelFieldReservations.Name = "flowLayoutPanelFieldReservations";
            flowLayoutPanelFieldReservations.Size = new Size(774, 841);
            flowLayoutPanelFieldReservations.TabIndex = 0;
            flowLayoutPanelFieldReservations.Visible = false;
            flowLayoutPanelFieldReservations.WrapContents = false;
            // 
            // labelFieldReservationsEmpty
            // 
            labelFieldReservationsEmpty.Dock = DockStyle.Fill;
            labelFieldReservationsEmpty.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Italic);
            labelFieldReservationsEmpty.ForeColor = Color.Gray;
            labelFieldReservationsEmpty.Location = new Point(0, 0);
            labelFieldReservationsEmpty.Name = "labelFieldReservationsEmpty";
            labelFieldReservationsEmpty.Size = new Size(774, 841);
            labelFieldReservationsEmpty.TabIndex = 10;
            labelFieldReservationsEmpty.Text = "Esta cancha aún no recibió reservas.";
            labelFieldReservationsEmpty.TextAlign = ContentAlignment.MiddleCenter;
            labelFieldReservationsEmpty.Visible = false;
            // 
            // dgvFieldListReservations
            // 
            dgvFieldListReservations.AllowUserToAddRows = false;
            dgvFieldListReservations.AllowUserToDeleteRows = false;
            dgvFieldListReservations.BackgroundColor = SystemColors.Control;
            dgvFieldListReservations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFieldListReservations.Dock = DockStyle.Fill;
            dgvFieldListReservations.Location = new Point(0, 0);
            dgvFieldListReservations.MultiSelect = false;
            dgvFieldListReservations.Name = "dgvFieldListReservations";
            dgvFieldListReservations.ReadOnly = true;
            dgvFieldListReservations.RowHeadersWidth = 51;
            dgvFieldListReservations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFieldListReservations.Size = new Size(774, 841);
            dgvFieldListReservations.TabIndex = 1;
            // 
            // UC_FieldListReservations
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(panelContent);
            Controls.Add(panelTop);
            Name = "UC_FieldListReservations";
            Size = new Size(774, 901);
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFieldListReservations).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Panel panelContent;
        private FlowLayoutPanel flowLayoutPanelFieldReservations;
        private Label labelFieldReservationsEmpty;
        private DataGridView dgvFieldListReservations;
        private Button btnCerrar;
        private Label labelFieldListReservations;
        private Button btnVolver;
    }
}
