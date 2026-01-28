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
    public partial class FormUpdateService : Form
    {
        int _id;
        public string _serviceDescription;
        private readonly ServicesService _servicesService;
        public FormUpdateService(int id, string serviceDescription)
        {
            InitializeComponent();
            textBoxUpdateDescriptionService.Text = serviceDescription;
            _servicesService = new ServicesService();
            _id = id;
        }

        private async void buttonGuardarCambios_Click(object sender, EventArgs e)
        {
            //var newDescription = textBoxUpdateDescriptionService.Text;
            var nDescription = new ServiceUpdateDTO
            {
                ServiceDescription = textBoxUpdateDescriptionService.Text,
            };

            var result = await _servicesService.UpdateServiceByIdAsync(_id, nDescription);

            if (result == null)
            {
                MessageBox.Show("Hubo un error al actualizar el servicio.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
