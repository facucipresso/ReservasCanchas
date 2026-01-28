namespace WinFormsApp1.Forms.Complex
{
    partial class FormComplex
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
            panelComplexTop = new Panel();
            buttonVolver = new Button();
            labelTitle = new Label();
            panelFooter = new Panel();
            labelFooter = new Label();
            flowLayoutPanelComplexes = new FlowLayoutPanel();
            panelComplexTop.SuspendLayout();
            panelFooter.SuspendLayout();
            SuspendLayout();
            // 
            // panelComplexTop
            // 
            panelComplexTop.Controls.Add(labelTitle);
            panelComplexTop.Controls.Add(buttonVolver);
            panelComplexTop.Dock = DockStyle.Top;
            panelComplexTop.Location = new Point(0, 0);
            panelComplexTop.Name = "panelComplexTop";
            panelComplexTop.Size = new Size(800, 60);
            panelComplexTop.TabIndex = 0;
            // 
            // buttonVolver
            // 
            buttonVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonVolver.AutoSize = true;
            buttonVolver.Location = new Point(673, 12);
            buttonVolver.Name = "buttonVolver";
            buttonVolver.Size = new Size(94, 30);
            buttonVolver.TabIndex = 0;
            buttonVolver.Text = "Volver";
            buttonVolver.UseVisualStyleBackColor = true;
            buttonVolver.Click += buttonVolver_Click;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = false;
            labelTitle.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTitle.Location = new Point(12, 10);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(79, 31);
            labelTitle.TabIndex = 1;
            labelTitle.Text = "Listado de complejos";
            // 
            // panelFooter
            // 
            panelFooter.AutoSize = false;
            panelFooter.Controls.Add(labelFooter);
            panelFooter.Dock = DockStyle.Bottom;
            panelFooter.Location = new Point(0, 424);
            panelFooter.Name = "panelFooter";
            panelFooter.Size = new Size(800, 40);
            panelFooter.TabIndex = 1;
            // 
            // labelFooter
            // 
            labelFooter.AutoSize = true;
            labelFooter.Location = new Point(12, 6);
            labelFooter.Name = "labelFooter";
            labelFooter.Size = new Size(266, 20);
            labelFooter.TabIndex = 0;
            labelFooter.Text = "Gestion de Reservas de Canchas - 2025";
            // 
            // flowLayoutPanelComplexes
            // 
            flowLayoutPanelComplexes.AutoScroll = true;
            flowLayoutPanelComplexes.Dock = DockStyle.Fill;
            flowLayoutPanelComplexes.Location = new Point(0, 60);
            flowLayoutPanelComplexes.Name = "flowLayoutPanelComplexes";
            flowLayoutPanelComplexes.Size = new Size(800, 364);
            flowLayoutPanelComplexes.TabIndex = 2;
            // 
            // FormComplex
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(flowLayoutPanelComplexes);
            Controls.Add(panelFooter);
            Controls.Add(panelComplexTop);
            Name = "FormComplex";
            Text = "FormComplex";
            Load += FormComplex_Load;
            panelComplexTop.ResumeLayout(false);
            panelComplexTop.PerformLayout();
            panelFooter.ResumeLayout(false);
            panelFooter.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelComplexTop;
        private Button buttonVolver;
        private Label labelTitle;
        private Panel panelFooter;
        private Label labelFooter;
        private FlowLayoutPanel flowLayoutPanelComplexes;
    }
}