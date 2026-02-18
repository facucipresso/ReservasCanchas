using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Enum;
using WinFormsApp1.Models.Notification;
using WinFormsApp1.Services;

namespace WinFormsApp1.UserControls
{
    public partial class UC_Notificaciones : UserControl
    {

        private readonly NotificationService _notificationService;
        private readonly UserService _userService;
        private BindingList<NotificacionResponseDTO> _notifications;

        //EVENTO PARA MOSTRAR EL COMPLEJO
        public event Action<int, string, string> IngresarComplejoClicked;
        public UC_Notificaciones()
        {
            InitializeComponent();
            _notificationService = new NotificationService();
            _userService = new UserService();
            this.Load += UC_Notifications_Load;

            // dgvUsuarios.CellFormatting += dgvUsuarios_CellFormatting;
            dgvNotificaciones.CellFormatting += dgvNotificaciones_CellFormatting;
        }

        private async void UC_Notifications_Load(object sender, EventArgs e)
        {
            await LoadNotificationsAsync();
        }

        private async Task LoadNotificationsAsync()
        {
            try
            {
                var list = await _notificationService.GetAllNotificationsAsync();
                _notifications = new BindingList<NotificacionResponseDTO>(list);
                dgvNotificaciones.DataSource = _notifications;

                //OCULTO COLUMNAS QUE NO QUIERO QUE SE VEAN
                dgvNotificaciones.Columns["Id"].Visible = false;
                dgvNotificaciones.Columns["UserId"].Visible = false;
                dgvNotificaciones.Columns["IsRead"].Visible = false;
                dgvNotificaciones.Columns["ReservationId"].Visible = false;
                dgvNotificaciones.Columns["ComplexId"].Visible = false;

                dgvNotificaciones.Columns["Title"].HeaderText = "Titulo";
                dgvNotificaciones.Columns["Message"].HeaderText = "Mensaje";
                dgvNotificaciones.Columns["CreatedAt"].HeaderText = "Fecha";

                //DECLARO LAS POSICIONES DE LOS DOS BOTONES
                var totalColumnas = dgvNotificaciones.Columns.Count;
                dgvNotificaciones.Columns["ColumnButtonMarkAsReaded"].DisplayIndex = totalColumnas - 2;
                dgvNotificaciones.Columns["ColumnIngresarNotificacion"].DisplayIndex = totalColumnas - 1;

                dgvNotificaciones.Columns["ColumnButtonMarkAsReaded"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvNotificaciones.Columns["ColumnIngresarNotificacion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;




            }
            catch (Exception ex)
            {
                DialogService.ShowError(Form.ActiveForm ?? this.TopLevelControl as Form, ex.Message);
                //MessageBox.Show("Error ocurrido cargando las notificaciones por: " + ex.Message);
            }
        }

        private async void dgvNotificaciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Verificar que el clic no sea en el encabezado (fila -1)
            if (e.RowIndex < 0) return;



            // 2. Verificar si el clic fue en la columna de la imagen por su Nombre o Índice
            if (dgvNotificaciones.Columns[e.ColumnIndex].Name == "ColumnButtonMarkAsReaded")
            {
                var idNotification = Convert.ToInt32(dgvNotificaciones.Rows[e.RowIndex].Cells["Id"].Value);

                
                var notificacion = dgvNotificaciones.Rows[e.RowIndex].DataBoundItem as NotificacionResponseDTO;
                if (notificacion.IsRead == true)
                {
                    // opcional: tooltip o mensaje suave
                    Notifier.Show(this.FindForm(), "La notificacion ya esta marcada como leida", NotificationType.Warning);
                    // MessageBox.Show("La notificacion ya esta marcada como leida");
                    return;
                }
                


                DialogResult result = MessageBox.Show($"¿Deseas marcar como leida la notificacion?",
                                                    "Confirmar", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Ejecuto eliminación
                    // ME DA ERROR NO SE PORQUE
                    await _notificationService.MarkAsReadAsync(idNotification);

                    // refresco el dgv
                    //await LoadNotificationsAsync();

                    // ACTUALIZO EL OBJETO EN MEMORIA
                    notificacion.IsRead = true;

                    // FUERZO REDIBUJADO
                    dgvNotificaciones.Refresh();
                }
            }

            if (dgvNotificaciones.Columns[e.ColumnIndex].Name == "ColumnIngresarNotificacion")
            {
                var notificacion = dgvNotificaciones.Rows[e.RowIndex].DataBoundItem as NotificacionResponseDTO;
                if (notificacion == null) return;

                var complexId = Convert.ToInt32(dgvNotificaciones.Rows[e.RowIndex].Cells["ComplexId"].Value);

                var ownerComplex = await _userService.GetComplxOwnerByIdAsync(complexId);

                IngresarComplejoClicked?.Invoke(complexId, ownerComplex.Name, ownerComplex.LastName); //aca le tengo que agregar nombre y apellido

                // si todavía no está leída la marco como leída REAL
                if (!notificacion.IsRead)
                {
                    // le pego a la api
                    await _notificationService.MarkAsReadAsync(notificacion.Id);

                    // actualizo en memoria
                    notificacion.IsRead = true;

                    // redibujo
                    dgvNotificaciones.Refresh();
                }
            }

        }

        private void dgvNotificaciones_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var notificacion = dgvNotificaciones.Rows[e.RowIndex].DataBoundItem as NotificacionResponseDTO;
            if (notificacion == null) return;

            if (notificacion.IsRead == true)
            {
                dgvNotificaciones.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                    Color.FromArgb(240, 240, 240);

                dgvNotificaciones.Rows[e.RowIndex].DefaultCellStyle.ForeColor =
                    Color.Gray;
            }

            
            if (dgvNotificaciones.Columns[e.ColumnIndex].Name == "ColumnButtonMarkAsReaded")
            {
                if (notificacion.IsRead == true)
                {
                    //ACA VA LA IMAGEN QUE ES COMO UN CHECK VERDE
                    e.Value = Properties.Resources.icono_mensaje_leido;
                }
                else
                {
                    //ACA VA LA IMAGEN QUE TENGO QUE DESCARGAR MAÑANA 19/1 DEL MENSAJE https://es.pngtree.com/freepng/green-message-bubble-with-ellipsis-illustration-vector_23695158.html
                    e.Value = Properties.Resources.icono_mensaje_noLeido;
                }
            }
            

        }

        public async Task RefreshNotificationsAsync()
        {
            await LoadNotificationsAsync();
        }

    }
}
