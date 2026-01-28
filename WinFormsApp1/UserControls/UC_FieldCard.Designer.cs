namespace WinFormsApp1.UserControls
{
    partial class UC_FieldCard
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
            labelNombreCancha = new Label();
            labelTipoCancha = new Label();
            labelPrecioXHora = new Label();
            labelTipoSuelo = new Label();
            labelIluminacionBool = new Label();
            labelCanchaCubiertaBool = new Label();
            labelEstadoCancha = new Label();
            buttonVerReservasDeUnaCancha = new Button();
            labelLine = new Label();
            SuspendLayout();
            // 
            // labelNombreCancha
            // 
            labelNombreCancha.AutoSize = true;
            labelNombreCancha.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            labelNombreCancha.Location = new Point(14, 27);
            labelNombreCancha.Name = "labelNombreCancha";
            labelNombreCancha.Size = new Size(234, 31);
            labelNombreCancha.TabIndex = 0;
            labelNombreCancha.Text = "Nombre de la cancha";
            // 
            // labelTipoCancha
            // 
            labelTipoCancha.AutoSize = true;
            labelTipoCancha.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelTipoCancha.Location = new Point(14, 95);
            labelTipoCancha.Name = "labelTipoCancha";
            labelTipoCancha.Size = new Size(100, 28);
            labelTipoCancha.TabIndex = 0;
            labelTipoCancha.Text = "Futbol: 11";
            // 
            // labelPrecioXHora
            // 
            labelPrecioXHora.AutoSize = true;
            labelPrecioXHora.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelPrecioXHora.Location = new Point(14, 140);
            labelPrecioXHora.Name = "labelPrecioXHora";
            labelPrecioXHora.Size = new Size(226, 28);
            labelPrecioXHora.TabIndex = 0;
            labelPrecioXHora.Text = "Precio por hora: $50.000";
            // 
            // labelTipoSuelo
            // 
            labelTipoSuelo.AutoSize = true;
            labelTipoSuelo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelTipoSuelo.Location = new Point(186, 95);
            labelTipoSuelo.Name = "labelTipoSuelo";
            labelTipoSuelo.Size = new Size(147, 28);
            labelTipoSuelo.TabIndex = 0;
            labelTipoSuelo.Text = "Suelo: Sintetico";
            // 
            // labelIluminacionBool
            // 
            labelIluminacionBool.AutoSize = true;
            labelIluminacionBool.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelIluminacionBool.Location = new Point(14, 185);
            labelIluminacionBool.Name = "labelIluminacionBool";
            labelIluminacionBool.Size = new Size(138, 28);
            labelIluminacionBool.TabIndex = 0;
            labelIluminacionBool.Text = "Iluminacion: SI";
            // 
            // labelCanchaCubiertaBool
            // 
            labelCanchaCubiertaBool.AutoSize = true;
            labelCanchaCubiertaBool.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelCanchaCubiertaBool.Location = new Point(14, 230);
            labelCanchaCubiertaBool.Name = "labelCanchaCubiertaBool";
            labelCanchaCubiertaBool.Size = new Size(176, 28);
            labelCanchaCubiertaBool.TabIndex = 0;
            labelCanchaCubiertaBool.Text = "Cancha cubierta: SI";
            // 
            // labelEstadoCancha
            // 
            labelEstadoCancha.AutoSize = true;
            labelEstadoCancha.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelEstadoCancha.Location = new Point(14, 275);
            labelEstadoCancha.Name = "labelEstadoCancha";
            labelEstadoCancha.Size = new Size(134, 28);
            labelEstadoCancha.TabIndex = 0;
            labelEstadoCancha.Text = "Estado: Activa";
            // 
            // buttonVerReservasDeUnaCancha
            // 
            buttonVerReservasDeUnaCancha.BackColor = Color.RoyalBlue;
            buttonVerReservasDeUnaCancha.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonVerReservasDeUnaCancha.ForeColor = Color.LightGray;
            buttonVerReservasDeUnaCancha.Location = new Point(65, 352);
            buttonVerReservasDeUnaCancha.Name = "buttonVerReservasDeUnaCancha";
            buttonVerReservasDeUnaCancha.Size = new Size(229, 46);
            buttonVerReservasDeUnaCancha.TabIndex = 1;
            buttonVerReservasDeUnaCancha.Text = "Ver reservas";
            buttonVerReservasDeUnaCancha.UseVisualStyleBackColor = false;
            buttonVerReservasDeUnaCancha.Click += buttonVerReservasDeUnaCancha_Click;
            // 
            // labelLine
            // 
            labelLine.AutoSize = true;
            labelLine.Location = new Point(3, 58);
            labelLine.Name = "labelLine";
            labelLine.Size = new Size(345, 20);
            labelLine.TabIndex = 2;
            labelLine.Text = "________________________________________________________";
            // 
            // UC_FieldCard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(labelLine);
            Controls.Add(buttonVerReservasDeUnaCancha);
            Controls.Add(labelTipoSuelo);
            Controls.Add(labelEstadoCancha);
            Controls.Add(labelCanchaCubiertaBool);
            Controls.Add(labelIluminacionBool);
            Controls.Add(labelPrecioXHora);
            Controls.Add(labelTipoCancha);
            Controls.Add(labelNombreCancha);
            Name = "UC_FieldCard";
            Size = new Size(354, 414);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelNombreCancha;
        private Label labelTipoCancha;
        private Label labelPrecioXHora;
        private Label labelTipoSuelo;
        private Label labelIluminacionBool;
        private Label labelCanchaCubiertaBool;
        private Label labelEstadoCancha;
        private Button buttonVerReservasDeUnaCancha;
        private Label labelLine;
    }
}
