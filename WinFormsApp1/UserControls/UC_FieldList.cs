using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Services;

namespace WinFormsApp1.UserControls
{
    public partial class UC_FieldList : UserControl
    {
        private readonly int _complexId;
        private readonly ComplexService _complexService;

        public event Action VolverClicked;
        public event Action CerrarClicked;

        public UC_FieldList(int complexId)
        {
            InitializeComponent();

            _complexId = complexId;
            _complexService = new ComplexService();

            this.Load += UC_FieldList_Load;

            btnVolver.Click += (s, e) => VolverClicked?.Invoke();
            btnCerrar.Click += (s, e) => CerrarClicked?.Invoke();

            flowLayoutPanelFields.WrapContents = true;
            flowLayoutPanelFields.AutoScroll = true;
            flowLayoutPanelFields.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanelFields.Padding = new Padding(0);

            flowLayoutPanelFields.SizeChanged += FlowLayoutPanelFields_SizeChanged;

        }

        private async void UC_FieldList_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadFieldsAsync();
            }
            catch (Exception ex)
            {
                DialogService.ShowError(Form.ActiveForm ?? this.TopLevelControl as Form, ex.Message);
            }
        }

        private async Task LoadFieldsAsync()
        {
            var fields = await _complexService.GetAllFieldsOfComplexAsync(_complexId);

            if (fields == null || !fields.Any())
            {
                ShowEmptyState();
                return;
            }

            ShowGrid();

            flowLayoutPanelFields.Controls.Clear();

            foreach (var field in fields)
            {
                var card = new UC_FieldCard(field);
                card.VerReservasCanchaClicked += Card_VerReservasCanchaClicked;
                flowLayoutPanelFields.Controls.Add(card);
            }
            AjustarDistribucionCards();
        }

        private void FlowLayoutPanelFields_SizeChanged(object sender, EventArgs e)
        {
            AjustarDistribucionCards();
        }

        private void AjustarDistribucionCards()
        {
            if (flowLayoutPanelFields.Controls.Count == 0)
                return;

            int cardWidth = 354; 
            int availableWidth = flowLayoutPanelFields.ClientSize.Width;

            // Cuántas cards entran por fila
            int cardsPerRow = Math.Max(1, availableWidth / cardWidth);

            // Espacio total sobrante
            int espacioRestante = availableWidth - (cardsPerRow * cardWidth);

            // Cantidad de espacios = cards + 1
            int cantidadEspacios = cardsPerRow + 1;

            int margenHorizontal = espacioRestante / cantidadEspacios;

            foreach (Control control in flowLayoutPanelFields.Controls)
            {
                control.Margin = new Padding(margenHorizontal, 20, 0, 20);
            }
        }

        public event EventHandler<int> VerReservasCanchaClicked;

        private void Card_VerReservasCanchaClicked(object sender, int fieldId)
        {
            VerReservasCanchaClicked?.Invoke(this, fieldId);
        }

        private void ShowEmptyState()
        {
            flowLayoutPanelFields.Visible = false;
            labelFieldsComplexEmpty.Visible = true;
        }
        private void ShowGrid()
        {
            flowLayoutPanelFields.Visible = true;
            labelFieldsComplexEmpty.Visible = false;
        }

    }
}
