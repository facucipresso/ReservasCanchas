namespace WinFormsApp1.UserControls
{
    partial class UC_Usuarios
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
            dgvUsuarios = new DataGridView();
            ColumnAcciones = new DataGridViewImageColumn();
            panelFiltrosUsuarios = new Panel();
            comboBoxInicialApellido = new ComboBox();
            comboBoxRoles = new ComboBox();
            labelInicialApellido = new Label();
            labelRol = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            panelFiltrosUsuarios.SuspendLayout();
            SuspendLayout();
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToResizeColumns = false;
            dgvUsuarios.AllowUserToResizeRows = false;
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.BackgroundColor = SystemColors.Control;
            dgvUsuarios.BorderStyle = BorderStyle.None;
            dgvUsuarios.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dgvUsuarios.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.CornflowerBlue;
            dataGridViewCellStyle1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = Color.CornflowerBlue;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvUsuarios.ColumnHeadersHeight = 40;
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvUsuarios.Columns.AddRange(new DataGridViewColumn[] { ColumnAcciones });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvUsuarios.DefaultCellStyle = dataGridViewCellStyle2;
            dgvUsuarios.Dock = DockStyle.Fill;
            dgvUsuarios.EnableHeadersVisualStyles = false;
            dgvUsuarios.Location = new Point(0, 60);
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvUsuarios.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvUsuarios.RowHeadersWidth = 51;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.SelectionBackColor = Color.LightSteelBlue;
            dgvUsuarios.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvUsuarios.RowTemplate.Height = 33;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.Size = new Size(1244, 325);
            dgvUsuarios.TabIndex = 0;
            dgvUsuarios.CellContentClick += dgvUsuarios_CellContentClick;
            // 
            // ColumnAcciones
            // 
            ColumnAcciones.HeaderText = "Acciones";
            ColumnAcciones.ImageLayout = DataGridViewImageCellLayout.Zoom;
            ColumnAcciones.MinimumWidth = 6;
            ColumnAcciones.Name = "ColumnAcciones";
            ColumnAcciones.ReadOnly = true;
            ColumnAcciones.Resizable = DataGridViewTriState.True;
            ColumnAcciones.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // panelFiltrosUsuarios
            // 
            panelFiltrosUsuarios.Controls.Add(comboBoxInicialApellido);
            panelFiltrosUsuarios.Controls.Add(comboBoxRoles);
            panelFiltrosUsuarios.Controls.Add(labelInicialApellido);
            panelFiltrosUsuarios.Controls.Add(labelRol);
            panelFiltrosUsuarios.Dock = DockStyle.Top;
            panelFiltrosUsuarios.Location = new Point(0, 0);
            panelFiltrosUsuarios.Name = "panelFiltrosUsuarios";
            panelFiltrosUsuarios.Size = new Size(1244, 60);
            panelFiltrosUsuarios.TabIndex = 1;
            // 
            // comboBoxInicialApellido
            // 
            comboBoxInicialApellido.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxInicialApellido.FormattingEnabled = true;
            comboBoxInicialApellido.Location = new Point(472, 17);
            comboBoxInicialApellido.Name = "comboBoxInicialApellido";
            comboBoxInicialApellido.Size = new Size(151, 33);
            comboBoxInicialApellido.TabIndex = 1;
            // 
            // comboBoxRoles
            // 
            comboBoxRoles.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxRoles.FormattingEnabled = true;
            comboBoxRoles.Location = new Point(89, 17);
            comboBoxRoles.Name = "comboBoxRoles";
            comboBoxRoles.Size = new Size(151, 33);
            comboBoxRoles.TabIndex = 1;
            // 
            // labelInicialApellido
            // 
            labelInicialApellido.AutoSize = true;
            labelInicialApellido.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelInicialApellido.Location = new Point(294, 20);
            labelInicialApellido.Name = "labelInicialApellido";
            labelInicialApellido.Size = new Size(172, 25);
            labelInicialApellido.TabIndex = 0;
            labelInicialApellido.Text = "Inicial del apellido: ";
            // 
            // labelRol
            // 
            labelRol.AutoSize = true;
            labelRol.Location = new Point(35, 20);
            labelRol.Name = "labelRol";
            labelRol.Size = new Size(48, 25);
            labelRol.TabIndex = 0;
            labelRol.Text = "Rol: ";
            // 
            // UC_Usuarios
            // 
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(dgvUsuarios);
            Controls.Add(panelFiltrosUsuarios);
            Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(64, 64, 64);
            Name = "UC_Usuarios";
            Size = new Size(1244, 385);
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            panelFiltrosUsuarios.ResumeLayout(false);
            panelFiltrosUsuarios.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvUsuarios;
        private DataGridViewImageColumn ColumnAcciones;
        private Panel panelFiltrosUsuarios;
        private Label labelRol;
        private ComboBox comboBoxInicialApellido;
        private ComboBox comboBoxRoles;
        private Label labelInicialApellido;
    }
}
