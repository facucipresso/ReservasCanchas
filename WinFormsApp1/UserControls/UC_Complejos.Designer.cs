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

            // panelFiltros
            panelFiltros.Dock = DockStyle.Top;
            panelFiltros.Height = 70;
            panelFiltros.Padding = new Padding(20, 15, 20, 10);
            panelFiltros.BackColor = Color.WhiteSmoke;

            // labelEstado
            labelEstado.AutoSize = true;
            labelEstado.Text = "Estado:";
            labelEstado.Location = new Point(20, 20);

            // comboEstado
            comboEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            comboEstado.Location = new Point(100, 17);
            comboEstado.Width = 200;

            // labelProvincia
            labelProvincia.AutoSize = true;
            labelProvincia.Text = "Provincia:";
            labelProvincia.Location = new Point(340, 20);

            // comboProvincia
            comboProvincia.DropDownStyle = ComboBoxStyle.DropDownList;
            comboProvincia.Location = new Point(440, 17);
            comboProvincia.Width = 220;

            // flowLayoutPanelComplexes
            flowLayoutPanelComplexes.AutoScroll = true;
            flowLayoutPanelComplexes.Dock = DockStyle.Fill;
            flowLayoutPanelComplexes.Location = new Point(0, 70);
            flowLayoutPanelComplexes.Name = "flowLayoutPanelComplexes";

            // agregar controles
            panelFiltros.Controls.Add(labelEstado);
            panelFiltros.Controls.Add(comboEstado);
            panelFiltros.Controls.Add(labelProvincia);
            panelFiltros.Controls.Add(comboProvincia);

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

