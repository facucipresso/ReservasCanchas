using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Models.Service;
using WinFormsApp1.Services;

namespace WinFormsApp1.UserControls
{
    public partial class UC_Servisios : UserControl
    {
        private readonly ServicesService _serService;
        private BindingList<ServiceResponseDTO> _services;

        private bool editar = false;
        private string idSeleccionado = null;
        public UC_Servisios()
        {
            InitializeComponent();
            _serService = new ServicesService();
            this.Load += UC_Servisios_Load;
        }

        private async void UC_Servisios_Load(object sender, EventArgs e)
        {
            await LoadServicesAsync();
        }

        private async Task LoadServicesAsync()
        {
            var list = await _serService.GetAllServicesAsync();

            _services = new BindingList<ServiceResponseDTO>(list);
            dgvServicios.DataSource = _services;

            dgvServicios.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvServicios.Columns["ServiceDescription"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvServicios.Columns["ServiceDescription"].HeaderText = "Descripcion del Servicio";
        }

        private async void buttonAgregar_Click(object sender, EventArgs e)
        {
            //CUANDO CREO UN SERVICIO
            if (editar == false)
            {
                try
                {
                    ServiceUpdateDTO serviceUpdateDTO = new ServiceUpdateDTO();
                    serviceUpdateDTO.ServiceDescription = textServiceDescription.Text;
                    await _serService.CreateServiceAsync(serviceUpdateDTO);
                    MessageBox.Show("Se inserto correctamente el nuevo servicio");
                    await LoadServicesAsync();
                    textServiceDescription.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo insertar correctamente el nuevo servicio por: " + ex);
                }
            }
            // CUANDO EDITO UN SERVICIO
            if (editar == true)
            {
                try
                {
                    ServiceUpdateDTO serviceUpdateDTO = new ServiceUpdateDTO();
                    serviceUpdateDTO.ServiceDescription = textServiceDescription.Text;
                    await _serService.UpdateServiceByIdAsync(Convert.ToInt32(idSeleccionado), serviceUpdateDTO);
                    MessageBox.Show("Se edito correctamente el servicio");
                    await LoadServicesAsync();
                    CancelarEdicion();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo editar correctamente el servicio por: " + ex);
                }

            }


        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {

            if (dgvServicios.SelectedRows.Count > 0)
            {
                editar = true;
                if (editar == true)
                {
                    labelAgregarEditarServicio.Text = "Editar Servicio";
                    buttonCancelar.Visible = true;
                }

                textServiceDescription.Text = dgvServicios.CurrentRow.Cells["ServiceDescription"].Value.ToString();
                idSeleccionado = dgvServicios.CurrentRow.Cells["Id"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila para editar");
            }
        }

        private async void buttonEliminar_Click(object sender, EventArgs e)
        {
            if (dgvServicios.SelectedRows.Count > 0)
            {
                var service = dgvServicios.CurrentRow.Cells["ServiceDescription"].Value.ToString();
                idSeleccionado = dgvServicios.CurrentRow.Cells["Id"].Value.ToString();

                var resp = MessageBox.Show($"¿Borrar servicio '{service}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resp != DialogResult.Yes) return;

                var deleted = await _serService.DeleteServiceByIdAsync(Convert.ToInt32(idSeleccionado));

                if (deleted == false)
                {
                    MessageBox.Show("No se pudo borrar el servicio.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Se elimino correctamente el servicio");

                await LoadServicesAsync();
                //bindingSource1.Remove(servicio);  Esto actualiza el grid inmediatamente
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila para eliminar");
            }
        }

        private void CancelarEdicion()
        {
            editar = false;
            idSeleccionado = null;

            textServiceDescription.Clear();
            labelAgregarEditarServicio.Text = "Agregar Servicio";

            buttonCancelar.Visible = false;
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            CancelarEdicion();
        }
    }
}
