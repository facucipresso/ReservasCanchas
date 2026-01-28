namespace WinFormsApp1.UserControls
{
    partial class UC_Complejos
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
            flowLayoutPanelComplexes = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // flowLayoutPanelComplexes
            // 
            flowLayoutPanelComplexes.AutoScroll = true;
            flowLayoutPanelComplexes.Dock = DockStyle.Fill;
            flowLayoutPanelComplexes.Location = new Point(0, 0);
            flowLayoutPanelComplexes.Name = "flowLayoutPanelComplexes";
            flowLayoutPanelComplexes.Size = new Size(1244, 385);
            flowLayoutPanelComplexes.TabIndex = 0;
            // 
            // UC_Complejos
            // 
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(flowLayoutPanelComplexes);
            Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(64, 64, 64);
            Name = "UC_Complejos";
            Size = new Size(1244, 385);
            ResumeLayout(false);
        }

        #endregion
        private FlowLayoutPanel flowLayoutPanelComplexes;
    }
}
