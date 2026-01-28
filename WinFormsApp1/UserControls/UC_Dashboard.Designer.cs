namespace WinFormsApp1.UserControls
{
    partial class UC_Dashboard
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
            tlpMain = new TableLayoutPanel();
            flpCards = new FlowLayoutPanel();
            tlpBottom = new TableLayoutPanel();
            panelComplejos = new Panel();
            flpUltimosComplejos = new FlowLayoutPanel();
            labelComplejos = new Label();
            panelUsuarios = new Panel();
            flpUltimosUsuarios = new FlowLayoutPanel();
            labelUsuarios = new Label();
            panelReseñas = new Panel();
            flpUltimasReseñas = new FlowLayoutPanel();
            labelReseñas = new Label();
            tlpMain.SuspendLayout();
            tlpBottom.SuspendLayout();
            panelComplejos.SuspendLayout();
            panelUsuarios.SuspendLayout();
            panelReseñas.SuspendLayout();
            SuspendLayout();
            // 
            // tlpMain
            // 
            tlpMain.ColumnCount = 1;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMain.Controls.Add(flpCards, 0, 0);
            tlpMain.Controls.Add(tlpBottom, 0, 1);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(0, 0);
            tlpMain.Name = "tlpMain";
            tlpMain.RowCount = 2;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.Size = new Size(1244, 385);
            tlpMain.TabIndex = 0;
            // 
            // flpCards
            // 
            flpCards.Dock = DockStyle.Fill;
            flpCards.Location = new Point(3, 3);
            flpCards.Name = "flpCards";
            flpCards.Padding = new Padding(10);
            flpCards.Size = new Size(1238, 114);
            flpCards.TabIndex = 0;
            flpCards.WrapContents = false;
            // 
            // tlpBottom
            // 
            tlpBottom.ColumnCount = 3;
            tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            tlpBottom.Controls.Add(panelComplejos, 0, 0);
            tlpBottom.Controls.Add(panelUsuarios, 1, 0);
            tlpBottom.Controls.Add(panelReseñas, 2, 0);
            tlpBottom.Dock = DockStyle.Fill;
            tlpBottom.Location = new Point(3, 123);
            tlpBottom.Name = "tlpBottom";
            tlpBottom.RowCount = 1;
            tlpBottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBottom.Size = new Size(1238, 259);
            tlpBottom.TabIndex = 1;
            // 
            // panelComplejos
            // 
            panelComplejos.Controls.Add(flpUltimosComplejos);
            panelComplejos.Controls.Add(labelComplejos);
            panelComplejos.Dock = DockStyle.Fill;
            panelComplejos.Location = new Point(3, 3);
            panelComplejos.Name = "panelComplejos";
            panelComplejos.Padding = new Padding(10);
            panelComplejos.Size = new Size(406, 253);
            panelComplejos.TabIndex = 0;
            // 
            // flpUltimosComplejos
            // 
            flpUltimosComplejos.AutoScroll = true;
            flpUltimosComplejos.Dock = DockStyle.Fill;
            flpUltimosComplejos.FlowDirection = FlowDirection.TopDown;
            flpUltimosComplejos.Location = new Point(10, 48);
            flpUltimosComplejos.Name = "flpUltimosComplejos";
            flpUltimosComplejos.Padding = new Padding(5);
            flpUltimosComplejos.Size = new Size(386, 195);
            flpUltimosComplejos.TabIndex = 1;
            flpUltimosComplejos.WrapContents = false;
            // 
            // labelComplejos
            // 
            labelComplejos.AutoSize = true;
            labelComplejos.Dock = DockStyle.Top;
            labelComplejos.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelComplejos.Location = new Point(10, 10);
            labelComplejos.Name = "labelComplejos";
            labelComplejos.Padding = new Padding(5, 5, 0, 8);
            labelComplejos.Size = new Size(173, 38);
            labelComplejos.TabIndex = 0;
            labelComplejos.Text = "Ultimos complejos";
            // 
            // panelUsuarios
            // 
            panelUsuarios.Controls.Add(flpUltimosUsuarios);
            panelUsuarios.Controls.Add(labelUsuarios);
            panelUsuarios.Dock = DockStyle.Fill;
            panelUsuarios.Location = new Point(415, 3);
            panelUsuarios.Name = "panelUsuarios";
            panelUsuarios.Padding = new Padding(10);
            panelUsuarios.Size = new Size(406, 253);
            panelUsuarios.TabIndex = 1;
            // 
            // flpUltimosUsuarios
            // 
            flpUltimosUsuarios.AutoScroll = true;
            flpUltimosUsuarios.Dock = DockStyle.Fill;
            flpUltimosUsuarios.FlowDirection = FlowDirection.TopDown;
            flpUltimosUsuarios.Location = new Point(10, 48);
            flpUltimosUsuarios.Name = "flpUltimosUsuarios";
            flpUltimosUsuarios.Padding = new Padding(5);
            flpUltimosUsuarios.Size = new Size(386, 195);
            flpUltimosUsuarios.TabIndex = 1;
            flpUltimosUsuarios.WrapContents = false;
            // 
            // labelUsuarios
            // 
            labelUsuarios.AutoSize = true;
            labelUsuarios.Dock = DockStyle.Top;
            labelUsuarios.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelUsuarios.Location = new Point(10, 10);
            labelUsuarios.Name = "labelUsuarios";
            labelUsuarios.Padding = new Padding(5, 5, 0, 8);
            labelUsuarios.Size = new Size(158, 38);
            labelUsuarios.TabIndex = 0;
            labelUsuarios.Text = "Ultimos usuarios";
            // 
            // panelReseñas
            // 
            panelReseñas.Controls.Add(flpUltimasReseñas);
            panelReseñas.Controls.Add(labelReseñas);
            panelReseñas.Dock = DockStyle.Fill;
            panelReseñas.Location = new Point(827, 3);
            panelReseñas.Name = "panelReseñas";
            panelReseñas.Padding = new Padding(10);
            panelReseñas.Size = new Size(408, 253);
            panelReseñas.TabIndex = 2;
            // 
            // flpUltimasReseñas
            // 
            flpUltimasReseñas.AutoScroll = true;
            flpUltimasReseñas.Dock = DockStyle.Fill;
            flpUltimasReseñas.FlowDirection = FlowDirection.TopDown;
            flpUltimasReseñas.Location = new Point(10, 48);
            flpUltimasReseñas.Name = "flpUltimasReseñas";
            flpUltimasReseñas.Padding = new Padding(5);
            flpUltimasReseñas.Size = new Size(388, 195);
            flpUltimasReseñas.TabIndex = 1;
            flpUltimasReseñas.WrapContents = false;
            // 
            // labelReseñas
            // 
            labelReseñas.AutoSize = true;
            labelReseñas.Dock = DockStyle.Top;
            labelReseñas.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelReseñas.Location = new Point(10, 10);
            labelReseñas.Name = "labelReseñas";
            labelReseñas.Padding = new Padding(5, 5, 0, 8);
            labelReseñas.Size = new Size(150, 38);
            labelReseñas.TabIndex = 0;
            labelReseñas.Text = "Ultimas reseñas";
            // 
            // UC_Dashboard
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlpMain);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(64, 64, 64);
            Margin = new Padding(4, 5, 4, 5);
            Name = "UC_Dashboard";
            Size = new Size(1244, 385);
            tlpMain.ResumeLayout(false);
            tlpBottom.ResumeLayout(false);
            panelComplejos.ResumeLayout(false);
            panelComplejos.PerformLayout();
            panelUsuarios.ResumeLayout(false);
            panelUsuarios.PerformLayout();
            panelReseñas.ResumeLayout(false);
            panelReseñas.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpMain;
        private FlowLayoutPanel flpCards;
        private TableLayoutPanel tlpBottom;
        private Panel panelComplejos;
        private Label labelComplejos;
        private Panel panelUsuarios;
        private Label labelUsuarios;
        private Panel panelReseñas;
        private Label labelReseñas;
        private FlowLayoutPanel flpUltimosComplejos;
        private FlowLayoutPanel flpUltimosUsuarios;
        private FlowLayoutPanel flpUltimasReseñas;
    }
}
