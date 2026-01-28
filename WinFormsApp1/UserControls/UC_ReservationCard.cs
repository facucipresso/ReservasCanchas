using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Models.Reservation;

namespace WinFormsApp1.UserControls
{
    public partial class UC_ReservationCard : UserControl
    {
        public UC_ReservationCard()
        {
            InitializeComponent();
        }

        public void SetData(ReservationResponseDTO dto)
        {
            labelFechaReserva.Text = dto.Date.ToString("dd/MM/yyyy");
            labelHoraReserva.Text = $"{dto.InitTime.ToString(@"hh\:mm")} hs    "; //agrego ToString();
            labelNombreQuienReserva.Text = $"{dto.UserName} {dto.UserLastName}";
            labelCanchaReserva.Text = dto.FieldName;

            labelEstadoReserva.Text = $"Estado: {dto.ReservationState}";
            labelTipoPagoReserva.Text = $"Pago: {dto.PayType}";
            labelValorTotalReserva.Text = $"Total: ${dto.TotalPrice:N0}";
            labelTipoReserva.Text = $"Tipo: {dto.ReservationType}";
            labelFechaCreacionReserva.Text = $"Reserva creada: {dto.CreationDate:dd/MM/yyyy}";
        }

        public void SetCompactMode(bool compact)
        {
            labelCanchaReserva.Visible = !compact;
        }


    }
}
