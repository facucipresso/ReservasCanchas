namespace WinFormsApp1.UserControls
{
    partial class UC_Notificaciones
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_Notificaciones));
            dgvNotificaciones = new DataGridView();
            ColumnButtonMarkAsReaded = new DataGridViewImageColumn();
            ColumnIngresarNotificacion = new DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)dgvNotificaciones).BeginInit();
            SuspendLayout();
            // 
            // dgvNotificaciones
            // 
            dgvNotificaciones.AllowUserToAddRows = false;
            dgvNotificaciones.AllowUserToResizeColumns = false;
            dgvNotificaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNotificaciones.BackgroundColor = SystemColors.Control;
            dgvNotificaciones.BorderStyle = BorderStyle.None;
            dgvNotificaciones.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dgvNotificaciones.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.CornflowerBlue;
            dataGridViewCellStyle1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = Color.CornflowerBlue;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvNotificaciones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvNotificaciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNotificaciones.Columns.AddRange(new DataGridViewColumn[] { ColumnButtonMarkAsReaded, ColumnIngresarNotificacion });
            dgvNotificaciones.EnableHeadersVisualStyles = false;
            dgvNotificaciones.Location = new Point(3, 3);
            dgvNotificaciones.Name = "dgvNotificaciones";
            dgvNotificaciones.ReadOnly = true;
            dgvNotificaciones.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = Color.LightSteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvNotificaciones.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvNotificaciones.RowHeadersWidth = 51;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.SelectionBackColor = Color.LightSteelBlue;
            dgvNotificaciones.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dgvNotificaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNotificaciones.Size = new Size(1238, 379);
            dgvNotificaciones.TabIndex = 0;
            dgvNotificaciones.CellContentClick += dgvNotificaciones_CellContentClick;
            // 
            // ColumnButtonMarkAsReaded
            // 
            ColumnButtonMarkAsReaded.HeaderText = "Marcar Leido";
            ColumnButtonMarkAsReaded.Image = (Image)resources.GetObject("ColumnButtonMarkAsReaded.Image");
            ColumnButtonMarkAsReaded.ImageLayout = DataGridViewImageCellLayout.Zoom;
            ColumnButtonMarkAsReaded.MinimumWidth = 6;
            ColumnButtonMarkAsReaded.Name = "ColumnButtonMarkAsReaded";
            ColumnButtonMarkAsReaded.ReadOnly = true;
            ColumnButtonMarkAsReaded.Resizable = DataGridViewTriState.True;
            // 
            // ColumnIngresarNotificacion
            // 
            ColumnIngresarNotificacion.HeaderText = "Ingresar al complejo";
            ColumnIngresarNotificacion.Image = (Image)resources.GetObject("ColumnIngresarNotificacion.Image");
            ColumnIngresarNotificacion.ImageLayout = DataGridViewImageCellLayout.Zoom;
            ColumnIngresarNotificacion.MinimumWidth = 6;
            ColumnIngresarNotificacion.Name = "ColumnIngresarNotificacion";
            ColumnIngresarNotificacion.ReadOnly = true;
            // 
            // UC_Notificaciones
            // 
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(dgvNotificaciones);
            Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(64, 64, 64);
            Name = "UC_Notificaciones";
            Size = new Size(1244, 385);
            ((System.ComponentModel.ISupportInitialize)dgvNotificaciones).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvNotificaciones;
        private DataGridViewImageColumn ColumnButtonMarkAsReaded;
        private DataGridViewImageColumn ColumnIngresarNotificacion;
    }
}
