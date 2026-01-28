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
using WinFormsApp1.UserControls;

namespace WinFormsApp1.Forms
{
    public partial class FormPrincipal : Form
    {

        private readonly ComplexService _complexService;
        private UC_Notificaciones? uc_Notificaciones;
        public FormPrincipal()
        {
            InitializeComponent();
            _complexService = new ComplexService();
            UC_Dashboard uc = new UC_Dashboard();
            addUserControl(uc);
        }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void buttomDasboard1_Click(object sender, EventArgs e)
        {
            UC_Dashboard uc = new UC_Dashboard();
            addUserControl(uc);
        }

        private void buttomComplejos1_Click(object sender, EventArgs e)
        {
            UC_Complejos uc = new UC_Complejos();
            addUserControl(uc);
        }

        private void buttomServisios1_Click(object sender, EventArgs e)
        {
            UC_Servisios uc = new UC_Servisios();
            addUserControl(uc);
        }

        private void buttomNotificaciones1_Click(object sender, EventArgs e)
        {
            uc_Notificaciones = new UC_Notificaciones();
            uc_Notificaciones.IngresarComplejoClicked += AbrirComplejoDesdeNotificacion;
            addUserControl(uc_Notificaciones);
        }

        private void AbrirComplejoDesdeNotificacion(int complexId, string nameOwner, string lastNameOwner) //aca que tome tambien nombre y apellido del dueño
        {
            var form = new FormComplexWorkspace(complexId, nameOwner, lastNameOwner); //que tome nombre y apellido del dueño

            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(
                this.Location.X + (this.Width - form.Width) / 2,
                this.Location.Y + (this.Height - form.Height) / 2
            );

            form.FormClosed += async (s, e) =>
            {
                if (uc_Notificaciones != null)
                    await uc_Notificaciones.RefreshNotificationsAsync();
            };

            form.Show(this);
        }


        private void buttomUsuarios1_Click(object sender, EventArgs e)
        {
            UC_Usuarios uc = new UC_Usuarios();
            addUserControl(uc);
        }

        private void buttomCerrarSesion1_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "¿Está seguro que desea cerrar sesión?",
                "Cerrar sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes) return;

            // limpio todos los datos de la sesion
            var authService = new AuthService();
            authService.Logout();

            // muestro el login de nuevo
            FormLogin login = new FormLogin();
            login.Show();

            // cierro este formulario
            this.Close();

        }
    }
}
