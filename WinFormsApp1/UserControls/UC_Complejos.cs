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
using WinFormsApp1.Models.Complex;
using WinFormsApp1.Services;

namespace WinFormsApp1.UserControls
{
    public partial class UC_Complejos : UserControl
    {

        private readonly ComplexService _complexService;
        public UC_Complejos()
        {
            InitializeComponent();
            _complexService = new ComplexService();

            this.Load += UC_Complejos_Load;

            //LINEA AGREGADA RECIEN 19/1
            flowLayoutPanelComplexes.SizeChanged += FlowLayoutPanelComplexes_SizeChanged;
        }

        private async void UC_Complejos_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadComplexesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            AdjustCardWidths();
        }

        private void ComplexCard_EnterClicked(int? complexId, string? nameOwner, string? lastNameOwner)
        {
            //Esto todavia no va a pasar, tengo que tener terminado esto primero
            if (complexId == null || nameOwner == null || lastNameOwner == null) return;

            // Abrir otro formulario con detalle del complejo o pasar el id al dashboard
            // Por ejemplo:
            /*
            using (var formDetail = new FormComplexDetail(complexId.Value))
            {
                formDetail.ShowDialog();
                // si se hicieron cambios en detail, recargar:
                _ = LoadComplexesAsync();
            }
            */

            using (var formComplexWorkspace = new FormComplexWorkspace(complexId.Value, nameOwner, lastNameOwner))//aca tambien nombre y apellido
            {
                formComplexWorkspace.ShowDialog(); 
            }
            
        }

        // esto aca ni iria porque no tengo este evento en este form, esto quedo de FormComplex
        //private void buttonVolver_Click(object sender, EventArgs e)
        //{
        //   this.Close();
        //}

        private void FlowLayoutPanelComplexes_SizeChanged(object? sender, EventArgs e)
        {
            AdjustCardWidths();
        }

        private void AdjustCardWidths()
        {
            if (flowLayoutPanelComplexes.Controls.Count == 0)
                return;

            int cardMinWidth = 350; // el ancho base que ya usás
            int spacing = 20;       // margen aproximado entre cards

            int panelWidth = flowLayoutPanelComplexes.ClientSize.Width;

            // cuántas cards entran por fila
            int cardsPerRow = Math.Max(1, panelWidth / (cardMinWidth + spacing));

            // nuevo ancho para que ocupen todo el espacio
            int newWidth = (panelWidth / cardsPerRow) - spacing;

            foreach (Control ctrl in flowLayoutPanelComplexes.Controls)
            {
                if (ctrl is ComplexCard card)
                {
                    card.Width = newWidth;
                }
            }
        }

    }
}
