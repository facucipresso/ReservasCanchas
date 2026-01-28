namespace WinFormsApp1.Forms.Service
{
    partial class FormCreateService
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
            buttonCrearServicio = new Button();
            textBoxCreateService = new TextBox();
            SuspendLayout();
            // 
            // buttonCrearServicio
            // 
            buttonCrearServicio.Location = new Point(345, 209);
            buttonCrearServicio.Name = "buttonCrearServicio";
            buttonCrearServicio.Size = new Size(137, 29);
            buttonCrearServicio.TabIndex = 1;
            buttonCrearServicio.Text = "Crear Servicio";
            buttonCrearServicio.UseVisualStyleBackColor = true;
            buttonCrearServicio.Click += buttonCrearServicio_Click;
            // 
            // textBoxCreateService
            // 
            textBoxCreateService.Location = new Point(316, 137);
            textBoxCreateService.Name = "textBoxCreateService";
            textBoxCreateService.Size = new Size(198, 27);
            textBoxCreateService.TabIndex = 2;
            // 
            // FormCreateService
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBoxCreateService);
            Controls.Add(buttonCrearServicio);
            Name = "FormCreateService";
            Text = "FormCreateService";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button buttonCrearServicio;
        private TextBox textBoxCreateService;
    }
}