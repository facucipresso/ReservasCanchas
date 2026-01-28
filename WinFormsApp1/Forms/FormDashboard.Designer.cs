namespace WinFormsApp1.Forms
{
    partial class FormDashboard
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
            labelDashboard = new Label();
            buttonComplex = new Button();
            labelDashboardErrorComplex = new Label();
            buttonServices = new Button();
            SuspendLayout();
            // 
            // labelDashboard
            // 
            labelDashboard.AutoSize = true;
            labelDashboard.Location = new Point(304, 165);
            labelDashboard.Name = "labelDashboard";
            labelDashboard.Size = new Size(176, 20);
            labelDashboard.TabIndex = 0;
            labelDashboard.Text = "Bienvenido al Dashboard";
            // 
            // buttonComplex
            // 
            buttonComplex.Location = new Point(304, 221);
            buttonComplex.Name = "buttonComplex";
            buttonComplex.Size = new Size(178, 29);
            buttonComplex.TabIndex = 1;
            buttonComplex.Text = "Ver complejos";
            buttonComplex.UseVisualStyleBackColor = true;
            buttonComplex.Click += buttonComplex_Click;
            // 
            // labelDashboardErrorComplex
            // 
            labelDashboardErrorComplex.AutoSize = true;
            labelDashboardErrorComplex.Location = new Point(368, 289);
            labelDashboardErrorComplex.Name = "labelDashboardErrorComplex";
            labelDashboardErrorComplex.Size = new Size(50, 20);
            labelDashboardErrorComplex.TabIndex = 2;
            labelDashboardErrorComplex.Text = "label1";
            labelDashboardErrorComplex.Visible = false;
            // 
            // buttonServices
            // 
            buttonServices.Location = new Point(552, 221);
            buttonServices.Name = "buttonServices";
            buttonServices.Size = new Size(161, 29);
            buttonServices.TabIndex = 3;
            buttonServices.Text = "Ver servicios";
            buttonServices.UseVisualStyleBackColor = true;
            buttonServices.Click += button1_Click;
            // 
            // FormDashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1753, 979);
            Controls.Add(buttonServices);
            Controls.Add(labelDashboardErrorComplex);
            Controls.Add(buttonComplex);
            Controls.Add(labelDashboard);
            Name = "FormDashboard";
            Text = "FormDashboard";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelDashboard;
        private Button buttonComplex;
        private Label labelDashboardErrorComplex;
        private Button buttonServices;
    }
}