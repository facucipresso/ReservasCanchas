using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Models.Field;

namespace WinFormsApp1.UserControls
{
    public partial class UC_FieldCard : UserControl
    {
        private readonly FieldDetailResponseDTO _field;
        private readonly int _fieldId;

        public event EventHandler<int> VerReservasCanchaClicked;


        public UC_FieldCard(FieldDetailResponseDTO field)
        {
            InitializeComponent();

            _field = field;
            _fieldId = field.Id;
            LoadData();
        }

        private void LoadData()
        {
            labelNombreCancha.Text = _field.Name;
            if (_field.FieldType == "Cancha5")
            {
                labelTipoCancha.Text = "Futbol: 5";
            }
            else if (_field.FieldType == "Cancha7")
            {
                labelTipoCancha.Text = "Futbol: 7";
            }
            else
            {
                labelTipoCancha.Text = "Futbol: 11";
            }

            labelTipoSuelo.Text = _field.FloorType == "CespedNatural" ? $"Suelo: NATURAL" : $"Suelo: SINTETICO";
            labelPrecioXHora.Text = $"Precio por hora: {_field.HourPrice}";
            labelIluminacionBool.Text = _field.Illumination == true ? $"Iluminacion: SI" : $"Iluminacion: NO";
            labelCanchaCubiertaBool.Text = _field.Covered == true ? $"Cancha cubierta: SI" : $"Cancha cubierta: NO";
            labelEstadoCancha.Text = _field.Active == true ? $"Estado: ACTIVA" : $"ESTADO: NO ACTIVA";
        }


        private void buttonVerReservasDeUnaCancha_Click(object sender, EventArgs e)
        {
            VerReservasCanchaClicked?.Invoke(this, _fieldId);
        }
    

    }
}

