using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Forms.Service;
using WinFormsApp1.Models.Service;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms
{
    public partial class FormService2 : Form
    {
        //private BindingSource _bs = new BindingSource();
        private readonly ServicesService _serService;

        public FormService2(List<ServiceResponseDTO> services)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            dataGridViewServicios.AutoGenerateColumns = false;
            dataGridViewServicios.Dock = DockStyle.Fill;
            bindingSource1.DataSource = services;
            dataGridViewServicios.DataSource = bindingSource1;
            _serService = new ServicesService();

        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private async void dataGridViewServicios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // ignorar encabezado

            // Si clic en columna Editar
            if (dataGridViewServicios.Columns[e.ColumnIndex].Name == "ColEditar")
            {
                var servicio = (ServiceResponseDTO)dataGridViewServicios.Rows[e.RowIndex].DataBoundItem;

                var resp = MessageBox.Show($"¿Editar servicio '{servicio.ServiceDescription}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                var id = servicio.Id;
                var serviceDescription = servicio.ServiceDescription; // de aca tengo que sacar la descripcion del servicio y en otra variable necesito sacar el id del servicio

                //FormUpdateService formUpdateService = new FormUpdateService(id, serviceDescription);
                //formUpdateService.ShowDialog();

                if (resp != DialogResult.Yes) return;

                using (var formUpdateService = new FormUpdateService(id, serviceDescription))
                {
                    if (formUpdateService.ShowDialog() == DialogResult.OK)
                    {
                        // Volvemos a pedir los datos al backend para mostrar los cambios
                        await LoadServicesAsync();
                    }
                }

            }

            // Si clic en columna Borrar
            if (dataGridViewServicios.Columns[e.ColumnIndex].Name == "ColBorrar")
            {
                var servicio = (ServiceResponseDTO)dataGridViewServicios.Rows[e.RowIndex].DataBoundItem;
                var resp = MessageBox.Show($"¿Borrar servicio '{servicio.ServiceDescription}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                var id = servicio.Id; // lo voy a usar para llamar al servicio y pegarle a la api 

                if (resp != DialogResult.Yes) return;

                var deleted = await _serService.DeleteServiceByIdAsync(id);

                if (deleted == false)
                {
                    MessageBox.Show("No se pudo borrar el servicio.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bindingSource1.Remove(servicio); // Esto actualiza el grid inmediatamente

            }

        }

        private async Task LoadServicesAsync()
        {
            var list = await _serService.GetAllServicesAsync();
            bindingSource1.DataSource = list;
            dataGridViewServicios.DataSource = bindingSource1;
        }

        private async void buttonAgregarServicio_Click(object sender, EventArgs e)
        {
            using (var formCreateService = new FormCreateService())
            {
                if (formCreateService.ShowDialog() == DialogResult.OK)
                {
                    // Volvemos a pedir los datos al backend para mostrar los cambios
                    await LoadServicesAsync();
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
