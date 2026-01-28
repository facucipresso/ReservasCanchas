namespace WinFormsApp1.Forms.Service
{
    partial class FormUpdateService
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
            textBoxUpdateDescriptionService = new TextBox();
            buttonGuardarCambios = new Button();
            SuspendLayout();
            // 
            // textBoxUpdateDescriptionService
            // 
            textBoxUpdateDescriptionService.Location = new Point(245, 128);
            textBoxUpdateDescriptionService.Name = "textBoxUpdateDescriptionService";
            textBoxUpdateDescriptionService.Size = new Size(319, 27);
            textBoxUpdateDescriptionService.TabIndex = 0;
            // 
            // buttonGuardarCambios
            // 
            buttonGuardarCambios.Location = new Point(328, 230);
            buttonGuardarCambios.Name = "buttonGuardarCambios";
            buttonGuardarCambios.Size = new Size(154, 29);
            buttonGuardarCambios.TabIndex = 1;
            buttonGuardarCambios.Text = "Guardar Cambios";
            buttonGuardarCambios.UseVisualStyleBackColor = true;
            buttonGuardarCambios.Click += buttonGuardarCambios_Click;
            // 
            // FormUpdateService
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonGuardarCambios);
            Controls.Add(textBoxUpdateDescriptionService);
            Name = "FormUpdateService";
            Text = "FormUpdateService";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxUpdateDescriptionService;
        private Button buttonGuardarCambios;
    }
}