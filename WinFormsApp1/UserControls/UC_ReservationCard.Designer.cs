namespace WinFormsApp1.UserControls
{
    partial class UC_ReservationCard
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
            panelNotificationTop = new Panel();
            labelCanchaReserva = new Label();
            labelNombreQuienReserva = new Label();
            labelHoraReserva = new Label();
            labelFechaReserva = new Label();
            panelNotificationButtom = new Panel();
            labelTipoReserva = new Label();
            labelValorTotalReserva = new Label();
            labelFechaCreacionReserva = new Label();
            labelTipoPagoReserva = new Label();
            labelEstadoReserva = new Label();
            panelNotificationTop.SuspendLayout();
            panelNotificationButtom.SuspendLayout();
            SuspendLayout();
            // 
            // panelNotificationTop
            // 
            panelNotificationTop.BackColor = Color.White;
            panelNotificationTop.Controls.Add(labelCanchaReserva);
            panelNotificationTop.Controls.Add(labelNombreQuienReserva);
            panelNotificationTop.Controls.Add(labelHoraReserva);
            panelNotificationTop.Controls.Add(labelFechaReserva);
            panelNotificationTop.Dock = DockStyle.Top;
            panelNotificationTop.Location = new Point(0, 0);
            panelNotificationTop.Name = "panelNotificationTop";
            panelNotificationTop.Padding = new Padding(16, 6, 16, 6);
            panelNotificationTop.Size = new Size(772, 33);
            panelNotificationTop.TabIndex = 0;
            // 
            // labelCanchaReserva
            // 
            labelCanchaReserva.AutoSize = true;
            labelCanchaReserva.Dock = DockStyle.Right;
            labelCanchaReserva.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelCanchaReserva.Location = new Point(591, 6);
            labelCanchaReserva.Name = "labelCanchaReserva";
            labelCanchaReserva.Size = new Size(165, 25);
            labelCanchaReserva.TabIndex = 0;
            labelCanchaReserva.Text = "Cancha 3 chancha7";
            // 
            // labelNombreQuienReserva
            // 
            labelNombreQuienReserva.AutoSize = true;
            labelNombreQuienReserva.Dock = DockStyle.Fill;
            labelNombreQuienReserva.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelNombreQuienReserva.Location = new Point(192, 6);
            labelNombreQuienReserva.Name = "labelNombreQuienReserva";
            labelNombreQuienReserva.Size = new Size(100, 25);
            labelNombreQuienReserva.TabIndex = 0;
            labelNombreQuienReserva.Text = "Juan Perez";
            // 
            // labelHoraReserva
            // 
            labelHoraReserva.AutoSize = true;
            labelHoraReserva.Dock = DockStyle.Left;
            labelHoraReserva.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelHoraReserva.Location = new Point(116, 6);
            labelHoraReserva.Name = "labelHoraReserva";
            labelHoraReserva.Size = new Size(76, 25);
            labelHoraReserva.TabIndex = 0;
            labelHoraReserva.Text = "18:00 hs";
            // 
            // labelFechaReserva
            // 
            labelFechaReserva.AutoSize = true;
            labelFechaReserva.Dock = DockStyle.Left;
            labelFechaReserva.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelFechaReserva.Location = new Point(16, 6);
            labelFechaReserva.Name = "labelFechaReserva";
            labelFechaReserva.Size = new Size(100, 25);
            labelFechaReserva.TabIndex = 0;
            labelFechaReserva.Text = "12/10/2026";
            // 
            // panelNotificationButtom
            // 
            panelNotificationButtom.BackColor = Color.White;
            panelNotificationButtom.Controls.Add(labelTipoReserva);
            panelNotificationButtom.Controls.Add(labelValorTotalReserva);
            panelNotificationButtom.Controls.Add(labelFechaCreacionReserva);
            panelNotificationButtom.Controls.Add(labelTipoPagoReserva);
            panelNotificationButtom.Controls.Add(labelEstadoReserva);
            panelNotificationButtom.Dock = DockStyle.Fill;
            panelNotificationButtom.Location = new Point(0, 33);
            panelNotificationButtom.Name = "panelNotificationButtom";
            panelNotificationButtom.Padding = new Padding(16, 6, 16, 6);
            panelNotificationButtom.Size = new Size(772, 67);
            panelNotificationButtom.TabIndex = 1;
            // 
            // labelTipoReserva
            // 
            labelTipoReserva.AutoSize = true;
            labelTipoReserva.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTipoReserva.Location = new Point(23, 31);
            labelTipoReserva.Name = "labelTipoReserva";
            labelTipoReserva.Size = new Size(97, 20);
            labelTipoReserva.TabIndex = 0;
            labelTipoReserva.Text = "Tipo: Partido";
            // 
            // labelValorTotalReserva
            // 
            labelValorTotalReserva.AutoSize = true;
            labelValorTotalReserva.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelValorTotalReserva.Location = new Point(593, 3);
            labelValorTotalReserva.Name = "labelValorTotalReserva";
            labelValorTotalReserva.Size = new Size(113, 23);
            labelValorTotalReserva.TabIndex = 0;
            labelValorTotalReserva.Text = "Total: $60.000";
            // 
            // labelFechaCreacionReserva
            // 
            labelFechaCreacionReserva.AutoSize = true;
            labelFechaCreacionReserva.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelFechaCreacionReserva.Location = new Point(262, 31);
            labelFechaCreacionReserva.Name = "labelFechaCreacionReserva";
            labelFechaCreacionReserva.Size = new Size(193, 20);
            labelFechaCreacionReserva.TabIndex = 0;
            labelFechaCreacionReserva.Text = "Reserva creada: 10/10/2025";
            // 
            // labelTipoPagoReserva
            // 
            labelTipoPagoReserva.AutoSize = true;
            labelTipoPagoReserva.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTipoPagoReserva.Location = new Point(262, 3);
            labelTipoPagoReserva.Name = "labelTipoPagoReserva";
            labelTipoPagoReserva.Size = new Size(119, 23);
            labelTipoPagoReserva.TabIndex = 0;
            labelTipoPagoReserva.Text = "Pago: Efectivo";
            // 
            // labelEstadoReserva
            // 
            labelEstadoReserva.AutoSize = true;
            labelEstadoReserva.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelEstadoReserva.Location = new Point(23, 3);
            labelEstadoReserva.Name = "labelEstadoReserva";
            labelEstadoReserva.Size = new Size(146, 23);
            labelEstadoReserva.TabIndex = 0;
            labelEstadoReserva.Text = "Estado: Pendiente";
            // 
            // UC_ReservationCard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            Controls.Add(panelNotificationButtom);
            Controls.Add(panelNotificationTop);
            Name = "UC_ReservationCard";
            Size = new Size(772, 100);
            panelNotificationTop.ResumeLayout(false);
            panelNotificationTop.PerformLayout();
            panelNotificationButtom.ResumeLayout(false);
            panelNotificationButtom.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelNotificationTop;
        private Panel panelNotificationButtom;
        private Label labelCanchaReserva;
        private Label labelNombreQuienReserva;
        private Label labelHoraReserva;
        private Label labelFechaReserva;
        private Label labelTipoReserva;
        private Label labelTipoPagoReserva;
        private Label labelEstadoReserva;
        private Label labelValorTotalReserva;
        private Label labelFechaCreacionReserva;
    }
}
