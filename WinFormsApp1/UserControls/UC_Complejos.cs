using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<ComplexSuperAdminResponseDTO> _allComplexes = new();

        public UC_Complejos()
        {
            InitializeComponent();

            _complexService = new ComplexService();

            this.Load += UC_Complejos_Load;

            comboEstado.SelectedIndexChanged += FiltrosChanged;
            comboProvincia.SelectedIndexChanged += FiltrosChanged;

            flowLayoutPanelComplexes.SizeChanged += FlowLayoutPanelComplexes_SizeChanged;

            CargarFiltros();
        }

        private async void UC_Complejos_Load(object sender, EventArgs e)
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

        private void CargarFiltros()
        {
            // Estados
            comboEstado.Items.Add("Todos");
            comboEstado.Items.Add("Pendiente");
            comboEstado.Items.Add("Habilitado");
            comboEstado.Items.Add("Deshabilitado");
            comboEstado.Items.Add("Bloqueado");
            comboEstado.Items.Add("Rechazado");
            comboEstado.SelectedIndex = 0;

            // Provincias (lista fija Argentina)
            comboProvincia.Items.Add("Todas");

            string[] provincias =
            {
                "Buenos Aires","Ciudad Autónoma de Buenos Aires","Catamarca","Chaco","Chubut","Córdoba",
                "Corrientes","Entre Ríos","Formosa","Jujuy","La Pampa",
                "La Rioja","Mendoza","Misiones","Neuquén","Río Negro",
                "Salta","San Juan","San Luis","Santa Cruz","Santa Fe",
                "Santiago del Estero","Tierra del Fuego, Antártida e Islas del Atlántico Sur","Tucumán"
            };

            comboProvincia.Items.AddRange(provincias);
            comboProvincia.SelectedIndex = 0;
        }

        private async Task LoadComplexesAsync()
        {
            flowLayoutPanelComplexes.Controls.Clear();

            var complexes = await _complexService.GetAllComplexesAsync();

            if (complexes == null || complexes.Count == 0)
            {
                var lbl = new Label
                {
                    Text = "No hay complejos para mostrar.",
                    AutoSize = true,
                    Padding = new Padding(10)
                };

                flowLayoutPanelComplexes.Controls.Add(lbl);
                return;
            }

            _allComplexes = complexes;

            MostrarCards(_allComplexes);
        }

        private void FiltrosChanged(object? sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void AplicarFiltros()
        {
            if (_allComplexes == null || !_allComplexes.Any())
                return;

            var filtrados = _allComplexes.AsEnumerable();

            string estadoSeleccionado = comboEstado.SelectedItem?.ToString() ?? "Todos";
            string provinciaSeleccionada = comboProvincia.SelectedItem?.ToString() ?? "Todas";

            if (estadoSeleccionado != "Todos")
            {
                filtrados = filtrados.Where(c => c.ComplexState == estadoSeleccionado);
            }

            if (provinciaSeleccionada != "Todas")
            {
                filtrados = filtrados.Where(c => c.Province == provinciaSeleccionada);
            }

            MostrarCards(filtrados.ToList());
        }

        private void MostrarCards(List<ComplexSuperAdminResponseDTO> lista)
        {
            flowLayoutPanelComplexes.Controls.Clear();

            if (!lista.Any())
            {
                var lbl = new Label
                {
                    Text = "No hay complejos con los filtros seleccionados.",
                    AutoSize = true,
                    Padding = new Padding(10)
                };

                flowLayoutPanelComplexes.Controls.Add(lbl);
                return;
            }

            foreach (var dto in lista)
            {
                var card = new ComplexCard();
                card.SetData(dto);
                card.EnterClicked += ComplexCard_EnterClicked;
                flowLayoutPanelComplexes.Controls.Add(card);
            }

            AdjustCardWidths();
        }

        private async void ComplexCard_EnterClicked(int? complexId, string? nameOwner, string? lastNameOwner)
        {
            if (complexId == null || nameOwner == null || lastNameOwner == null)
                return;

            using (var formComplexWorkspace = new FormComplexWorkspace(complexId.Value, nameOwner, lastNameOwner))
            {
                formComplexWorkspace.ShowDialog();

                if (formComplexWorkspace.ComplexWasModified)
                {
                    await LoadComplexesAsync();
                }
            }
        }

        private void FlowLayoutPanelComplexes_SizeChanged(object? sender, EventArgs e)
        {
            AdjustCardWidths();
        }

        private void AdjustCardWidths()
        {
            if (flowLayoutPanelComplexes.Controls.Count == 0)
                return;

            int cardMinWidth = 350;
            int spacing = 20;

            int panelWidth = flowLayoutPanelComplexes.ClientSize.Width;

            int cardsPerRow = Math.Max(1, panelWidth / (cardMinWidth + spacing));

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