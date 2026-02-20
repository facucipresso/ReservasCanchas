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
                DialogService.ShowError( Form.ActiveForm ?? this.TopLevelControl as Form, ex.Message);
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

    }
}
