namespace WinFormsApp1.UserControls
{
    partial class UC_Complejos
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
            panelFiltros = new Panel();
            labelEstado = new Label();
            comboEstado = new ComboBox();
            labelProvincia = new Label();
            comboProvincia = new ComboBox();
            flowLayoutPanelComplexes = new FlowLayoutPanel();
            panelFiltros.SuspendLayout();
            SuspendLayout();
            // 
            // panelFiltros
            // 
            panelFiltros.BackColor = Color.WhiteSmoke;
            panelFiltros.Controls.Add(labelEstado);
            panelFiltros.Controls.Add(comboEstado);
            panelFiltros.Controls.Add(labelProvincia);
            panelFiltros.Controls.Add(comboProvincia);
            panelFiltros.Dock = DockStyle.Top;
            panelFiltros.Location = new Point(0, 0);
            panelFiltros.Name = "panelFiltros";
            panelFiltros.Padding = new Padding(20, 15, 20, 10);
            panelFiltros.Size = new Size(1244, 70);
            panelFiltros.TabIndex = 1;
            // 
            // labelEstado
            // 
            labelEstado.AutoSize = true;
            labelEstado.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelEstado.Location = new Point(20, 20);
            labelEstado.Name = "labelEstado";
            labelEstado.Size = new Size(71, 25);
            labelEstado.TabIndex = 0;
            labelEstado.Text = "Estado:";
            // 
            // comboEstado
            // 
            comboEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            comboEstado.Location = new Point(100, 17);
            comboEstado.Name = "comboEstado";
            comboEstado.Size = new Size(200, 28);
            comboEstado.TabIndex = 1;
            // 
            // labelProvincia
            // 
            labelProvincia.AutoSize = true;
            labelProvincia.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelProvincia.Location = new Point(340, 20);
            labelProvincia.Name = "labelProvincia";
            labelProvincia.Size = new Size(92, 25);
            labelProvincia.TabIndex = 2;
            labelProvincia.Text = "Provincia:";
            // 
            // comboProvincia
            // 
            comboProvincia.DropDownStyle = ComboBoxStyle.DropDownList;
            comboProvincia.Location = new Point(440, 17);
            comboProvincia.Name = "comboProvincia";
            comboProvincia.Size = new Size(220, 28);
            comboProvincia.TabIndex = 3;
            // 
            // flowLayoutPanelComplexes
            // 
            flowLayoutPanelComplexes.AutoScroll = true;
            flowLayoutPanelComplexes.Dock = DockStyle.Fill;
            flowLayoutPanelComplexes.Location = new Point(0, 70);
            flowLayoutPanelComplexes.Name = "flowLayoutPanelComplexes";
            flowLayoutPanelComplexes.Size = new Size(1244, 315);
            flowLayoutPanelComplexes.TabIndex = 0;
            // 
            // UC_Complejos
            // 
            Controls.Add(flowLayoutPanelComplexes);
            Controls.Add(panelFiltros);
            Name = "UC_Complejos";
            Size = new Size(1244, 385);
            panelFiltros.ResumeLayout(false);
            panelFiltros.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelFiltros;
        private Label labelEstado;
        private ComboBox comboEstado;
        private Label labelProvincia;
        private ComboBox comboProvincia;
        private FlowLayoutPanel flowLayoutPanelComplexes;
    }
}

