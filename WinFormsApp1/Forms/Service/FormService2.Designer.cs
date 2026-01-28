namespace WinFormsApp1.Forms
{
    partial class FormService2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panel1 = new Panel();
            buttonAgregarServicio = new Button();
            button1 = new Button();
            labelServicios = new Label();
            panel2 = new Panel();
            labelFooter = new Label();
            dataGridViewServicios = new DataGridView();
            bindingSource1 = new BindingSource(components);
            ColServiceDescription = new DataGridViewTextBoxColumn();
            ColEditar = new DataGridViewButtonColumn();
            ColBorrar = new DataGridViewButtonColumn();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewServicios).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonAgregarServicio);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(labelServicios);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 50);
            panel1.TabIndex = 0;
            // 
            // buttonAgregarServicio
            // 
            buttonAgregarServicio.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonAgregarServicio.BackColor = Color.LightGray;
            buttonAgregarServicio.FlatAppearance.BorderSize = 0;
            buttonAgregarServicio.FlatStyle = FlatStyle.Flat;
            buttonAgregarServicio.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonAgregarServicio.ForeColor = Color.Black;
            buttonAgregarServicio.Location = new Point(490, 8);
            buttonAgregarServicio.Name = "buttonAgregarServicio";
            buttonAgregarServicio.Size = new Size(150, 35);
            buttonAgregarServicio.TabIndex = 2;
            buttonAgregarServicio.Text = "Agregar Servicio";
            buttonAgregarServicio.UseVisualStyleBackColor = false;
            buttonAgregarServicio.Click += buttonAgregarServicio_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.BackColor = Color.LightGray;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.Black;
            button1.Location = new Point(673, 8);
            button1.Name = "button1";
            button1.Size = new Size(100, 35);
            button1.TabIndex = 1;
            button1.Text = "Volver";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // labelServicios
            // 
            labelServicios.AutoSize = true;
            labelServicios.Location = new Point(327, 16);
            labelServicios.Name = "labelServicios";
            labelServicios.Size = new Size(67, 20);
            labelServicios.TabIndex = 0;
            labelServicios.Text = "Servicios";
            // 
            // panel2
            // 
            panel2.Controls.Add(labelFooter);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 415);
            panel2.Name = "panel2";
            panel2.Size = new Size(800, 35);
            panel2.TabIndex = 1;
            // 
            // labelFooter
            // 
            labelFooter.AutoSize = true;
            labelFooter.Location = new Point(213, 7);
            labelFooter.Name = "labelFooter";
            labelFooter.Size = new Size(266, 20);
            labelFooter.TabIndex = 0;
            labelFooter.Text = "Gestion de Reservas de Canchas - 2025";
            // 
            // dataGridViewServicios
            // 
            dataGridViewServicios.AutoGenerateColumns = false;
            dataGridViewServicios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewServicios.Columns.AddRange(new DataGridViewColumn[] { ColServiceDescription, ColEditar, ColBorrar });
            dataGridViewServicios.DataSource = bindingSource1;
            dataGridViewServicios.Dock = DockStyle.Fill;
            dataGridViewServicios.Location = new Point(0, 50);
            dataGridViewServicios.Name = "dataGridViewServicios";
            dataGridViewServicios.RowHeadersWidth = 51;
            dataGridViewServicios.Size = new Size(800, 365);
            dataGridViewServicios.TabIndex = 2;
            dataGridViewServicios.CellContentClick += dataGridViewServicios_CellContentClick;
            // 
            // bindingSource1
            // 
            bindingSource1.DataSource = typeof(Models.Service.ServiceResponseDTO);
            bindingSource1.CurrentChanged += bindingSource1_CurrentChanged;
            // 
            // ColServiceDescription
            // 
            ColServiceDescription.DataPropertyName = "ServiceDescription";
            ColServiceDescription.HeaderText = "Servicio";
            ColServiceDescription.MinimumWidth = 6;
            ColServiceDescription.Name = "ColServiceDescription";
            ColServiceDescription.Width = 125;
            // 
            // ColEditar
            // 
            ColEditar.HeaderText = "Editar";
            ColEditar.MinimumWidth = 6;
            ColEditar.Name = "ColEditar";
            ColEditar.Width = 125;
            // 
            // ColBorrar
            // 
            ColBorrar.HeaderText = "Borrar";
            ColBorrar.MinimumWidth = 6;
            ColBorrar.Name = "ColBorrar";
            ColBorrar.Width = 125;
            // 
            // FormService2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridViewServicios);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "FormService2";
            Text = "FormService2";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewServicios).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button button1;
        private Label labelServicios;
        private Button buttonAgregarServicio;
        private Panel panel2;
        private Label labelFooter;
        private DataGridView dataGridViewServicios;
        private BindingSource bindingSource1;
        private DataGridViewTextBoxColumn ColServiceDescription;
        private DataGridViewButtonColumn ColEditar;
        private DataGridViewButtonColumn ColBorrar;
    }
}