namespace WinFormsApp1.Forms.Complex
{
    partial class FormComplexDetail
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
            pictureBox1 = new PictureBox();
            labelNombreComplejoDetail = new Label();
            labelDescripcionComplejoDetail = new Label();
            labelUbicacionComplejoDetail = new Label();
            labelContactoComplejoDetail = new Label();
            labelPorcentajeSenia = new Label();
            labelHoraComiezoIlum = new Label();
            labelAdPorIlum = new Label();
            labelCBU = new Label();
            labelServicios = new Label();
            dgvServiciosComplejoDetail = new DataGridView();
            buttonVerCanchas = new Button();
            buttonVerReservas = new Button();
            labelEstadoComplejoDetail = new Label();
            buttonCambioEstado1 = new Button();
            buttonCambioEstado2 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvServiciosComplejoDetail).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(752, 251);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // labelNombreComplejoDetail
            // 
            labelNombreComplejoDetail.AutoSize = true;
            labelNombreComplejoDetail.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            labelNombreComplejoDetail.Location = new Point(12, 266);
            labelNombreComplejoDetail.Name = "labelNombreComplejoDetail";
            labelNombreComplejoDetail.Size = new Size(290, 38);
            labelNombreComplejoDetail.TabIndex = 1;
            labelNombreComplejoDetail.Text = "Nombre del complejo";
            // 
            // labelDescripcionComplejoDetail
            // 
            labelDescripcionComplejoDetail.AutoSize = true;
            labelDescripcionComplejoDetail.Font = new Font("Segoe UI Semilight", 13.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            labelDescripcionComplejoDetail.Location = new Point(12, 311);
            labelDescripcionComplejoDetail.Name = "labelDescripcionComplejoDetail";
            labelDescripcionComplejoDetail.Size = new Size(332, 31);
            labelDescripcionComplejoDetail.TabIndex = 2;
            labelDescripcionComplejoDetail.Text = "Descripcion del complejo loquitos";
            // 
            // labelUbicacionComplejoDetail
            // 
            labelUbicacionComplejoDetail.AutoSize = true;
            labelUbicacionComplejoDetail.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelUbicacionComplejoDetail.Location = new Point(12, 369);
            labelUbicacionComplejoDetail.Name = "labelUbicacionComplejoDetail";
            labelUbicacionComplejoDetail.Size = new Size(294, 28);
            labelUbicacionComplejoDetail.TabIndex = 3;
            labelUbicacionComplejoDetail.Text = "Provincia - Localidad - Direccion";
            // 
            // labelContactoComplejoDetail
            // 
            labelContactoComplejoDetail.AutoSize = true;
            labelContactoComplejoDetail.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelContactoComplejoDetail.Location = new Point(11, 408);
            labelContactoComplejoDetail.Name = "labelContactoComplejoDetail";
            labelContactoComplejoDetail.Size = new Size(287, 28);
            labelContactoComplejoDetail.TabIndex = 4;
            labelContactoComplejoDetail.Text = "Contacto / Numero de telefono";
            // 
            // labelPorcentajeSenia
            // 
            labelPorcentajeSenia.AutoSize = true;
            labelPorcentajeSenia.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelPorcentajeSenia.Location = new Point(12, 449);
            labelPorcentajeSenia.Name = "labelPorcentajeSenia";
            labelPorcentajeSenia.Size = new Size(309, 28);
            labelPorcentajeSenia.TabIndex = 5;
            labelPorcentajeSenia.Text = "Porcentaje de seña: 10% (ejemplo)";
            // 
            // labelHoraComiezoIlum
            // 
            labelHoraComiezoIlum.AutoSize = true;
            labelHoraComiezoIlum.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelHoraComiezoIlum.Location = new Point(12, 489);
            labelHoraComiezoIlum.Name = "labelHoraComiezoIlum";
            labelHoraComiezoIlum.Size = new Size(317, 28);
            labelHoraComiezoIlum.TabIndex = 7;
            labelHoraComiezoIlum.Text = "Hora de comienzo iluminacion: 8hs";
            // 
            // labelAdPorIlum
            // 
            labelAdPorIlum.AutoSize = true;
            labelAdPorIlum.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelAdPorIlum.Location = new Point(419, 490);
            labelAdPorIlum.Name = "labelAdPorIlum";
            labelAdPorIlum.Size = new Size(272, 28);
            labelAdPorIlum.TabIndex = 7;
            labelAdPorIlum.Text = "Adicional por iluminacion: 5%";
            // 
            // labelCBU
            // 
            labelCBU.AutoSize = true;
            labelCBU.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelCBU.Location = new Point(11, 527);
            labelCBU.Name = "labelCBU";
            labelCBU.Size = new Size(234, 28);
            labelCBU.TabIndex = 7;
            labelCBU.Text = "CBU: 1234567899875456";
            // 
            // labelServicios
            // 
            labelServicios.AutoSize = true;
            labelServicios.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelServicios.Location = new Point(11, 555);
            labelServicios.Name = "labelServicios";
            labelServicios.Size = new Size(93, 28);
            labelServicios.TabIndex = 7;
            labelServicios.Text = "Servicios:";
            // 
            // dgvServiciosComplejoDetail
            // 
            dgvServiciosComplejoDetail.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvServiciosComplejoDetail.Location = new Point(12, 586);
            dgvServiciosComplejoDetail.Name = "dgvServiciosComplejoDetail";
            dgvServiciosComplejoDetail.RowHeadersWidth = 51;
            dgvServiciosComplejoDetail.Size = new Size(623, 75);
            dgvServiciosComplejoDetail.TabIndex = 8;
            // 
            // buttonVerCanchas
            // 
            buttonVerCanchas.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonVerCanchas.Location = new Point(12, 679);
            buttonVerCanchas.Name = "buttonVerCanchas";
            buttonVerCanchas.Size = new Size(303, 29);
            buttonVerCanchas.TabIndex = 9;
            buttonVerCanchas.Text = "Ver Canchas";
            buttonVerCanchas.UseVisualStyleBackColor = true;
            // 
            // buttonVerReservas
            // 
            buttonVerReservas.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonVerReservas.Location = new Point(331, 679);
            buttonVerReservas.Name = "buttonVerReservas";
            buttonVerReservas.Size = new Size(303, 29);
            buttonVerReservas.TabIndex = 9;
            buttonVerReservas.Text = "Ver Reservas";
            buttonVerReservas.UseVisualStyleBackColor = true;
            // 
            // labelEstadoComplejoDetail
            // 
            labelEstadoComplejoDetail.AutoSize = true;
            labelEstadoComplejoDetail.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelEstadoComplejoDetail.Location = new Point(12, 721);
            labelEstadoComplejoDetail.Name = "labelEstadoComplejoDetail";
            labelEstadoComplejoDetail.Size = new Size(166, 28);
            labelEstadoComplejoDetail.TabIndex = 7;
            labelEstadoComplejoDetail.Text = "Estado: Pendiente";
            // 
            // buttonCambioEstado1
            // 
            buttonCambioEstado1.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonCambioEstado1.Location = new Point(404, 739);
            buttonCambioEstado1.Name = "buttonCambioEstado1";
            buttonCambioEstado1.Size = new Size(171, 45);
            buttonCambioEstado1.TabIndex = 9;
            buttonCambioEstado1.Text = "Habilitar";
            buttonCambioEstado1.UseVisualStyleBackColor = true;
            // 
            // buttonCambioEstado2
            // 
            buttonCambioEstado2.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonCambioEstado2.Location = new Point(581, 739);
            buttonCambioEstado2.Name = "buttonCambioEstado2";
            buttonCambioEstado2.Size = new Size(171, 45);
            buttonCambioEstado2.TabIndex = 9;
            buttonCambioEstado2.Text = "Rechazar";
            buttonCambioEstado2.UseVisualStyleBackColor = true;
            // 
            // FormComplexDetail
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(776, 796);
            Controls.Add(buttonCambioEstado2);
            Controls.Add(buttonCambioEstado1);
            Controls.Add(buttonVerReservas);
            Controls.Add(buttonVerCanchas);
            Controls.Add(dgvServiciosComplejoDetail);
            Controls.Add(labelAdPorIlum);
            Controls.Add(labelServicios);
            Controls.Add(labelEstadoComplejoDetail);
            Controls.Add(labelCBU);
            Controls.Add(labelHoraComiezoIlum);
            Controls.Add(labelPorcentajeSenia);
            Controls.Add(labelContactoComplejoDetail);
            Controls.Add(labelUbicacionComplejoDetail);
            Controls.Add(labelDescripcionComplejoDetail);
            Controls.Add(labelNombreComplejoDetail);
            Controls.Add(pictureBox1);
            Name = "FormComplexDetail";
            Text = "FormComplexDetail";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvServiciosComplejoDetail).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label labelNombreComplejoDetail;
        private Label labelDescripcionComplejoDetail;
        private Label labelUbicacionComplejoDetail;
        private Label labelContactoComplejoDetail;
        private Label labelPorcentajeSenia;
        private Label labelHoraComiezoIlum;
        private Label labelAdPorIlum;
        private Label labelCBU;
        private Label labelServicios;
        private DataGridView dgvServiciosComplejoDetail;
        private Button buttonVerCanchas;
        private Button buttonVerReservas;
        private Label labelEstadoComplejoDetail;
        private Button buttonCambioEstado1;
        private Button buttonCambioEstado2;
    }
}