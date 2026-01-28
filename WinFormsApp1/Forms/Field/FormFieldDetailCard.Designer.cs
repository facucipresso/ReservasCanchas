namespace WinFormsApp1.Forms.Field
{
    partial class FormFieldDetailCard
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
            labelNombreCancha = new Label();
            labelTipoCancha = new Label();
            labelPrecioXHora = new Label();
            labelIluminacionBool = new Label();
            labelCubiertaBool = new Label();
            labelTipoSuelo = new Label();
            labelEstadoCancha = new Label();
            buttonVerReservasDeUnaCancha = new Button();
            SuspendLayout();
            // 
            // labelNombreCancha
            // 
            labelNombreCancha.AutoSize = true;
            labelNombreCancha.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            labelNombreCancha.Location = new Point(14, 27);
            labelNombreCancha.Name = "labelNombreCancha";
            labelNombreCancha.Size = new Size(202, 28);
            labelNombreCancha.TabIndex = 0;
            labelNombreCancha.Text = "Nombre de la cancha";
            // 
            // labelTipoCancha
            // 
            labelTipoCancha.AutoSize = true;
            labelTipoCancha.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelTipoCancha.Location = new Point(14, 93);
            labelTipoCancha.Name = "labelTipoCancha";
            labelTipoCancha.Size = new Size(188, 28);
            labelTipoCancha.TabIndex = 0;
            labelTipoCancha.Text = "Futbol: 11 (ejemplo)";
            // 
            // labelPrecioXHora
            // 
            labelPrecioXHora.AutoSize = true;
            labelPrecioXHora.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelPrecioXHora.Location = new Point(12, 129);
            labelPrecioXHora.Name = "labelPrecioXHora";
            labelPrecioXHora.Size = new Size(226, 28);
            labelPrecioXHora.TabIndex = 0;
            labelPrecioXHora.Text = "Precio por hora: $50.000";
            // 
            // labelIluminacionBool
            // 
            labelIluminacionBool.AutoSize = true;
            labelIluminacionBool.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelIluminacionBool.Location = new Point(14, 167);
            labelIluminacionBool.Name = "labelIluminacionBool";
            labelIluminacionBool.Size = new Size(138, 28);
            labelIluminacionBool.TabIndex = 0;
            labelIluminacionBool.Text = "Iluminacion: SI";
            // 
            // labelCubiertaBool
            // 
            labelCubiertaBool.AutoSize = true;
            labelCubiertaBool.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelCubiertaBool.Location = new Point(14, 206);
            labelCubiertaBool.Name = "labelCubiertaBool";
            labelCubiertaBool.Size = new Size(165, 28);
            labelCubiertaBool.TabIndex = 0;
            labelCubiertaBool.Text = "Cacha cubierta: SI";
            // 
            // labelTipoSuelo
            // 
            labelTipoSuelo.AutoSize = true;
            labelTipoSuelo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelTipoSuelo.Location = new Point(253, 93);
            labelTipoSuelo.Name = "labelTipoSuelo";
            labelTipoSuelo.Size = new Size(152, 28);
            labelTipoSuelo.TabIndex = 0;
            labelTipoSuelo.Text = "Suelo: Sintetico ";
            // 
            // labelEstadoCancha
            // 
            labelEstadoCancha.AutoSize = true;
            labelEstadoCancha.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelEstadoCancha.Location = new Point(14, 247);
            labelEstadoCancha.Name = "labelEstadoCancha";
            labelEstadoCancha.Size = new Size(134, 28);
            labelEstadoCancha.TabIndex = 0;
            labelEstadoCancha.Text = "Estado: Activa";
            // 
            // buttonVerReservasDeUnaCancha
            // 
            buttonVerReservasDeUnaCancha.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonVerReservasDeUnaCancha.Location = new Point(256, 306);
            buttonVerReservasDeUnaCancha.Name = "buttonVerReservasDeUnaCancha";
            buttonVerReservasDeUnaCancha.Size = new Size(149, 39);
            buttonVerReservasDeUnaCancha.TabIndex = 1;
            buttonVerReservasDeUnaCancha.Text = "Ver Reservas";
            buttonVerReservasDeUnaCancha.UseVisualStyleBackColor = true;
            // 
            // FormFieldDetailCard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(423, 367);
            Controls.Add(buttonVerReservasDeUnaCancha);
            Controls.Add(labelTipoSuelo);
            Controls.Add(labelEstadoCancha);
            Controls.Add(labelCubiertaBool);
            Controls.Add(labelIluminacionBool);
            Controls.Add(labelPrecioXHora);
            Controls.Add(labelTipoCancha);
            Controls.Add(labelNombreCancha);
            Name = "FormFieldDetailCard";
            Text = "FormField";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelNombreCancha;
        private Label labelTipoCancha;
        private Label labelPrecioXHora;
        private Label labelIluminacionBool;
        private Label labelCubiertaBool;
        private Label labelTipoSuelo;
        private Label labelEstadoCancha;
        private Button buttonVerReservasDeUnaCancha;
    }
}