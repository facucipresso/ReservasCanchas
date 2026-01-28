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

namespace WinFormsApp1.Forms.Service
{
    public partial class FormCreateService : Form
    {
        private readonly ServicesService _service;
        public FormCreateService()
        {
            InitializeComponent();
            _service = new ServicesService();
        }

        private async void buttonCrearServicio_Click(object sender, EventArgs e)
        {
            ServiceUpdateDTO serviceUpdateDTO = new ServiceUpdateDTO
            {
                ServiceDescription = textBoxCreateService.Text,
            };

            var response = await _service.CreateServiceAsync(serviceUpdateDTO);

            if (response == null)
            {
                MessageBox.Show("Hubo un error al crear el servicio.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
