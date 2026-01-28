namespace WinFormsApp1.UserControls
{
    partial class UC_FieldList
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
            panel1 = new Panel();
            btnVolver = new Button();
            btnCerrar = new Button();
            labelCanchasDelComplejo = new Label();
            labelFieldsComplexEmpty = new Label();
            flowLayoutPanelFields = new FlowLayoutPanel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnVolver);
            panel1.Controls.Add(btnCerrar);
            panel1.Controls.Add(labelCanchasDelComplejo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(774, 60);
            panel1.TabIndex = 0;
            // 
            // btnVolver
            // 
            btnVolver.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVolver.Location = new Point(536, 20);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(94, 29);
            btnVolver.TabIndex = 1;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            // 
            // btnCerrar
            // 
            btnCerrar.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCerrar.Location = new Point(651, 20);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(94, 29);
            btnCerrar.TabIndex = 1;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            // 
            // labelCanchasDelComplejo
            // 
            labelCanchasDelComplejo.AutoSize = true;
            labelCanchasDelComplejo.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            labelCanchasDelComplejo.Location = new Point(24, 16);
            labelCanchasDelComplejo.Name = "labelCanchasDelComplejo";
            labelCanchasDelComplejo.Size = new Size(248, 31);
            labelCanchasDelComplejo.TabIndex = 0;
            labelCanchasDelComplejo.Text = "Canchas del complejo";
            // 
            // labelFieldsComplexEmpty
            // 
            labelFieldsComplexEmpty.Dock = DockStyle.Fill;
            labelFieldsComplexEmpty.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Italic);
            labelFieldsComplexEmpty.ForeColor = Color.Gray;
            labelFieldsComplexEmpty.Location = new Point(0, 60);
            labelFieldsComplexEmpty.Name = "labelFieldsComplexEmpty";
            labelFieldsComplexEmpty.Size = new Size(774, 841);
            labelFieldsComplexEmpty.TabIndex = 10;
            labelFieldsComplexEmpty.Text = "Este complejo aún no tiene canchas.";
            labelFieldsComplexEmpty.TextAlign = ContentAlignment.MiddleCenter;
            labelFieldsComplexEmpty.Visible = false;
            // 
            // flowLayoutPanelFields
            // 
            flowLayoutPanelFields.Dock = DockStyle.Fill;
            flowLayoutPanelFields.Location = new Point(0, 60);
            flowLayoutPanelFields.Name = "flowLayoutPanelFields";
            flowLayoutPanelFields.Size = new Size(774, 841);
            flowLayoutPanelFields.TabIndex = 1;
            // 
            // UC_FieldList
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(labelFieldsComplexEmpty);
            Controls.Add(flowLayoutPanelFields);
            Controls.Add(panel1);
            Name = "UC_FieldList";
            Size = new Size(774, 901);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label labelFieldsComplexEmpty; 
        private FlowLayoutPanel flowLayoutPanelFields;
        private Label labelCanchasDelComplejo;
        private Button btnCerrar;
        private Button btnVolver;
    }
}
