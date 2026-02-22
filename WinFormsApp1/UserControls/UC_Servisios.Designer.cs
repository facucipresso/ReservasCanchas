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
            panelTrasListServices = new Panel();
            panelBotones = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvServicios).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            panelTrasListServices.SuspendLayout();
            panelBotones.SuspendLayout();
            SuspendLayout();
            // 
            // dgvServicios
            // 
            dgvServicios.AllowUserToAddRows = false;
            dgvServicios.AllowUserToResizeRows = false;
            dgvServicios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvServicios.BackgroundColor = SystemColors.Control;
            dgvServicios.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.CornflowerBlue;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dgvServicios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvServicios.ColumnHeadersHeight = 29;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvServicios.DefaultCellStyle = dataGridViewCellStyle2;
            dgvServicios.Dock = DockStyle.Fill;
            dgvServicios.Location = new Point(15, 55);
            dgvServicios.Name = "dgvServicios";
            dgvServicios.ReadOnly = true;
            dgvServicios.RowHeadersWidth = 51;
            dgvServicios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvServicios.Size = new Size(971, 723);
            dgvServicios.TabIndex = 0;
            // 
            // labelServiceDescription
            // 
            labelServiceDescription.AutoSize = true;
            labelServiceDescription.Location = new Point(43, 72);
            labelServiceDescription.Name = "labelServiceDescription";
            labelServiceDescription.Size = new Size(176, 28);
            labelServiceDescription.TabIndex = 1;
            labelServiceDescription.Text = "Nombre Servicio:";
            // 
            // textServiceDescription
            // 
            textServiceDescription.Location = new Point(43, 103);
            textServiceDescription.Name = "textServiceDescription";
            textServiceDescription.Size = new Size(792, 34);
            textServiceDescription.TabIndex = 2;
            // 
            // buttonAgregar
            // 
            buttonAgregar.BackColor = Color.FromArgb(19, 15, 64);
            buttonAgregar.ForeColor = Color.White;
            buttonAgregar.Location = new Point(43, 143);
            buttonAgregar.Name = "buttonAgregar";
            buttonAgregar.Size = new Size(792, 45);
            buttonAgregar.TabIndex = 3;
            buttonAgregar.Text = "Guardar";
            buttonAgregar.UseVisualStyleBackColor = false;
            buttonAgregar.Click += buttonAgregar_Click;
            // 
            // labelAgregarEditarServicio
            // 
            labelAgregarEditarServicio.AutoSize = true;
            labelAgregarEditarServicio.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelAgregarEditarServicio.Location = new Point(43, 40);
            labelAgregarEditarServicio.Name = "labelAgregarEditarServicio";
            labelAgregarEditarServicio.Size = new Size(205, 32);
            labelAgregarEditarServicio.TabIndex = 0;
            labelAgregarEditarServicio.Text = "Agregar Servicio";
            // 
            // labelListaServicios
            // 
            labelListaServicios.Dock = DockStyle.Top;
            labelListaServicios.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelListaServicios.Location = new Point(15, 15);
            labelListaServicios.Name = "labelListaServicios";
            labelListaServicios.Size = new Size(971, 40);
            labelListaServicios.TabIndex = 2;
            labelListaServicios.Text = "Listado de Servicios";
            // 
            // buttonEditar
            // 
            buttonEditar.BackColor = Color.FromArgb(19, 15, 64);
            buttonEditar.ForeColor = Color.White;
            buttonEditar.Location = new Point(50, 15);
            buttonEditar.Name = "buttonEditar";
            buttonEditar.Size = new Size(150, 40);
            buttonEditar.TabIndex = 0;
            buttonEditar.Text = "Editar";
            buttonEditar.UseVisualStyleBackColor = false;
            buttonEditar.Click += buttonEditar_Click;
            // 
            // buttonEliminar
            // 
            buttonEliminar.BackColor = Color.FromArgb(19, 15, 64);
            buttonEliminar.ForeColor = Color.White;
            buttonEliminar.Location = new Point(250, 15);
            buttonEliminar.Name = "buttonEliminar";
            buttonEliminar.Size = new Size(150, 40);
            buttonEliminar.TabIndex = 1;
            buttonEliminar.Text = "Eliminar";
            buttonEliminar.UseVisualStyleBackColor = false;
            buttonEliminar.Click += buttonEliminar_Click;
            // 
            // buttonCancelar
            // 
            buttonCancelar.BackColor = Color.Firebrick;
            buttonCancelar.ForeColor = Color.White;
            buttonCancelar.Location = new Point(43, 194);
            buttonCancelar.Name = "buttonCancelar";
            buttonCancelar.Size = new Size(792, 45);
            buttonCancelar.TabIndex = 4;
            buttonCancelar.Text = "Cancelar";
            buttonCancelar.UseVisualStyleBackColor = false;
            buttonCancelar.Visible = false;
            buttonCancelar.Click += buttonCancelar_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = SystemColors.ControlLight;
            flowLayoutPanel1.Controls.Add(labelAgregarEditarServicio);
            flowLayoutPanel1.Controls.Add(labelServiceDescription);
            flowLayoutPanel1.Controls.Add(textServiceDescription);
            flowLayoutPanel1.Controls.Add(buttonAgregar);
            flowLayoutPanel1.Controls.Add(buttonCancelar);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(1001, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(40);
            flowLayoutPanel1.Size = new Size(663, 863);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // panelTrasListServices
            // 
            panelTrasListServices.BackColor = SystemColors.Control;
            panelTrasListServices.Controls.Add(dgvServicios);
            panelTrasListServices.Controls.Add(panelBotones);
            panelTrasListServices.Controls.Add(labelListaServicios);
            panelTrasListServices.Dock = DockStyle.Left;
            panelTrasListServices.Location = new Point(0, 0);
            panelTrasListServices.Name = "panelTrasListServices";
            panelTrasListServices.Padding = new Padding(15);
            panelTrasListServices.Size = new Size(1001, 863);
            panelTrasListServices.TabIndex = 1;
            // 
            // panelBotones
            // 
            panelBotones.Controls.Add(buttonEditar);
            panelBotones.Controls.Add(buttonEliminar);
            panelBotones.Dock = DockStyle.Bottom;
            panelBotones.Location = new Point(15, 778);
            panelBotones.Name = "panelBotones";
            panelBotones.Size = new Size(971, 70);
            panelBotones.TabIndex = 1;
            // 
            // UC_Servisios
            // 
            Controls.Add(flowLayoutPanel1);
            Controls.Add(panelTrasListServices);
            Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            ForeColor = Color.FromArgb(64, 64, 64);
            Name = "UC_Servisios";
            Size = new Size(1664, 863);
            ((System.ComponentModel.ISupportInitialize)dgvServicios).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            panelTrasListServices.ResumeLayout(false);
            panelBotones.ResumeLayout(false);
            ResumeLayout(false);
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
        private Panel panelTrasListServices;
        private Panel panelBotones;
    }
}
