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
using WinFormsApp1.Forms.Complex;
using WinFormsApp1.Infrastructure;
using WinFormsApp1.Models.Complex;
using WinFormsApp1.Models.Service;
using WinFormsApp1.Services;

namespace WinFormsApp1.UserControls
{
    public partial class UC_ComplexDetail : UserControl
    {
        private readonly ComplexService _complexService;
        private readonly HttpClient _httpClient;

        private int _complexId;
        private string _nameOwnwer;
        private string _lastNameOwnwer;
        private BindingList<ServiceResponseDTO> _services;

        // PARA AGREGAR LOS EVENTOS
        public event Action VerCanchasClicked;
        public event Action VerReservasClicked;
        public event Action VerReseniasClicked;

        //public event Action VolverClicked;
        public event Action CerrarClicked;

        public event Action? ComplexUpdated;


        public UC_ComplexDetail(int complexId, string? nameOwner = null, string? lastnameOwner = null)
        {
            InitializeComponent();

            _complexService = new ComplexService();
            _httpClient = ApiClient.Http;
            _complexId = complexId;
            _nameOwnwer = nameOwner ?? string.Empty;
            _lastNameOwnwer = lastnameOwner ?? string.Empty;

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            this.Load += UC_ComplexDetail_Load;

            // PARA AGREGAR LOS EVENTOS
            buttonVerCanchas.Click += (s, e) => VerCanchasClicked?.Invoke();
            buttonVerReservas.Click += (s, e) => VerReservasClicked?.Invoke();
            buttonVerResenias.Click += (s, e) => VerReseniasClicked?.Invoke();
            btnCerrar.Click += (s, e) => CerrarClicked?.Invoke();

        }

        private async void UC_ComplexDetail_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadComplexDetailAsync();
            }
            catch (Exception ex)
            {
                DialogService.ShowError(Form.ActiveForm ?? this.TopLevelControl as Form, ex.Message);
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadComplexDetailAsync()
        {
            var complex = await _complexService.GetComplexDetailByIdAsync(_complexId);

            if (complex == null)
            {
                Notifier.Show(this.FindForm(), "No se pudo mostrar el complejo", NotificationType.Error);
                //MessageBox.Show("No se pudo mostrar el complejo");
                return;
            }

            LoadTextData(complex);
            LoadServices(complex.Services);
            await LoadImageAsync(complex.ImagePath);
            ApplyStateUI(complex.State);

        }

        private void LoadTextData(ComplexDetailResponseDTO complex)
        {
            /*
            MessageBox.Show(
                $"LoadTextData ejecutado\nOwner: '{_nameOwnwer}' '{_lastNameOwnwer}'",
                "DEBUG"
            );
            */

            labelNombreComplejoDetail.Text = complex.Name;
            labelDescripcionComplejoDetail.Text = complex.Description;
            labelDuenioComplejo.Text = $"Dueño: {_nameOwnwer} {_lastNameOwnwer}";
            labelUbicacionComplejoDetail.Text =
                $"{complex.Province} - {complex.Locality} - {complex.Street} {complex.Number}";

            labelContactoComplejoDetail.Text = $"Contacto: {complex.Phone}";
            labelPorsentajeSenia.Text = $"Porcentaje de seña: {complex.PercentageSign}%";
            labelHoraComienzoIlum.Text = $"Inicio iluminación: {complex.StartIlumination}";
            labelAddPorIlum.Text = $"Adicional iluminación: {complex.AditionalIlumination}%";
            labelCBU.Text = $"CBU: {complex.CBU}";
            labelEstadoComplejoDetail.Text = $"Estado: {complex.State}";
        }

        private void LoadServices(IEnumerable<ServiceResponseDTO>? services)
        {
            if (services == null) return;

            _services = new BindingList<ServiceResponseDTO>(services.ToList());
            dataGridView1.DataSource = _services;

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


            dataGridView1.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridView1.Columns["ServiceDescription"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private async Task LoadImageAsync(string? imagePath)
        {
            // Cargo una imagen por defecto
            if (string.IsNullOrWhiteSpace(imagePath))
            {
                pictureBox1.Image = Properties.Resources.imgComplexDefault;
                return;
            }


            try
            {
                pictureBox1.Image?.Dispose();

                using var stream = await _httpClient.GetStreamAsync(imagePath);
                pictureBox1.Image = Image.FromStream(stream);
            }
            catch
            {
                // Cargo una imagen por defecto
                pictureBox1.Image = Properties.Resources.imgComplexDefault;
            }
        }

        private void ApplyStateUI(string state)
        {
            switch (state)
            {
                case "Pendiente":
                    buttonCambioDeEstado1.Show();
                    buttonCambioDeEstado2.Show();
                    buttonCambioDeEstado1.Text = "Habilitar";
                    buttonCambioDeEstado1.BackColor = Color.Green;
                    buttonCambioDeEstado2.Text = "Rechazar";
                    buttonCambioDeEstado2.BackColor = Color.Red;
                    break;
                case "Habilitado":
                    buttonCambioDeEstado1.Hide();
                    buttonCambioDeEstado2.Show();
                    buttonCambioDeEstado2.Text = "Bloquear";
                    buttonCambioDeEstado2.BackColor = Color.Red;
                    break;
                case "Rechazado":
                    buttonCambioDeEstado1.Hide();
                    buttonCambioDeEstado2.Hide();
                    //buttonCambioDeEstado2.Text = "Bloquear";
                    //buttonCambioDeEstado2.BackColor = Color.Red;
                    break;

                case "Deshabilitado":
                    buttonCambioDeEstado1.Hide();
                    buttonCambioDeEstado2.Show();
                    buttonCambioDeEstado2.Text = "Bloquear";
                    buttonCambioDeEstado2.BackColor = Color.Red;
                    break;
                
                case "Bloqueado":
                    buttonCambioDeEstado1.Hide();
                    buttonCambioDeEstado2.Show();
                    buttonCambioDeEstado2.Text = "Habilitar";
                    buttonCambioDeEstado2.BackColor = Color.Green;
                    break;
                 /* SI FUE RECHAZADO EL ADMIN DEL COMPLEJO HACE LAS REFORMAS Y LO VUELVE A PONER EN PENDIENTE
                 case "Rechazado":
                    buttonCambioDeEstado1.Hide();
                    buttonCambioDeEstado2.Show();
                    buttonCambioDeEstado2.Text = "Habilitar";
                    break;
                 */
            }

            labelEstadoComplejoDetail.ForeColor = state switch
            {
                "Pendiente" => Color.Orange,
                "Habilitado" => Color.Green,
                "Deshabilitado" => Color.Red,
                "Bloqueado" => Color.DarkRed,
                "Rechazado" => Color.Red,
                _ => Color.Gray
            };

        }

        private async void buttonCambioDeEstado1_Click(object sender, EventArgs e)
        {
            UpdateComplexStateDTO newState = new UpdateComplexStateDTO();
            switch (buttonCambioDeEstado1.Text)
            {
                case "Habilitar":
                    newState.State = ComplexState.Habilitado;
                    break;
                case "Deshabilitar":
                    newState.State = ComplexState.Deshabilitado;
                    break;
                default:
                    //newState.State = ComplexState.Deshabilitado;
                    break;
            }
            // UNA VEZ SALIDO DEL SWICH SE LLAMA AL SERVICIO CON EL NUEVO ESTADO
            
            
            await _complexService.ChangeStateComplexAsync(_complexId, newState);
            
            await LoadComplexDetailAsync();
            ComplexUpdated?.Invoke();
        }

        private async void buttonCambioDeEstado2_Click(object sender, EventArgs e)
        {
            UpdateComplexStateDTO newState = new UpdateComplexStateDTO();
            string? reason = null;
            string? action;
            string? successMessage = null;

            switch (buttonCambioDeEstado2.Text)
            {
                case "Habilitar":
                    newState.State = ComplexState.Habilitado;
                    successMessage = "Complejo habilitado correctamente";
                    break;
                case "Rechazar":
                    //newState.State = ComplexState.Rechazado;
                    action = buttonCambioDeEstado2.Text;

                    using (var modal = new FormReasonModal(action))
                    {
                        if (modal.ShowDialog(this) == DialogResult.OK)
                        {
                            reason = modal.ReasonText;
                        }
                        else
                        {
                            return; // Si cancela, no hacemos nada
                        }
                    }
                    newState.State = ComplexState.Rechazado;
                    successMessage = "Complejo rechazado correctamente";
                    break;
                case "Bloquear":
                    //newState.State = ComplexState.Bloqueado;
                    action = buttonCambioDeEstado2.Text;

                    using (var modal = new FormReasonModal(action))
                    {
                        if (modal.ShowDialog(this) == DialogResult.OK)
                        {
                            reason = modal.ReasonText;
                        }
                        else
                        {
                            return; // Si cancela, no hacemos nada
                        }
                    }
                    newState.State = ComplexState.Bloqueado;
                    successMessage = "Complejo bloqueado correctamente";
                    break;
                default:
                    //newState.State = ComplexState.Rechazado;
                    break;
            }

            newState.CancelationReason = reason; // null si no corresponde

            await _complexService.ChangeStateComplexAsync(_complexId, newState);
            
            await LoadComplexDetailAsync();
            ComplexUpdated?.Invoke();

            Notifier.Show(this.FindForm(), successMessage, NotificationType.Success);
            //DialogService.ShowError(Form.ActiveForm ?? this.TopLevelControl as Form, "Ocurrió un error inesperado.");

        }
    }
}
