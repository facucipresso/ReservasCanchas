using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Models.Complex;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms.Complex
{
    public partial class FormComplex : Form
    {
        private readonly ComplexService _complexService;

        //aca deberia recibir la lista de los complexForAdmin creeria
        public FormComplex()
        {
            InitializeComponent();
            _complexService = new ComplexService();
            this.WindowState = FormWindowState.Maximized;
            labelTitle.Text = "Complejos registrados";

        }

        // le tuve que agregar el async
        private async  void FormComplex_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadComplexesAsync();
            }
            catch (Exception ex)
            {
                DialogService.ShowError(Form.ActiveForm ?? this.TopLevelControl as Form, ex.Message);
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadComplexesAsync()
        {
            //no entiendo que hace aca
            flowLayoutPanelComplexes.Controls.Clear();

            var complexes = await _complexService.GetAllComplexesAsync(); // tu servicio

            if (complexes == null || complexes.Count == 0)
            {
                // no me parece profesional poner un label, capaz un menssage box o algo mas profesional
                var lbl = new Label { Text = "No hay complejos para mostrar.", AutoSize = true, Padding = new Padding(10) };
                flowLayoutPanelComplexes.Controls.Add(lbl);
                return;
            }

            foreach (var dto in complexes)
            {
                var card = new ComplexCard();
                card.SetData(dto); // tu método para llenar los labels
                card.EnterClicked += ComplexCard_EnterClicked; // se dispara cuando hacen click en "Entrar"
                flowLayoutPanelComplexes.Controls.Add(card);
            }
        }

        private void ComplexCard_EnterClicked(int? complexId, string? nameOwwe, string? lastnameOwner) 
        {
            /* Esto todavia no va a pasar, tengo que tener terminado esto primero
            if (complexId == null) return;

            // Abrir otro formulario con detalle del complejo o pasar el id al dashboard
            // Por ejemplo:
            using (var formDetail = new FormComplexDetail(complexId.Value))
            {
                formDetail.ShowDialog();
                // si se hicieron cambios en detail, recargar:
                _ = LoadComplexesAsync();
            }
            */
        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
