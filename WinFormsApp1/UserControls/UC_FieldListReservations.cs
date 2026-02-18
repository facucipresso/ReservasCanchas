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
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                /*
                ShowGrid();

                _reservations = new BindingList<ReservationResponseDTO>(lista);
                dgvFieldListReservations.DataSource = _reservations;

                //ALGUNAS CONFIGURACIONES EXTRAS
                dgvFieldListReservations.AutoGenerateColumns = true;
                dgvFieldListReservations.ReadOnly = true;
                dgvFieldListReservations.AllowUserToAddRows = false;
                dgvFieldListReservations.AllowUserToDeleteRows = false;
                dgvFieldListReservations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvFieldListReservations.MultiSelect = false;
                dgvFieldListReservations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


                //OCULTO COLUMNAS QUE NO QUIERO QUE SE VEAN
                dgvFieldListReservations.Columns["ReservationId"].Visible = false;
                dgvFieldListReservations.Columns["FieldId"].Visible = false;
                dgvFieldListReservations.Columns["FieldName"].Visible = false;
                dgvFieldListReservations.Columns["UserId"].Visible = false;
                dgvFieldListReservations.Columns["UserLastName"].Visible = false;
                dgvFieldListReservations.Columns["UserEmail"].Visible = false;
                dgvFieldListReservations.Columns["UserPhone"].Visible = false;
                dgvFieldListReservations.Columns["PricePaid"].Visible = false;

                //CAMBIO NOMBRE DE LAS COLUMNAS AUTOGENERADAS
                dgvFieldListReservations.Columns["CreationDate"].HeaderText = "Creacion";
                dgvFieldListReservations.Columns["Date"].HeaderText = "Fecha";
                dgvFieldListReservations.Columns["InitTime"].HeaderText = "Horario";
                dgvFieldListReservations.Columns["ReservationState"].HeaderText = "Estado";
                dgvFieldListReservations.Columns["UserName"].HeaderText = "Usuario";
                dgvFieldListReservations.Columns["PayType"].HeaderText = "Tipo pago";
                dgvFieldListReservations.Columns["TotalPrice"].HeaderText = "Total";
                dgvFieldListReservations.Columns["ReservationType"].HeaderText = "Tipo";
                dgvFieldListReservations.Columns["VoucherPath"].HeaderText = "Comprobante";
                */


            }
            catch (Exception ex)
            {
                DialogService.ShowError(Form.ActiveForm ?? this.TopLevelControl as Form, "Error ocurrido cargando las reservas de la cancha: " + ex.Message);
                /*
                MessageBox.Show(
                    "Error ocurrido cargando las reservas de la cancha:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                */
            }


        }

        /*
        //esto se agrega para el label
        private void ShowEmptyState()
        {
            dgvFieldListReservations.Visible = false;
            labelFieldReservationsEmpty.Visible = true;
        }
        //esto se agrega para el label
        private void ShowGrid()
        {
            dgvFieldListReservations.Visible = true;
            labelFieldReservationsEmpty.Visible = false;
        }
        */

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
