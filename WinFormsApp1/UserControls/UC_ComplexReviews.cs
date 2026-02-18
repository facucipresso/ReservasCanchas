using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Enum;
using WinFormsApp1.Models.Review;
using WinFormsApp1.Services;

namespace WinFormsApp1.UserControls
{
    public partial class UC_ComplexReviews : UserControl
    {
        private readonly ComplexService _complexService;
        private readonly int _complexId;

        private BindingList<ReviewResponseDTO> _reviews;

        public event Action VolverClicked;
        public event Action CerrarClicked;

        public UC_ComplexReviews(int complexId)
        {
            InitializeComponent();
            _complexId = complexId;
            _complexService = new ComplexService();

            this.Load += UC_ComplexListReviews_Load;

            btnVolver.Click += (s, e) => VolverClicked?.Invoke();
            btnCerrar.Click += (s, e) => CerrarClicked?.Invoke();
            //aca tenog que agregar el evento para eliminar creo


        }

        private async void UC_ComplexListReviews_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadComplexReviewsAsync();
            }
            catch (Exception ex)
            {
                DialogService.ShowError(Form.ActiveForm ?? this.TopLevelControl as Form, ex.Message);
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadComplexReviewsAsync()
        {
            try
            {
                var lista = await _complexService.GetAllComplexReviewsAsync(_complexId);

                flowLayoutPanelReviews.Controls.Clear();

                if (lista == null || !lista.Any())
                {
                    ShowEmptyState();
                    return;
                }

                ShowCards();

                foreach (var review in lista)
                {
                    var card = new UC_ReviewCard(review);

                    card.Width = flowLayoutPanelReviews.ClientSize.Width - 25;

                    card.DeleteClicked += async (id) =>
                    {
                        var confirm = MessageBox.Show(
                            $"¿Deseas eliminar el review {id}?",
                            "Confirmar",
                            MessageBoxButtons.YesNo);

                        if (confirm == DialogResult.Yes)
                        {
                            await _complexService.DeleteReviewsAsync(id);

                            Notifier.Show(this.FindForm(), "Review eliminada correctamente", NotificationType.Success);
                            await LoadComplexReviewsAsync();
                        }
                    };

                    flowLayoutPanelReviews.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                DialogService.ShowError( Form.ActiveForm ?? this.TopLevelControl as Form, "Error cargando reviews: " + ex.Message);
            }
        }

        private void ShowCards()
        {
            flowLayoutPanelReviews.Visible = true;
            labelComplexReviewEmpty.Visible = false;
        }

        private void ShowEmptyState()
        {
            flowLayoutPanelReviews.Visible = false;
            labelComplexReviewEmpty.Visible = true;
        }

        /*
        private async Task LoadComplexReviewsAsync()
        {
            try
            {
                var lista = await _complexService.GetAllComplexReviewsAsync(_complexId);

                if (lista == null || !lista.Any())
                {
                    //esto se agrega para el label
                    ShowEmptyState();
                    return;
                }
                //esto se agrega para el label
                ShowGrid();

                _reviews = new BindingList<ReviewResponseDTO>(lista);
                dgvComplexReviews.DataSource = _reviews;

                //ALGUNAS CONFIGURACIONES EXTRAS
                dgvComplexReviews.AutoGenerateColumns = true;
                dgvComplexReviews.ReadOnly = true;
                dgvComplexReviews.AllowUserToAddRows = false;
                dgvComplexReviews.AllowUserToDeleteRows = false;
                dgvComplexReviews.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvComplexReviews.MultiSelect = false;
                dgvComplexReviews.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


                //OCULTO COLUMNAS QUE NO QUIERO QUE SE VEAN
                dgvComplexReviews.Columns["Id"].Visible = false;
                dgvComplexReviews.Columns["UserId"].Visible = false;
                dgvComplexReviews.Columns["ReservationId"].Visible = false;

                //POSICION PARA EL BOTON DE ELIMINAR RESEÑA
                var totalColumnas = dgvComplexReviews.Columns.Count;
                dgvComplexReviews.Columns["ColumnEliminarReview"].DisplayIndex = totalColumnas - 1;



            }
            catch (Exception ex)
            {
                DialogService.ShowError(Form.ActiveForm ?? this.TopLevelControl as Form, "Error ocurrido cargando las reservas de la cancha por: " + ex.Message);
                //MessageBox.Show("Error ocurrido cargando las reservas de la cancha por: " + ex.Message);
            }


        }
        */

        /*
        private async void dgvComplexReviews_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Verificar que el clic no sea en el encabezado (fila -1)
            if (e.RowIndex < 0) return;

            // 2. Verificar si el clic fue en la columna de la imagen por su Nombre o Índice
            if (dgvComplexReviews.Columns[e.ColumnIndex].Name == "ColumnEliminarReview")
            {
                
                var id = Convert.ToInt32 (dgvComplexReviews.Rows[e.RowIndex].Cells["Id"].Value);

                DialogResult result = MessageBox.Show($"¿Deseas eliminar el review {id}?",
                                                    "Confirmar", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Ejecuto eliminación
                    await _complexService.DeleteReviewsAsync(id);

                    // refresco el dgv
                    await LoadComplexReviewsAsync();
                    Notifier.Show(this.FindForm(), "Review eliminada correctamente", NotificationType.Success);
                }
            }
        }
        */

        //esto se agrega para el label
        /*
        private void ShowEmptyState()
        {
            dgvComplexReviews.Visible = false;
            labelComplexReviewEmpty.Visible = true;
        }
        */

        //esto se agrega para el label
        /*
        private void ShowGrid()
        {
            dgvComplexReviews.Visible = true;
            labelComplexReviewEmpty.Visible = false;
        }
        */

    }
}
