using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Forms.Complex;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms
{
    public partial class FormDashboard : Form
    {
        string _username;

        private readonly ComplexService _complexService;
        private readonly ServicesService _servicesService;
        public FormDashboard(string username)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            _username = username;
            _complexService = new ComplexService();
            _servicesService = new ServicesService();
            labelDashboard.Text = "Hola " + _username + " Bienvenido!!";
        }

        private async void buttonComplex_Click(object sender, EventArgs e)
        {

            try
            {
                using (var form = new FormComplex())
                {
                    form.ShowDialog(); // o Show() según prefieras
                }
            }
            catch (Exception ex)
            {
                DialogService.ShowError(Form.ActiveForm ?? this.TopLevelControl as Form, ex.Message);
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            /*
            try
            {
                var complexes = await _complexService.GetAllComplexesAsync();

                MessageBox.Show("Los complejos llegaron bien!!!");
                // Ver como hago para mostrarlo en un data grid view
                // dgvComplejos.DataSource = complejos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
        }

        private async  void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var services = await _servicesService.GetAllServicesAsync();

                labelDashboardErrorComplex.Visible = true;
                labelDashboardErrorComplex.Text = "La data de los servicios llego bien";

                FormService2 formService2 = new FormService2(services);
                formService2.ShowDialog();
                //FormServices formServices = new FormServices(services);
                //formServices.ShowDialog();

                // Ver como hago para mostrarlo en un data grid view
                // dgvComplejos.DataSource = complejos;
            }
            catch (Exception ex)
            {
                DialogService.ShowError(Form.ActiveForm ?? this.TopLevelControl as Form, ex.Message);
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
