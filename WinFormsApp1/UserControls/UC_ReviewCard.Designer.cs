namespace WinFormsApp1.UserControls
{
    partial class UC_ReviewCard
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
            panelNombreYPuntaje = new Panel();
            labelPuntaje = new Label();
            labelNombreUser = new Label();
            panelComentario = new Panel();
            buttonDeleteReview = new Button();
            labelComentario = new Label();
            panelNombreYPuntaje.SuspendLayout();
            panelComentario.SuspendLayout();
            SuspendLayout();
            // 
            // panelNombreYPuntaje
            // 
            panelNombreYPuntaje.BackColor = Color.White;
            panelNombreYPuntaje.Controls.Add(labelPuntaje);
            panelNombreYPuntaje.Controls.Add(labelNombreUser);
            panelNombreYPuntaje.Dock = DockStyle.Top;
            panelNombreYPuntaje.Location = new Point(0, 0);
            panelNombreYPuntaje.Name = "panelNombreYPuntaje";
            panelNombreYPuntaje.Size = new Size(772, 42);
            panelNombreYPuntaje.TabIndex = 0;
            // 
            // labelPuntaje
            // 
            labelPuntaje.AutoSize = true;
            labelPuntaje.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelPuntaje.ForeColor = Color.Goldenrod;
            labelPuntaje.Location = new Point(17, 7);
            labelPuntaje.Name = "labelPuntaje";
            labelPuntaje.Size = new Size(97, 28);
            labelPuntaje.TabIndex = 1;
            labelPuntaje.Text = "★★★★☆";
            // 
            // labelNombreUser
            // 
            labelNombreUser.AutoSize = true;
            labelNombreUser.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelNombreUser.Location = new Point(136, 12);
            labelNombreUser.Name = "labelNombreUser";
            labelNombreUser.Size = new Size(217, 23);
            labelNombreUser.TabIndex = 0;
            labelNombreUser.Text = "Nombre ficticio del usuario";
            // 
            // panelComentario
            // 
            panelComentario.BackColor = Color.White;
            panelComentario.Controls.Add(buttonDeleteReview);
            panelComentario.Controls.Add(labelComentario);
            panelComentario.Dock = DockStyle.Fill;
            panelComentario.Location = new Point(0, 42);
            panelComentario.Name = "panelComentario";
            panelComentario.Size = new Size(772, 58);
            panelComentario.TabIndex = 1;
            // 
            // buttonDeleteReview
            // 
            buttonDeleteReview.Location = new Point(597, 17);
            buttonDeleteReview.Name = "buttonDeleteReview";
            buttonDeleteReview.Size = new Size(128, 29);
            buttonDeleteReview.TabIndex = 1;
            buttonDeleteReview.Text = "Eliminar review";
            buttonDeleteReview.UseVisualStyleBackColor = true;
            // 
            // labelComentario
            // 
            labelComentario.AutoSize = true;
            labelComentario.Location = new Point(7, 15);
            labelComentario.Name = "labelComentario";
            labelComentario.Size = new Size(448, 20);
            labelComentario.TabIndex = 0;
            labelComentario.Text = "El comentario del usuario sobre el complejo o sobre lo que quiera";
            // 
            // UC_ReviewCard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(panelComentario);
            Controls.Add(panelNombreYPuntaje);
            Name = "UC_ReviewCard";
            Size = new Size(772, 100);
            panelNombreYPuntaje.ResumeLayout(false);
            panelNombreYPuntaje.PerformLayout();
            panelComentario.ResumeLayout(false);
            panelComentario.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelNombreYPuntaje;
        private Panel panelComentario;
        private Label labelPuntaje;
        private Label labelNombreUser;
        private Label labelComentario;
        private Button buttonDeleteReview;
    }
}
