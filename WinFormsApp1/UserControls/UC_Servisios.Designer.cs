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
            panelTrasListServices = new Panel();
            panelBotones = new Panel();
            tableLayoutMain = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dgvServicios).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            panelTrasListServices.SuspendLayout();
            panelBotones.SuspendLayout();
            tableLayoutMain.SuspendLayout();
            SuspendLayout();

            // =======================
            // dgvServicios
            // =======================

            dgvServicios.AllowUserToAddRows = false;
            dgvServicios.AllowUserToDeleteRows = false;
            dgvServicios.AllowUserToResizeColumns = false;
            dgvServicios.AllowUserToResizeRows = false;
            dgvServicios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvServicios.BackgroundColor = SystemColors.ControlLight;
            dgvServicios.BorderStyle = BorderStyle.None;
            dgvServicios.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dgvServicios.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvServicios.EnableHeadersVisualStyles = false;

            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.CornflowerBlue;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.CornflowerBlue;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvServicios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;

            dgvServicios.ColumnHeadersHeight = 35;

            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewCellStyle2.SelectionBackColor = Color.LightSteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvServicios.DefaultCellStyle = dataGridViewCellStyle2;

            dgvServicios.Dock = DockStyle.Fill;
            dgvServicios.MultiSelect = false;
            dgvServicios.ReadOnly = true;
            dgvServicios.RowHeadersVisible = false;
            dgvServicios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvServicios.Location = new Point(15, 55);
            dgvServicios.Size = new Size(864, 669);

            // =======================
            // Panel izquierdo
            // =======================

            labelListaServicios.Dock = DockStyle.Top;
            labelListaServicios.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelListaServicios.Height = 40;
            labelListaServicios.Text = "Listado de Servicios";
            labelListaServicios.TextAlign = ContentAlignment.MiddleCenter;

            buttonEditar.BackColor = Color.FromArgb(19, 15, 64);
            buttonEditar.ForeColor = Color.White;
            buttonEditar.Size = new Size(150, 40);
            buttonEditar.Location = new Point(20, 15);
            buttonEditar.Text = "Editar";
            buttonEditar.Click += buttonEditar_Click;

            buttonEliminar.BackColor = Color.IndianRed;
            buttonEliminar.ForeColor = Color.White;
            buttonEliminar.Size = new Size(150, 40);
            buttonEliminar.Location = new Point(200, 15);
            buttonEliminar.Text = "Eliminar";
            buttonEliminar.Click += buttonEliminar_Click;

            panelBotones.Dock = DockStyle.Bottom;
            panelBotones.Height = 70;
            panelBotones.Controls.Add(buttonEditar);
            panelBotones.Controls.Add(buttonEliminar);

            panelTrasListServices.Dock = DockStyle.Fill;
            panelTrasListServices.Padding = new Padding(15);
            panelTrasListServices.Controls.Add(dgvServicios);
            panelTrasListServices.Controls.Add(panelBotones);
            panelTrasListServices.Controls.Add(labelListaServicios);

            // =======================
            // Panel derecho (FlowLayout)
            // =======================

            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Padding = new Padding(40);

            labelAgregarEditarServicio.AutoSize = false;
            labelAgregarEditarServicio.Height = 40;
            labelAgregarEditarServicio.Text = "Agregar Servicio";
            labelAgregarEditarServicio.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelAgregarEditarServicio.TextAlign = ContentAlignment.MiddleCenter;

            labelServiceDescription.AutoSize = true;
            labelServiceDescription.Text = "Nombre Servicio:";

            textServiceDescription.Height = 34;

            buttonAgregar.Height = 45;
            buttonAgregar.BackColor = Color.FromArgb(19, 15, 64);
            buttonAgregar.ForeColor = Color.White;
            buttonAgregar.Text = "Guardar";
            buttonAgregar.Click += buttonAgregar_Click;

            buttonCancelar.Height = 45;
            buttonCancelar.BackColor = Color.Gray;
            buttonCancelar.ForeColor = Color.White;
            buttonCancelar.Text = "Cancelar";
            buttonCancelar.Visible = false;
            buttonCancelar.Click += buttonCancelar_Click;

            flowLayoutPanel1.Controls.Add(labelAgregarEditarServicio);
            flowLayoutPanel1.Controls.Add(labelServiceDescription);
            flowLayoutPanel1.Controls.Add(textServiceDescription);
            flowLayoutPanel1.Controls.Add(buttonAgregar);
            flowLayoutPanel1.Controls.Add(buttonCancelar);


            // =======================
            // Layout principal
            // =======================

            tableLayoutMain.ColumnCount = 2;
            tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutMain.Dock = DockStyle.Fill;
            tableLayoutMain.Controls.Add(panelTrasListServices, 0, 0);
            tableLayoutMain.Controls.Add(flowLayoutPanel1, 1, 0);

            Controls.Add(tableLayoutMain);
            Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            ForeColor = Color.FromArgb(64, 64, 64);
            Size = new Size(1500, 815);

            ((System.ComponentModel.ISupportInitialize)dgvServicios).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            panelTrasListServices.ResumeLayout(false);
            panelBotones.ResumeLayout(false);
            tableLayoutMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        /*
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
            panelTrasListServices = new Panel();
            panelBotones = new Panel();
            tableLayoutMain = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dgvServicios).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            panelTrasListServices.SuspendLayout();
            panelBotones.SuspendLayout();
            tableLayoutMain.SuspendLayout();
            SuspendLayout();
            // 
            // dgvServicios
            // 
            dgvServicios.AllowUserToAddRows = false;
            dgvServicios.AllowUserToDeleteRows = false;
            dgvServicios.AllowUserToResizeColumns = false;
            dgvServicios.AllowUserToResizeRows = false;
            dgvServicios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvServicios.BackgroundColor = SystemColors.ControlLight;
            dgvServicios.BorderStyle = BorderStyle.None;
            dgvServicios.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dgvServicios.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.CornflowerBlue;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.CornflowerBlue;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
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
            dgvServicios.EnableHeadersVisualStyles = false;
            dgvServicios.Location = new Point(15, 55);
            dgvServicios.MultiSelect = false;
            dgvServicios.Name = "dgvServicios";
            dgvServicios.ReadOnly = true;
            dgvServicios.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvServicios.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvServicios.RowHeadersWidth = 51;
            dataGridViewCellStyle4.SelectionBackColor = Color.LightSteelBlue;
            dgvServicios.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvServicios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvServicios.Size = new Size(864, 669);
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
            textServiceDescription.Size = new Size(400, 34);
            textServiceDescription.TabIndex = 2;
            // 
            // buttonAgregar
            // 
            buttonAgregar.BackColor = Color.FromArgb(19, 15, 64);
            buttonAgregar.ForeColor = Color.White;
            buttonAgregar.Location = new Point(43, 143);
            buttonAgregar.Name = "buttonAgregar";
            buttonAgregar.Size = new Size(400, 45);
            buttonAgregar.TabIndex = 3;
            buttonAgregar.Text = "Guardar";
            buttonAgregar.UseVisualStyleBackColor = false;
            buttonAgregar.Click += buttonAgregar_Click;
            // 
            // labelAgregarEditarServicio
            // 
            labelAgregarEditarServicio.AutoSize = false;
            labelAgregarEditarServicio.Height = 40;
            labelAgregarEditarServicio.Dock = DockStyle.Top;
            labelAgregarEditarServicio.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelAgregarEditarServicio.Location = new Point(43, 40);
            labelAgregarEditarServicio.Name = "labelAgregarEditarServicio";
            labelAgregarEditarServicio.Size = new Size(400, 32);
            labelAgregarEditarServicio.TabIndex = 0;
            labelAgregarEditarServicio.Text = "Agregar Servicio";
            labelAgregarEditarServicio.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelListaServicios
            // 
            labelListaServicios.Dock = DockStyle.Top;
            labelListaServicios.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelListaServicios.Location = new Point(15, 15);
            labelListaServicios.Name = "labelListaServicios";
            labelListaServicios.Size = new Size(864, 40);
            labelListaServicios.TabIndex = 2;
            labelListaServicios.Text = "Listado de Servicios";
            labelListaServicios.TextAlign = ContentAlignment.TopCenter;
            // 
            // buttonEditar
            // 
            buttonEditar.BackColor = Color.FromArgb(19, 15, 64);
            buttonEditar.ForeColor = Color.White;
            buttonEditar.Location = new Point(20, 15);
            buttonEditar.Name = "buttonEditar";
            buttonEditar.Size = new Size(150, 40);
            buttonEditar.TabIndex = 0;
            buttonEditar.Text = "Editar";
            buttonEditar.UseVisualStyleBackColor = false;
            buttonEditar.Click += buttonEditar_Click;
            // 
            // buttonEliminar
            // 
            buttonEliminar.BackColor = Color.IndianRed;
            buttonEliminar.ForeColor = Color.White;
            buttonEliminar.Location = new Point(200, 15);
            buttonEliminar.Name = "buttonEliminar";
            buttonEliminar.Size = new Size(150, 40);
            buttonEliminar.TabIndex = 1;
            buttonEliminar.Text = "Eliminar";
            buttonEliminar.UseVisualStyleBackColor = false;
            buttonEliminar.Click += buttonEliminar_Click;
            // 
            // buttonCancelar
            // 
            buttonCancelar.BackColor = Color.Gray;
            buttonCancelar.ForeColor = Color.White;
            buttonCancelar.Location = new Point(43, 194);
            buttonCancelar.Name = "buttonCancelar";
            buttonCancelar.Size = new Size(400, 45);
            buttonCancelar.TabIndex = 4;
            buttonCancelar.Text = "Cancelar";
            buttonCancelar.UseVisualStyleBackColor = false;
            buttonCancelar.Visible = false;
            buttonCancelar.Click += buttonCancelar_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = SystemColors.Control;
            flowLayoutPanel1.Controls.Add(labelAgregarEditarServicio);
            flowLayoutPanel1.Controls.Add(labelServiceDescription);
            flowLayoutPanel1.Controls.Add(textServiceDescription);
            flowLayoutPanel1.Controls.Add(buttonAgregar);
            flowLayoutPanel1.Controls.Add(buttonCancelar);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(903, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(40);
            flowLayoutPanel1.Size = new Size(594, 809);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // panelTrasListServices
            // 
            panelTrasListServices.BackColor = SystemColors.Control;
            panelTrasListServices.Controls.Add(dgvServicios);
            panelTrasListServices.Controls.Add(panelBotones);
            panelTrasListServices.Controls.Add(labelListaServicios);
            panelTrasListServices.Dock = DockStyle.Fill;
            panelTrasListServices.Location = new Point(3, 3);
            panelTrasListServices.Name = "panelTrasListServices";
            panelTrasListServices.Padding = new Padding(15);
            panelTrasListServices.Size = new Size(894, 809);
            panelTrasListServices.TabIndex = 0;
            // 
            // panelBotones
            // 
            panelBotones.Controls.Add(buttonEditar);
            panelBotones.Controls.Add(buttonEliminar);
            panelBotones.Dock = DockStyle.Bottom;
            panelBotones.Location = new Point(15, 724);
            panelBotones.Name = "panelBotones";
            panelBotones.Size = new Size(864, 70);
            panelBotones.TabIndex = 1;
            // 
            // tableLayoutMain
            // 
            tableLayoutMain.ColumnCount = 2;
            tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutMain.Controls.Add(panelTrasListServices, 0, 0);
            tableLayoutMain.Controls.Add(flowLayoutPanel1, 1, 0);
            tableLayoutMain.Dock = DockStyle.Fill;
            tableLayoutMain.Location = new Point(0, 0);
            tableLayoutMain.Name = "tableLayoutMain";
            tableLayoutMain.RowCount = 1;
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutMain.Size = new Size(1500, 815);
            tableLayoutMain.TabIndex = 0;
            // 
            // UC_Servisios
            // 
            Controls.Add(tableLayoutMain);
            Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            ForeColor = Color.FromArgb(64, 64, 64);
            Name = "UC_Servisios";
            Size = new Size(1500, 815);
            ((System.ComponentModel.ISupportInitialize)dgvServicios).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            panelTrasListServices.ResumeLayout(false);
            panelBotones.ResumeLayout(false);
            tableLayoutMain.ResumeLayout(false);
            ResumeLayout(false);
        }*/

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
        private TableLayoutPanel tableLayoutMain;
    }
}
