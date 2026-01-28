namespace WinFormsApp1.Forms.Complex
{
    partial class FormComplexWorkspace
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
            panelContentComplexWorkspace = new Panel();
            SuspendLayout();
            // 
            // panelContentComplexWorkspace
            // 
            panelContentComplexWorkspace.AutoScroll = true;
            panelContentComplexWorkspace.Dock = DockStyle.Fill;
            panelContentComplexWorkspace.Location = new Point(0, 0);
            panelContentComplexWorkspace.Name = "panelContentComplexWorkspace";
            panelContentComplexWorkspace.Size = new Size(776, 903);
            panelContentComplexWorkspace.TabIndex = 0;
            // 
            // FormComplexWorkspace
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(776, 903);
            Controls.Add(panelContentComplexWorkspace);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormComplexWorkspace";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "FormComplexWorkspace";
            ResumeLayout(false);
        }

        #endregion

        private Panel panelContentComplexWorkspace;
    }
}