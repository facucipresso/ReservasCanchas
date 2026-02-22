using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Models.Reservation;
using WinFormsApp1.Services;

namespace WinFormsApp1.UserControls
{
    public partial class UC_ComplexReservations : UserControl
    {

        private readonly ComplexService _complexService;
        private BindingList<ReservationResponseDTO> _reservations;
        private readonly int _complexId;

        public event Action VolverClicked;
        public event Action CerrarClicked;

        public UC_ComplexReservations(int complexId)
        {
            InitializeComponent();

            _complexId = complexId;
            _complexService = new ComplexService();

            this.Load += UC_ComplexListReservations_Load;

            btnVolver.Click += (s, e) => VolverClicked?.Invoke();
            btnCerrar.Click += (s, e) => CerrarClicked?.Invoke();

        }

        private async void UC_ComplexListReservations_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadComplexReservationsAsync();
            }
            catch (Exception ex)
            {
                DialogService.ShowError(Form.ActiveForm ?? this.TopLevelControl as Form, ex.Message);
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task LoadComplexReservationsAsync()
        {
            try
            {
                var lista = await _complexService.GetAllComplexReservationsAsync(_complexId);

                if (lista == null || !lista.Any())
                {
                    ShowEmptyState();
                    return;
                }

                ShowCards();

                flowLayoutPanelReservations.SuspendLayout();
                flowLayoutPanelReservations.Controls.Clear();

                foreach (var reservation in lista)
                {
                    var card = new UC_ReservationCard();
                    card.SetData(reservation);

                    card.Margin = new Padding(10);
                    card.Width = flowLayoutPanelReservations.ClientSize.Width - 30;

                    flowLayoutPanelReservations.Controls.Add(card);
                }

                flowLayoutPanelReservations.ResumeLayout();
            }
            catch (Exception ex)
            {
                DialogService.ShowError(Form.ActiveForm ?? this.TopLevelControl as Form, ex.Message);

            }
        }


        private void ShowEmptyState()
        {
            flowLayoutPanelReservations.Visible = false;
            dgvComplexReservations.Visible = false;
            labelComplexReservationsEmpty.Visible = true;
        }

        private void ShowCards()
        {
            labelComplexReservationsEmpty.Visible = false;
            dgvComplexReservations.Visible = false;
            flowLayoutPanelReservations.Visible = true;
        }

    }
}
