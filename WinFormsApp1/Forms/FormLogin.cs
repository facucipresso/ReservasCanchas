using System.Threading.Tasks;
using WinFormsApp1.Forms;
using WinFormsApp1.Models.Auth;
using WinFormsApp1.Security;
using WinFormsApp1.Services;

namespace WinFormsApp1
{
    public partial class FormLogin : Form
    {
        private readonly AuthService _authService = new AuthService();
        public FormLogin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }




        private async void buttonIngresar_Click(object sender, EventArgs e)
        {
            try
            {

                var response = await _authService.LoginAsync(textBoxUserName.Text, textBoxPassword.Text);

                if (response != null)
                {
                    // Guardo token en Session
                    Session.SetSession(response);

                    labelMsjBienvenida.Text = "Hola " + response.UserName + " Bienvenido!!";

                    FormPrincipal formPrincipal = new FormPrincipal(response.UserName);
                    formPrincipal.Show();

                    this.Hide();

                }
                else
                {
                    labelMsjBienvenida.Text = "Algo salio mal";
                }
            }
            catch (Exception ex)
            {
                DialogService.ShowError(Form.ActiveForm ?? this.TopLevelControl as Form, ex.Message);

            }
        }

        private void textBoxUserName_Enter(object sender, EventArgs e)
        {
            if (textBoxUserName.Text == "USUARIO")
            {
                textBoxUserName.Text = "";
                textBoxUserName.ForeColor = Color.LightGray;
            }
        }

        private void textBoxUserName_Leave(object sender, EventArgs e)
        {
            if (textBoxUserName.Text == "")
            {
                textBoxUserName.Text = "USUARIO";
                textBoxUserName.ForeColor = Color.DimGray;
            }
        }

        private void textBoxPassword_Enter(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "CONTRASEÑA")
            {
                textBoxPassword.Text = "";
                textBoxPassword.ForeColor = Color.LightGray;
                textBoxPassword.UseSystemPasswordChar = true;
            }
        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "")
            {
                textBoxPassword.Text = "CONTRASEÑA";
                textBoxPassword.ForeColor = Color.DimGray;
                textBoxPassword.UseSystemPasswordChar = false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
