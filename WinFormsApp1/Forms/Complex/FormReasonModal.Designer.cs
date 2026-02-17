namespace WinFormsApp1.Forms.Complex
{
    partial class FormReasonModal
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblReasonModal = new Label();
            textBoxReasonModal = new TextBox();
            buttonConfirmar = new Button();
            buttonCancelar = new Button();
            SuspendLayout();

            // 
            // lblReasonModal
            // 
            lblReasonModal.AutoSize = false;
            lblReasonModal.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold | FontStyle.Italic);
            lblReasonModal.Location = new Point(0, 15);
            lblReasonModal.Name = "lblReasonModal";
            lblReasonModal.Size = new Size(492, 35); // mismo ancho que el form
            lblReasonModal.TabIndex = 0;
            lblReasonModal.TextAlign = ContentAlignment.MiddleCenter;
            lblReasonModal.Text = "Ingrese la razón";

            // 
            // textBoxReasonModal
            // 
            textBoxReasonModal.Location = new Point(47, 70);
            textBoxReasonModal.Multiline = true;
            textBoxReasonModal.Name = "textBoxReasonModal";
            textBoxReasonModal.Size = new Size(405, 70);
            textBoxReasonModal.TabIndex = 1;

            // 
            // buttonConfirmar
            // 
            buttonConfirmar.Location = new Point(236, 165);
            buttonConfirmar.Name = "buttonConfirmar";
            buttonConfirmar.Size = new Size(107, 30);
            buttonConfirmar.TabIndex = 2;
            buttonConfirmar.Text = "CONFIRMAR";
            buttonConfirmar.UseVisualStyleBackColor = true;
            buttonConfirmar.Click += buttonConfirmar_Click;

            // 
            // buttonCancelar
            // 
            buttonCancelar.Location = new Point(358, 165);
            buttonCancelar.Name = "buttonCancelar";
            buttonCancelar.Size = new Size(107, 30);
            buttonCancelar.TabIndex = 3;
            buttonCancelar.Text = "CANCELAR";
            buttonCancelar.UseVisualStyleBackColor = true;
            buttonCancelar.Click += buttonCancelar_Click;

            // 
            // FormReasonModal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(492, 215);
            Controls.Add(buttonCancelar);
            Controls.Add(buttonConfirmar);
            Controls.Add(textBoxReasonModal);
            Controls.Add(lblReasonModal);
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
            Name = "FormReasonModal";
            Text = "FormReasonModal";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblReasonModal;
        private TextBox textBoxReasonModal;
        private Button buttonConfirmar;
        private Button buttonCancelar;
    }
}