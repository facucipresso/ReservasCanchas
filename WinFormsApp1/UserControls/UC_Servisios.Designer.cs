namespace WinFormsApp1.UserControls
{
    partial class UC_Servisios
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            dgvServicios = new DataGridView();
            labelServiceDescription = new Label();
            textServiceDescription = new TextBox();
            buttonAgregar = new Button();
            labelAgregarEditarServicio = new Label();
            labelListaServicios = new Label();
            buttonEditar = new Button();
            buttonEliminar = new Button();
            buttonCancelar = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dgvServicios).BeginInit();
            SuspendLayout();
            // 
            // dgvServicios
            // 
            dgvServicios.AllowUserToAddRows = false;
            dgvServicios.AllowUserToResizeRows = false;
            dgvServicios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvServicios.BackgroundColor = SystemColors.Control;
            dgvServicios.BorderStyle = BorderStyle.None;
            dgvServicios.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dgvServicios.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.CornflowerBlue;
            dataGridViewCellStyle1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = Color.CornflowerBlue;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvServicios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvServicios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvServicios.DefaultCellStyle = dataGridViewCellStyle2;
            dgvServicios.EnableHeadersVisualStyles = false;
            dgvServicios.Location = new Point(25, 82);
            dgvServicios.Name = "dgvServicios";
            dgvServicios.ReadOnly = true;
            dgvServicios.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvServicios.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvServicios.RowHeadersWidth = 51;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.SelectionBackColor = Color.LightSteelBlue;
            dgvServicios.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvServicios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvServicios.Size = new Size(621, 327);
            dgvServicios.TabIndex = 0;
            // 
            // labelServiceDescription
            // 
            labelServiceDescription.AutoSize = true;
            labelServiceDescription.BackColor = SystemColors.ControlLight;
            labelServiceDescription.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelServiceDescription.Location = new Point(697, 124);
            labelServiceDescription.Name = "labelServiceDescription";
            labelServiceDescription.Size = new Size(169, 28);
            labelServiceDescription.TabIndex = 1;
            labelServiceDescription.Text = "Nombre Servicio:";
            // 
            // textServiceDescription
            // 
            textServiceDescription.BackColor = SystemColors.ControlLight;
            textServiceDescription.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textServiceDescription.Location = new Point(872, 124);
            textServiceDescription.Name = "textServiceDescription";
            textServiceDescription.Size = new Size(340, 31);
            textServiceDescription.TabIndex = 2;
            // 
            // buttonAgregar
            // 
            buttonAgregar.BackColor = Color.FromArgb(19, 15, 64);
            buttonAgregar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonAgregar.ForeColor = Color.White;
            buttonAgregar.Location = new Point(697, 202);
            buttonAgregar.Name = "buttonAgregar";
            buttonAgregar.Size = new Size(515, 46);
            buttonAgregar.TabIndex = 3;
            buttonAgregar.Text = "Guardar";
            buttonAgregar.UseVisualStyleBackColor = false;
            buttonAgregar.Click += buttonAgregar_Click;
            // 
            // labelAgregarEditarServicio
            // 
            labelAgregarEditarServicio.AutoSize = true;
            labelAgregarEditarServicio.BackColor = SystemColors.ControlLight;
            labelAgregarEditarServicio.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelAgregarEditarServicio.Location = new Point(855, 33);
            labelAgregarEditarServicio.Name = "labelAgregarEditarServicio";
            labelAgregarEditarServicio.Size = new Size(192, 31);
            labelAgregarEditarServicio.TabIndex = 1;
            labelAgregarEditarServicio.Text = "Agregar Servicio";
            // 
            // labelListaServicios
            // 
            labelListaServicios.AutoSize = true;
            labelListaServicios.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelListaServicios.Location = new Point(223, 33);
            labelListaServicios.Name = "labelListaServicios";
            labelListaServicios.Size = new Size(226, 31);
            labelListaServicios.TabIndex = 1;
            labelListaServicios.Text = "Listado de Servicios";
            // 
            // buttonEditar
            // 
            buttonEditar.BackColor = Color.FromArgb(19, 15, 64);
            buttonEditar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonEditar.ForeColor = Color.White;
            buttonEditar.Location = new Point(36, 466);
            buttonEditar.Name = "buttonEditar";
            buttonEditar.Size = new Size(234, 46);
            buttonEditar.TabIndex = 3;
            buttonEditar.Text = "Editar";
            buttonEditar.UseVisualStyleBackColor = false;
            buttonEditar.Click += buttonEditar_Click;
            // 
            // buttonEliminar
            // 
            buttonEliminar.BackColor = Color.FromArgb(19, 15, 64);
            buttonEliminar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonEliminar.ForeColor = Color.White;
            buttonEliminar.Location = new Point(395, 466);
            buttonEliminar.Name = "buttonEliminar";
            buttonEliminar.Size = new Size(234, 46);
            buttonEliminar.TabIndex = 3;
            buttonEliminar.Text = "Eliminar";
            buttonEliminar.UseVisualStyleBackColor = false;
            buttonEliminar.Click += buttonEliminar_Click;
            // 
            // buttonCancelar
            // 
            buttonCancelar.BackColor = Color.Firebrick;
            buttonCancelar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonCancelar.ForeColor = Color.White;
            buttonCancelar.Location = new Point(697, 263);
            buttonCancelar.Name = "buttonCancelar";
            buttonCancelar.Size = new Size(515, 46);
            buttonCancelar.TabIndex = 3;
            buttonCancelar.Text = "Cancelar";
            buttonCancelar.UseVisualStyleBackColor = false;
            buttonCancelar.Visible = false;
            buttonCancelar.Click += buttonCancelar_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = SystemColors.ControlLight;
            flowLayoutPanel1.Dock = DockStyle.Right;
            flowLayoutPanel1.Location = new Point(666, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(578, 594);
            flowLayoutPanel1.TabIndex = 4;
            // 
            // UC_Servisios
            // 
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(buttonEliminar);
            Controls.Add(buttonEditar);
            Controls.Add(buttonCancelar);
            Controls.Add(buttonAgregar);
            Controls.Add(textServiceDescription);
            Controls.Add(labelListaServicios);
            Controls.Add(labelAgregarEditarServicio);
            Controls.Add(labelServiceDescription);
            Controls.Add(dgvServicios);
            Controls.Add(flowLayoutPanel1);
            Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(64, 64, 64);
            Name = "UC_Servisios";
            Size = new Size(1244, 594);
            ((System.ComponentModel.ISupportInitialize)dgvServicios).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvServicios;
        private Label labelServiceDescription;
        private TextBox textServiceDescription;
        private Button buttonAgregar;
        private Label labelAgregarEditarServicio;
        private Label labelListaServicios;
        private Button buttonEditar;
        private Button buttonEliminar;
        private Button buttonCancelar;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}
