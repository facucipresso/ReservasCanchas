using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Models.Reservation;
using WinFormsApp1.Services;

namespace WinFormsApp1.UserControls
{
    public partial class UC_FieldListReservations : UserControl
    {
        private readonly ComplexService _complexService;
        private BindingList<ReservationResponseDTO> _reservations;
        private readonly int _fieldId;

        public event Action VolverClicked;
        public event Action CerrarClicked;

        public UC_FieldListReservations(int fieldId)
        {
            InitializeComponent();
            _fieldId = fieldId;
            _complexService = new ComplexService();

            this.Load += UC_FieldListReservations_Load;

            btnVolver.Click += (s, e) =>VolverClicked?.Invoke();
            btnCerrar.Click += (s, e) =>CerrarClicked?.Invoke();
        }



        private async void UC_FieldListReservations_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadFieldsReservationsAsync();
            }
            catch (Exception ex)
            {
                DialogService.ShowError(Form.ActiveForm ?? this.TopLevelControl as Form, ex.Message);
            }
        }

        private async Task LoadFieldsReservationsAsync()
        {
            try
            {
                var lista = await _complexService.GetAllReservationsForFieldAsync(_fieldId);

                if (lista == null || !lista.Any())
                {
                    ShowEmptyState();
                    return;
                }

                ShowCards();

                flowLayoutPanelFieldReservations.SuspendLayout();
                flowLayoutPanelFieldReservations.Controls.Clear();

                foreach (var reservation in lista)
                {
                    var card = new UC_ReservationCard();
                    card.SetData(reservation);
                    card.SetCompactMode(true);

                    card.Margin = new Padding(10);
                    card.Width = flowLayoutPanelFieldReservations.ClientSize.Width - 30;

                    flowLayoutPanelFieldReservations.Controls.Add(card);
                }

                flowLayoutPanelFieldReservations.ResumeLayout();



            }
            catch (Exception ex)
            {
                DialogService.ShowError(Form.ActiveForm ?? this.TopLevelControl as Form, ex.Message);
            }


        }

        private void ShowEmptyState()
        {
            flowLayoutPanelFieldReservations.Visible = false;
            dgvFieldListReservations.Visible = false;
            labelFieldReservationsEmpty.Visible = true;
        }

        private void ShowCards()
        {
            labelFieldReservationsEmpty.Visible = false;
            dgvFieldListReservations.Visible = false;
            flowLayoutPanelFieldReservations.Visible = true;
        }
    }
}
