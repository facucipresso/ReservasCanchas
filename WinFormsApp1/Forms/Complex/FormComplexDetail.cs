using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Models.Complex;
using WinFormsApp1.Models.Service;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms.Complex
{
    public partial class FormComplexDetail : Form
    {
        private int idComplejo;
        private readonly ComplexService _complexService;

        private BindingList<ServiceResponseDTO> _servicios;

        private readonly HttpClient _httpClient;   


        public FormComplexDetail(int id)
        {
            InitializeComponent();
            _complexService = new ComplexService();
            this.idComplejo = id;

            _httpClient = Infrastructure.ApiClient.Http;

            this.Load += ComplejosDetail_Load;

            //toda la vaina de la imagen
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

        }

        private async void ComplejosDetail_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadComplexDetailAsync(idComplejo);
            }
            catch (Exception ex)
            {
                DialogService.ShowError(Form.ActiveForm ?? this.TopLevelControl as Form, ex.Message);
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadComplexDetailAsync(int id)
        {
            //no entiendo que hace aca
            //flowLayoutPanelComplexes.Controls.Clear();

            var complexDetail = await _complexService.GetComplexDetailByIdAsync(id); // tu servicio

            if (complexDetail == null)
            {
                // no me parece profesional poner un label, capaz un menssage box o algo mas profesional
                var lbl = new Label { Text = "Devolvio null la busqueda del detalle del complejo.", AutoSize = true, Padding = new Padding(10) };
                //flowLayoutPanelComplexes.Controls.Add(lbl);
                return;
            }

            //metodo para cargar la imagen
            await LoadComplexImageAsync(complexDetail.ImagePath);


            //card.SetData(complexDetail); // tu método para llenar los labels
            //card.EnterClicked += ComplexCard_EnterClicked; // se dispara cuando hacen click en "Entrar"
            //flowLayoutPanelComplexes.Controls.Add(card);

            //como hago para darle forma a la card con los datso que recibi



            labelNombreComplejoDetail.Text = complexDetail.Name;
            labelDescripcionComplejoDetail.Text = complexDetail.Description;
            labelUbicacionComplejoDetail.Text = $"Ubicacion: {complexDetail.Province} - {complexDetail.Locality} - {complexDetail.Street} {complexDetail.Number}";
            labelContactoComplejoDetail.Text = $"Contacto: {complexDetail.Phone}";
            labelPorcentajeSenia.Text = $"Porcentaje de seña: {complexDetail.PercentageSign}%";
            labelHoraComiezoIlum.Text = $"Horario comienzo iluminacion: {complexDetail.StartIlumination}hs";
            labelAdPorIlum.Text = $"Adicional por iluminacion: {complexDetail.AditionalIlumination}%";
            labelCBU.Text = $"CBU: {complexDetail.CBU}";
            labelServicios.Text = "Servicios:";
            labelEstadoComplejoDetail.Text = $"Estado: {complexDetail.State}";

            var serviciosComplejo = complexDetail.Services;
            if(serviciosComplejo != null)
            {
                _servicios = new BindingList<ServiceResponseDTO>(serviciosComplejo.ToList());
                dgvServiciosComplejoDetail.DataSource = _servicios;

                dgvServiciosComplejoDetail.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvServiciosComplejoDetail.Columns["ServiceDescription"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            switch (complexDetail.State)
            {
                case "Pendiente":
                    labelEstadoComplejoDetail.ForeColor = Color.Orange;

                    //aca voy a designar los botones
                    buttonCambioEstado1.Show();
                    buttonCambioEstado2.Show();
                    buttonCambioEstado1.Text = "Habilitar";
                    buttonCambioEstado2.Text = "Rechazar";

                    break;
                case "Habilitado":
                    labelEstadoComplejoDetail.ForeColor = Color.Green;

                    buttonCambioEstado1.Show();
                    buttonCambioEstado2.Show();
                    buttonCambioEstado1.Text = "Deshabilitar";
                    buttonCambioEstado2.Text = "Bloquear";

                    break;
                case "Deshabilitado":
                    labelEstadoComplejoDetail.ForeColor = Color.Red;

                    buttonCambioEstado1.Show();
                    buttonCambioEstado2.Show();
                    buttonCambioEstado1.Text = "Habilitar";
                    buttonCambioEstado2.Text = "Bloquear";

                    break;
                case "Bloqueado": 
                    labelEstadoComplejoDetail.ForeColor = Color.Red;

                    buttonCambioEstado1.Show();
                    buttonCambioEstado1.Text = "Habilitar";
                    buttonCambioEstado2.Hide();

                    break;
                case "Rechazado":
                    labelEstadoComplejoDetail.ForeColor = Color.Red;

                    buttonCambioEstado1.Hide();
                    buttonCambioEstado2.Hide();

                    break;
                default: // Si ningún caso coincide
                    labelEstadoComplejoDetail.ForeColor = Color.Brown;

                    buttonCambioEstado1.Show();
                    buttonCambioEstado2.Show();
                    buttonCambioEstado1.Text = "Algo";
                    buttonCambioEstado2.Text = "salio mal";

                    break;
            }


            //TODAVIA NO TENGO EL EVENTO DE LOS BOTONES DE VER CANCHAS, VER RESERVAS Y DE LAS ACCIONES EN CUANTO AL ESTADO



        }

        private async Task LoadComplexImageAsync(string? imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
            {
                pictureBox1.Image = null;
                return;
            }

            try
            {
                //libero memoria no administrada(imagenes, streams, archivos, conexiones no las limpia en garbage collector)
                pictureBox1.Image?.Dispose();

                using var stream = await _httpClient.GetStreamAsync(imagePath);
                pictureBox1.Image = Image.FromStream(stream);
            }
            catch
            {
                pictureBox1.Image = null;
                // opcional: imagen por defecto, esto me gustaria verlo de hacer despues
            }
        }

    }
}
