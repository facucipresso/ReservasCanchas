namespace WinFormsApp1.UserControls
{
    partial class UC_ComplexDetail
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
            buttonVerCanchas = new Button();
            buttonVerReservas = new Button();
            buttonVerResenias = new Button();
            pictureBox1 = new PictureBox();
            labelNombreComplejoDetail = new Label();
            labelDescripcionComplejoDetail = new Label();
            labelUbicacionComplejoDetail = new Label();
            labelContactoComplejoDetail = new Label();
            labelPorsentajeSenia = new Label();
            labelHoraComienzoIlum = new Label();
            labelCBU = new Label();
            labelServicios = new Label();
            labelAddPorIlum = new Label();
            dataGridView1 = new DataGridView();
            labelEstadoComplejoDetail = new Label();
            buttonCambioDeEstado1 = new Button();
            buttonCambioDeEstado2 = new Button();
            label1 = new Label();
            panel1 = new Panel();
            btnCerrar = new Button();
            labelComplexDetail = new Label();
            labelDuenioComplejo = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // buttonVerCanchas
            // 
            buttonVerCanchas.BackColor = Color.RoyalBlue;
            buttonVerCanchas.ForeColor = Color.LightGray;
            buttonVerCanchas.Location = new Point(11, 784);
            buttonVerCanchas.Name = "buttonVerCanchas";
            buttonVerCanchas.Size = new Size(226, 29);
            buttonVerCanchas.TabIndex = 0;
            buttonVerCanchas.Text = "Ver canchas";
            buttonVerCanchas.UseVisualStyleBackColor = false;
            // 
            // buttonVerReservas
            // 
            buttonVerReservas.BackColor = Color.RoyalBlue;
            buttonVerReservas.ForeColor = Color.LightGray;
            buttonVerReservas.Location = new Point(264, 784);
            buttonVerReservas.Name = "buttonVerReservas";
            buttonVerReservas.Size = new Size(226, 29);
            buttonVerReservas.TabIndex = 0;
            buttonVerReservas.Text = "Ver reservas";
            buttonVerReservas.UseVisualStyleBackColor = false;
            // 
            // buttonVerResenias
            // 
            buttonVerResenias.BackColor = Color.RoyalBlue;
            buttonVerResenias.ForeColor = Color.LightGray;
            buttonVerResenias.Location = new Point(519, 784);
            buttonVerResenias.Name = "buttonVerResenias";
            buttonVerResenias.Size = new Size(226, 29);
            buttonVerResenias.TabIndex = 0;
            buttonVerResenias.Text = "Ver reseñas";
            buttonVerResenias.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(12, 74);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(752, 263);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // labelNombreComplejoDetail
            // 
            labelNombreComplejoDetail.AutoSize = true;
            labelNombreComplejoDetail.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            labelNombreComplejoDetail.Location = new Point(40, 340);
            labelNombreComplejoDetail.Name = "labelNombreComplejoDetail";
            labelNombreComplejoDetail.Size = new Size(295, 38);
            labelNombreComplejoDetail.TabIndex = 2;
            labelNombreComplejoDetail.Text = "Nombre del Complejo";
            // 
            // labelDescripcionComplejoDetail
            // 
            labelDescripcionComplejoDetail.Font = new Font("Segoe UI", 13.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            labelDescripcionComplejoDetail.Location = new Point(40, 380);
            labelDescripcionComplejoDetail.Name = "labelDescripcionComplejoDetail";
            labelDescripcionComplejoDetail.Size = new Size(724, 80);
            labelDescripcionComplejoDetail.TabIndex = 2;
            labelDescripcionComplejoDetail.Text = "Descripcion del Complejo";
            // 
            // labelUbicacionComplejoDetail
            // 
            labelUbicacionComplejoDetail.AutoSize = true;
            labelUbicacionComplejoDetail.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelUbicacionComplejoDetail.Location = new Point(24, 484);
            labelUbicacionComplejoDetail.Name = "labelUbicacionComplejoDetail";
            labelUbicacionComplejoDetail.Size = new Size(303, 28);
            labelUbicacionComplejoDetail.TabIndex = 2;
            labelUbicacionComplejoDetail.Text = "Provincia - Localidad - Direccion";
            // 
            // labelContactoComplejoDetail
            // 
            labelContactoComplejoDetail.AutoSize = true;
            labelContactoComplejoDetail.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelContactoComplejoDetail.Location = new Point(24, 529);
            labelContactoComplejoDetail.Name = "labelContactoComplejoDetail";
            labelContactoComplejoDetail.Size = new Size(288, 28);
            labelContactoComplejoDetail.TabIndex = 2;
            labelContactoComplejoDetail.Text = "Contacto/Numero de telefono";
            // 
            // labelPorsentajeSenia
            // 
            labelPorsentajeSenia.AutoSize = true;
            labelPorsentajeSenia.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelPorsentajeSenia.Location = new Point(429, 529);
            labelPorsentajeSenia.Name = "labelPorsentajeSenia";
            labelPorsentajeSenia.Size = new Size(231, 28);
            labelPorsentajeSenia.TabIndex = 2;
            labelPorsentajeSenia.Text = "Porcentaje de seña: 10%";
            // 
            // labelHoraComienzoIlum
            // 
            labelHoraComienzoIlum.AutoSize = true;
            labelHoraComienzoIlum.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelHoraComienzoIlum.Location = new Point(24, 574);
            labelHoraComienzoIlum.Name = "labelHoraComienzoIlum";
            labelHoraComienzoIlum.Size = new Size(313, 28);
            labelHoraComienzoIlum.TabIndex = 2;
            labelHoraComienzoIlum.Text = "Hora comienzo iluminacion: 8PM";
            // 
            // labelCBU
            // 
            labelCBU.AutoSize = true;
            labelCBU.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelCBU.Location = new Point(24, 619);
            labelCBU.Name = "labelCBU";
            labelCBU.Size = new Size(210, 28);
            labelCBU.TabIndex = 2;
            labelCBU.Text = "CBU: 12354657981569";
            // 
            // labelServicios
            // 
            labelServicios.AutoSize = true;
            labelServicios.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelServicios.Location = new Point(13, 664);
            labelServicios.Name = "labelServicios";
            labelServicios.Size = new Size(216, 28);
            labelServicios.TabIndex = 2;
            labelServicios.Text = "Servicios del complejo";
            // 
            // labelAddPorIlum
            // 
            labelAddPorIlum.AutoSize = true;
            labelAddPorIlum.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelAddPorIlum.Location = new Point(429, 574);
            labelAddPorIlum.Name = "labelAddPorIlum";
            labelAddPorIlum.Size = new Size(293, 28);
            labelAddPorIlum.TabIndex = 2;
            labelAddPorIlum.Text = "Adicional por iluminacion: 30%";
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(13, 694);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(728, 86);
            dataGridView1.TabIndex = 3;
            // 
            // labelEstadoComplejoDetail
            // 
            labelEstadoComplejoDetail.AutoSize = true;
            labelEstadoComplejoDetail.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelEstadoComplejoDetail.Location = new Point(25, 839);
            labelEstadoComplejoDetail.Name = "labelEstadoComplejoDetail";
            labelEstadoComplejoDetail.Size = new Size(198, 31);
            labelEstadoComplejoDetail.TabIndex = 2;
            labelEstadoComplejoDetail.Text = "Estado: Pendiente";
            // 
            // buttonCambioDeEstado1
            // 
            buttonCambioDeEstado1.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonCambioDeEstado1.Location = new Point(274, 830);
            buttonCambioDeEstado1.Name = "buttonCambioDeEstado1";
            buttonCambioDeEstado1.Size = new Size(226, 62);
            buttonCambioDeEstado1.TabIndex = 0;
            buttonCambioDeEstado1.Text = "Habilitar";
            buttonCambioDeEstado1.UseVisualStyleBackColor = true;
            buttonCambioDeEstado1.Click += buttonCambioDeEstado1_Click;
            // 
            // buttonCambioDeEstado2
            // 
            buttonCambioDeEstado2.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonCambioDeEstado2.Location = new Point(520, 825);
            buttonCambioDeEstado2.Name = "buttonCambioDeEstado2";
            buttonCambioDeEstado2.Size = new Size(226, 62);
            buttonCambioDeEstado2.TabIndex = 0;
            buttonCambioDeEstado2.Text = "Rechazar";
            buttonCambioDeEstado2.UseVisualStyleBackColor = true;
            buttonCambioDeEstado2.Click += buttonCambioDeEstado2_Click;
            // 
            // label1
            // 
            label1.Location = new Point(13, 460);
            label1.Name = "label1";
            label1.Size = new Size(729, 18);
            label1.TabIndex = 4;
            label1.Text = "________________________________________________________________________________________________________________________";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Control;
            panel1.Controls.Add(btnCerrar);
            panel1.Controls.Add(labelComplexDetail);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(774, 60);
            panel1.TabIndex = 5;
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
            // labelComplexDetail
            // 
            labelComplexDetail.AutoSize = true;
            labelComplexDetail.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            labelComplexDetail.Location = new Point(24, 16);
            labelComplexDetail.Name = "labelComplexDetail";
            labelComplexDetail.Size = new Size(228, 31);
            labelComplexDetail.TabIndex = 0;
            labelComplexDetail.Text = "Detalle del complejo";
            // 
            // labelDuenioComplejo
            // 
            labelDuenioComplejo.AutoSize = true;
            labelDuenioComplejo.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelDuenioComplejo.Location = new Point(407, 432);
            labelDuenioComplejo.Name = "labelDuenioComplejo";
            labelDuenioComplejo.Size = new Size(243, 28);
            labelDuenioComplejo.TabIndex = 2;
            labelDuenioComplejo.Text = "Dueño: Facundo Cipresso";
            // 
            // UC_ComplexDetail
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(panel1);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Controls.Add(labelEstadoComplejoDetail);
            Controls.Add(labelServicios);
            Controls.Add(labelCBU);
            Controls.Add(labelAddPorIlum);
            Controls.Add(labelHoraComienzoIlum);
            Controls.Add(labelPorsentajeSenia);
            Controls.Add(labelDuenioComplejo);
            Controls.Add(labelContactoComplejoDetail);
            Controls.Add(labelUbicacionComplejoDetail);
            Controls.Add(labelDescripcionComplejoDetail);
            Controls.Add(labelNombreComplejoDetail);
            Controls.Add(pictureBox1);
            Controls.Add(buttonVerResenias);
            Controls.Add(buttonCambioDeEstado2);
            Controls.Add(buttonCambioDeEstado1);
            Controls.Add(buttonVerReservas);
            Controls.Add(buttonVerCanchas);
            Name = "UC_ComplexDetail";
            Size = new Size(774, 841);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonVerCanchas;
        private Button buttonVerReservas;
        private Button buttonVerResenias;
        private PictureBox pictureBox1;
        private Label labelNombreComplejoDetail;
        private Label labelDescripcionComplejoDetail;
        private Label labelUbicacionComplejoDetail;
        private Label labelContactoComplejoDetail;
        private Label labelPorsentajeSenia;
        private Label labelHoraComienzoIlum;
        private Label labelCBU;
        private Label labelServicios;
        private Label labelAddPorIlum;
        private DataGridView dataGridView1;
        private Label labelEstadoComplejoDetail;
        private Button buttonCambioDeEstado1;
        private Button buttonCambioDeEstado2;
        private Label label1;
        private Panel panel1;
        private Button btnCerrar;
        private Label labelComplexDetail;
        private Label labelDuenioComplejo;
    }
}
