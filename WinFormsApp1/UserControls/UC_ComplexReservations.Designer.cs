namespace WinFormsApp1.UserControls
{
    partial class UC_ComplexReservations
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
            panelTop = new Panel();
            btnVolver = new Button();
            btnCerrar = new Button();
            labelReservasDelComplejo = new Label();
            panelContent = new Panel();
            flowLayoutPanelReservations = new FlowLayoutPanel();
            dgvComplexReservations = new DataGridView();
            labelComplexReservationsEmpty = new Label();
            panelTop.SuspendLayout();
            panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvComplexReservations).BeginInit();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.Controls.Add(btnVolver);
            panelTop.Controls.Add(btnCerrar);
            panelTop.Controls.Add(labelReservasDelComplejo);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(774, 60);
            panelTop.TabIndex = 0;
            // 
            // btnVolver
            // 
            btnVolver.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnVolver.Location = new Point(536, 20);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(94, 29);
            btnVolver.TabIndex = 1;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            // 
            // btnCerrar
            // 
            btnCerrar.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnCerrar.Location = new Point(651, 20);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(94, 29);
            btnCerrar.TabIndex = 2;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            // 
            // labelReservasDelComplejo
            // 
            labelReservasDelComplejo.AutoSize = true;
            labelReservasDelComplejo.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold | FontStyle.Italic);
            labelReservasDelComplejo.Location = new Point(24, 16);
            labelReservasDelComplejo.Name = "labelReservasDelComplejo";
            labelReservasDelComplejo.Size = new Size(244, 31);
            labelReservasDelComplejo.TabIndex = 0;
            labelReservasDelComplejo.Text = "Reservas del complejo";
            // 
            // panelContent
            // 
            panelContent.Controls.Add(flowLayoutPanelReservations);
            panelContent.Controls.Add(dgvComplexReservations);
            panelContent.Controls.Add(labelComplexReservationsEmpty);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(0, 60);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(774, 841);
            panelContent.TabIndex = 1;
            // 
            // flowLayoutPanelReservations
            // 
            flowLayoutPanelReservations.AutoScroll = true;
            flowLayoutPanelReservations.Dock = DockStyle.Fill;
            flowLayoutPanelReservations.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanelReservations.Location = new Point(0, 0);
            flowLayoutPanelReservations.Name = "flowLayoutPanelReservations";
            flowLayoutPanelReservations.Size = new Size(774, 841);
            flowLayoutPanelReservations.TabIndex = 0;
            flowLayoutPanelReservations.Visible = false;
            flowLayoutPanelReservations.WrapContents = false;
            // 
            // dgvComplexReservations
            // 
            dgvComplexReservations.BackgroundColor = SystemColors.Control;
            dgvComplexReservations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvComplexReservations.Dock = DockStyle.Fill;
            dgvComplexReservations.Location = new Point(0, 0);
            dgvComplexReservations.Name = "dgvComplexReservations";
            dgvComplexReservations.RowHeadersWidth = 51;
            dgvComplexReservations.Size = new Size(774, 841);
            dgvComplexReservations.TabIndex = 1;
            // 
            // labelComplexReservationsEmpty
            // 
            labelComplexReservationsEmpty.Dock = DockStyle.Fill;
            labelComplexReservationsEmpty.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Italic);
            labelComplexReservationsEmpty.ForeColor = Color.Gray;
            labelComplexReservationsEmpty.Location = new Point(0, 0);
            labelComplexReservationsEmpty.Name = "labelComplexReservationsEmpty";
            labelComplexReservationsEmpty.Size = new Size(774, 841);
            labelComplexReservationsEmpty.TabIndex = 2;
            labelComplexReservationsEmpty.Text = "Este complejo aún no tiene reservas.";
            labelComplexReservationsEmpty.TextAlign = ContentAlignment.MiddleCenter;
            labelComplexReservationsEmpty.Visible = false;
            // 
            // UC_ComplexReservations
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(panelContent);
            Controls.Add(panelTop);
            Name = "UC_ComplexReservations";
            Size = new Size(774, 901);
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvComplexReservations).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Panel panelContent;
        private FlowLayoutPanel flowLayoutPanelReservations;
        private Label labelComplexReservationsEmpty;
        private DataGridView dgvComplexReservations;
        private Label labelReservasDelComplejo;
        private Button btnCerrar;
        private Button btnVolver;
    }
}




/*namespace WinFormsApp1.UserControls
{
    partial class UC_ComplexReservations
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
            panel1 = new Panel();
            btnVolver = new Button();
            btnCerrar = new Button();
            labelReservasDelComplejo = new Label();
            labelComplexReservationsEmpty = new Label();
            dgvComplexReservations = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvComplexReservations).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnVolver);
            panel1.Controls.Add(btnCerrar);
            panel1.Controls.Add(labelReservasDelComplejo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(774, 60);
            panel1.TabIndex = 0;
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
            // labelReservasDelComplejo
            // 
            labelReservasDelComplejo.AutoSize = true;
            labelReservasDelComplejo.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            labelReservasDelComplejo.Location = new Point(24, 16);
            labelReservasDelComplejo.Name = "labelReservasDelComplejo";
            labelReservasDelComplejo.Size = new Size(244, 31);
            labelReservasDelComplejo.TabIndex = 0;
            labelReservasDelComplejo.Text = "Reservas del complejo";
            // 
            // labelComplexReservationsEmpty
            // 
            labelComplexReservationsEmpty.Dock = DockStyle.Fill;
            labelComplexReservationsEmpty.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Italic);
            labelComplexReservationsEmpty.ForeColor = Color.Gray;
            labelComplexReservationsEmpty.Location = new Point(0, 0);
            labelComplexReservationsEmpty.Name = "labelComplexReservationsEmpty";
            labelComplexReservationsEmpty.Size = new Size(774, 901);
            labelComplexReservationsEmpty.TabIndex = 10;
            labelComplexReservationsEmpty.Text = "Este complejo aún no recibió reseñas.";
            labelComplexReservationsEmpty.TextAlign = ContentAlignment.MiddleCenter;
            labelComplexReservationsEmpty.Visible = false;
            // 
            // dgvComplexReservations
            // 
            dgvComplexReservations.BackgroundColor = SystemColors.Control;
            dgvComplexReservations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvComplexReservations.Dock = DockStyle.Fill;
            dgvComplexReservations.Location = new Point(0, 0);
            dgvComplexReservations.Name = "dgvComplexReservations";
            dgvComplexReservations.RowHeadersWidth = 51;
            dgvComplexReservations.Size = new Size(774, 901);
            dgvComplexReservations.TabIndex = 1;
            // 
            // UC_ComplexReservations
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(panel1);
            Controls.Add(labelComplexReservationsEmpty);
            Controls.Add(dgvComplexReservations);
            Name = "UC_ComplexReservations";
            Size = new Size(774, 901);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvComplexReservations).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1; 
        private Label labelComplexReservationsEmpty;
        private DataGridView dgvComplexReservations;
        private Label labelReservasDelComplejo;
        private Button btnCerrar;
        private Button btnVolver;
    }
}*/
