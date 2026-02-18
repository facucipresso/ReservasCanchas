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
                DialogService.ShowError(Form.ActiveForm ?? this.TopLevelControl as Form, "Error ocurrido cargando las reservas del complejo:\n" + ex.Message);

                /*
                MessageBox.Show(
                    "Error ocurrido cargando las reservas del complejo:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                */
            }
        }
        /*
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

                ShowGrid();

                _reservations = new BindingList<ReservationResponseDTO>(lista);
                dgvComplexReservations.DataSource = _reservations;

                //ALGUNAS CONFIGURACIONES EXTRAS
                dgvComplexReservations.AutoGenerateColumns = true;
                dgvComplexReservations.ReadOnly = true;
                dgvComplexReservations.AllowUserToAddRows = false;
                dgvComplexReservations.AllowUserToDeleteRows = false;
                dgvComplexReservations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvComplexReservations.MultiSelect = false;
                dgvComplexReservations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


                //OCULTO COLUMNAS QUE NO QUIERO QUE SE VEAN
                dgvComplexReservations.Columns["ReservationId"].Visible = false;
                dgvComplexReservations.Columns["FieldId"].Visible = false;
                //dgvComplexReservations.Columns["FieldName"].Visible = false;
                dgvComplexReservations.Columns["UserId"].Visible = false;
                dgvComplexReservations.Columns["UserLastName"].Visible = false;
                dgvComplexReservations.Columns["UserEmail"].Visible = false;
                dgvComplexReservations.Columns["UserPhone"].Visible = false;
                dgvComplexReservations.Columns["PricePaid"].Visible = false;

                //CAMBIO NOMBRE DE LAS COLUMNAS AUTOGENERADAS
                dgvComplexReservations.Columns["CreationDate"].HeaderText = "Creacion";
                dgvComplexReservations.Columns["Date"].HeaderText = "Fecha";
                dgvComplexReservations.Columns["InitTime"].HeaderText = "Horario";
                dgvComplexReservations.Columns["ReservationState"].HeaderText = "Estado";
                dgvComplexReservations.Columns["UserName"].HeaderText = "Usuario";
                dgvComplexReservations.Columns["PayType"].HeaderText = "Tipo pago";
                dgvComplexReservations.Columns["TotalPrice"].HeaderText = "Total";
                dgvComplexReservations.Columns["ReservationType"].HeaderText = "Tipo";
                dgvComplexReservations.Columns["VoucherPath"].HeaderText = "Comprobante";
                dgvComplexReservations.Columns["FieldName"].HeaderText = "Cancha";



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ocurrido cargando las reservas de la cancha por: " + ex.Message);
            }
        }*/

        //esto se agrega para el label
        /*
        private void ShowEmptyState()
        {
            dgvComplexReservations.Visible = false;
            labelComplexReservationsEmpty.Visible = true;
        }
        //esto se agrega para el label
        private void ShowGrid()
        {
            dgvComplexReservations.Visible = true;
            labelComplexReservationsEmpty.Visible = false;
        }
        */

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
