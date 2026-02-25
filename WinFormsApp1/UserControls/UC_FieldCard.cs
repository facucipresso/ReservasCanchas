using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
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

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
              ControlStyles.UserPaint |
              ControlStyles.OptimizedDoubleBuffer, true);

            this.UpdateStyles();

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

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AplicarBordesRedondeados();
        }

        private void AplicarBordesRedondeados()
        {
            int radio = 20; 

            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, radio, radio, 180, 90);
            path.AddArc(rect.Right - radio, rect.Y, radio, radio, 270, 90);
            path.AddArc(rect.Right - radio, rect.Bottom - radio, radio, radio, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radio, radio, radio, 90, 90);
            path.CloseFigure();

            this.Region = new Region(path);
        }


    }
}

