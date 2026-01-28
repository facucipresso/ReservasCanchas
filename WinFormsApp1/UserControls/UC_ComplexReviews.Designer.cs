namespace WinFormsApp1.UserControls
{
    partial class UC_ComplexReviews
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
            panel1 = new Panel();
            btnVolver = new Button();
            btnCerrar = new Button();
            labelReseniaDelComplejo = new Label();
            labelComplexReviewEmpty = new Label();
            dgvComplexReviews = new DataGridView();
            ColumnEliminarReview = new DataGridViewImageColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvComplexReviews).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnVolver);
            panel1.Controls.Add(btnCerrar);
            panel1.Controls.Add(labelReseniaDelComplejo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(774, 60);
            panel1.TabIndex = 0;
            // 
            // btnVolver
            // 
            btnVolver.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVolver.Location = new Point(536, 20);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(94, 29);
            btnVolver.TabIndex = 1;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
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
            // labelReseniaDelComplejo
            // 
            labelReseniaDelComplejo.AutoSize = true;
            labelReseniaDelComplejo.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            labelReseniaDelComplejo.Location = new Point(24, 16);
            labelReseniaDelComplejo.Name = "labelReseniaDelComplejo";
            labelReseniaDelComplejo.Size = new Size(236, 31);
            labelReseniaDelComplejo.TabIndex = 0;
            labelReseniaDelComplejo.Text = "Reseñas del complejo";
            // 
            // labelComplexReviewEmpty
            // 
            labelComplexReviewEmpty.Dock = DockStyle.Fill;
            labelComplexReviewEmpty.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Italic);
            labelComplexReviewEmpty.ForeColor = Color.Gray;
            labelComplexReviewEmpty.Location = new Point(0, 60);
            labelComplexReviewEmpty.Name = "labelComplexReviewEmpty";
            labelComplexReviewEmpty.Size = new Size(774, 841);
            labelComplexReviewEmpty.TabIndex = 10;
            labelComplexReviewEmpty.Text = "Este complejo aún no recibió reseñas.";
            labelComplexReviewEmpty.TextAlign = ContentAlignment.MiddleCenter;
            labelComplexReviewEmpty.Visible = false;
            // 
            // dgvComplexReviews
            // 
            dgvComplexReviews.BackgroundColor = SystemColors.Control;
            dgvComplexReviews.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvComplexReviews.Columns.AddRange(new DataGridViewColumn[] { ColumnEliminarReview });
            dgvComplexReviews.Dock = DockStyle.Fill;
            dgvComplexReviews.Location = new Point(0, 60);
            dgvComplexReviews.Name = "dgvComplexReviews";
            dgvComplexReviews.RowHeadersWidth = 51;
            dgvComplexReviews.Size = new Size(774, 841);
            dgvComplexReviews.TabIndex = 1;
            dgvComplexReviews.CellContentClick += dgvComplexReviews_CellContentClick;
            // 
            // ColumnEliminarReview
            // 
            ColumnEliminarReview.HeaderText = "Eliminar";
            ColumnEliminarReview.MinimumWidth = 6;
            ColumnEliminarReview.Name = "ColumnEliminarReview";
            ColumnEliminarReview.Width = 125;
            // 
            // UC_ComplexReviews
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(labelComplexReviewEmpty);
            Controls.Add(dgvComplexReviews);
            Controls.Add(panel1);
            Name = "UC_ComplexReviews";
            Size = new Size(774, 901);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvComplexReviews).EndInit();
            ResumeLayout(false);


        }

        #endregion

        private Panel panel1;
        private Label labelComplexReviewEmpty; //esto se agrega para el label
        private DataGridView dgvComplexReviews;
        private DataGridViewImageColumn ColumnEliminarReview;
        private Label labelReseniaDelComplejo;
        private Button btnCerrar;
        private Button btnVolver;
    }
}
