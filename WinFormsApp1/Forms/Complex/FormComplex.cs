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

        public FormComplex()
        {
            InitializeComponent();
            _complexService = new ComplexService();
            this.WindowState = FormWindowState.Maximized;
            labelTitle.Text = "Complejos registrados";

        }

        private async  void FormComplex_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadComplexesAsync();
            }
            catch (Exception ex)
            {
                DialogService.ShowError(Form.ActiveForm ?? this.TopLevelControl as Form, ex.Message);
            }
        }

        private async Task LoadComplexesAsync()
        {
            flowLayoutPanelComplexes.Controls.Clear();

            var complexes = await _complexService.GetAllComplexesAsync(); 

            if (complexes == null || complexes.Count == 0)
            {
                var lbl = new Label { Text = "No hay complejos para mostrar.", AutoSize = true, Padding = new Padding(10) };
                flowLayoutPanelComplexes.Controls.Add(lbl);
                return;
            }

            foreach (var dto in complexes)
            {
                var card = new ComplexCard();
                card.SetData(dto); 
                card.EnterClicked += ComplexCard_EnterClicked;
                flowLayoutPanelComplexes.Controls.Add(card);
            }
        }

        private void ComplexCard_EnterClicked(int? complexId, string? nameOwwe, string? lastnameOwner) 
        {

        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
