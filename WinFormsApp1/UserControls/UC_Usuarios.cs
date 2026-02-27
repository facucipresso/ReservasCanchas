using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Enum;
using WinFormsApp1.Models.User;
using WinFormsApp1.Services;

namespace WinFormsApp1.UserControls
{
    public partial class UC_Usuarios : UserControl
    {
        private readonly UserService _userService;

        // lista original sin filtrar
        private List<UserResponseWithRoleDTO> _usuariosOriginal;

        public UC_Usuarios()
        {
            InitializeComponent();

            _userService = new UserService();

            this.Load += UC_Users_Load;

            dgvUsuarios.CellFormatting += dgvUsuarios_CellFormatting;
            dgvUsuarios.CellContentClick += dgvUsuarios_CellContentClick;

            comboBoxRoles.SelectedIndexChanged += FiltrosChanged;
            comboBoxInicialApellido.SelectedIndexChanged += FiltrosChanged;

            CargarFiltros();
        }

        private async void UC_Users_Load(object sender, EventArgs e)
        {
            await LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                var list = await _userService.GetAllUsersWithRoleAsync();

                _usuariosOriginal = list;

                dgvUsuarios.DataSource =
                    new BindingList<UserResponseWithRoleDTO>(_usuariosOriginal);

                ConfigurarColumnas();
            }
            catch (Exception ex)
            {
                DialogService.ShowError(Form.ActiveForm ?? this.TopLevelControl as Form, ex.Message);
            }
        }

        private void ConfigurarColumnas()
        {
            if (dgvUsuarios.Columns["Id"] == null) return;

            dgvUsuarios.Columns["Id"].Visible = false;

            dgvUsuarios.Columns["FullName"].HeaderText = "Nombre";
            dgvUsuarios.Columns["UserName"].HeaderText = "Usuario";
            dgvUsuarios.Columns["Phone"].HeaderText = "Telefono";
            dgvUsuarios.Columns["UserState"].HeaderText = "Estado";
            dgvUsuarios.Columns["Role"].HeaderText = "Rol";

            var totalColumnas = dgvUsuarios.Columns.Count;
            dgvUsuarios.Columns["ColumnAcciones"].DisplayIndex = totalColumnas - 1;
        }

        private void CargarFiltros()
        {
            comboBoxRoles.Items.Clear();
            comboBoxRoles.Items.Add("Todos");
            comboBoxRoles.Items.Add("Usuario");
            comboBoxRoles.Items.Add("SuperAdmin");
            comboBoxRoles.Items.Add("AdminComplejo");
            comboBoxRoles.SelectedIndex = 0;

            comboBoxInicialApellido.Items.Clear();
            comboBoxInicialApellido.Items.Add("Todos");

            for (char c = 'A'; c <= 'Z'; c++)
                comboBoxInicialApellido.Items.Add(c.ToString());

            comboBoxInicialApellido.SelectedIndex = 0;
        }

        private void FiltrosChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void AplicarFiltros()
        {
            if (_usuariosOriginal == null) return;

            var query = _usuariosOriginal.AsQueryable();

            // FILTRO POR ROL
            var rolSeleccionado = comboBoxRoles.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(rolSeleccionado) && rolSeleccionado != "Todos")
            {
                query = query.Where(u => u.Role.ToString() == rolSeleccionado);
            }

            // FILTRO POR INICIAL DEL APELL
            var inicialSeleccionada = comboBoxInicialApellido.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(inicialSeleccionada) && inicialSeleccionada != "Todos")
            {
                query = query.Where(u =>
                    !string.IsNullOrEmpty(u.FullName) &&
                    u.FullName.StartsWith(inicialSeleccionada, StringComparison.OrdinalIgnoreCase));
            }

            dgvUsuarios.DataSource =
                new BindingList<UserResponseWithRoleDTO>(query.ToList());

            ConfigurarColumnas();
        }

        private async void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvUsuarios.Columns[e.ColumnIndex].Name == "ColumnAcciones")
            {
                var idUser = Convert.ToInt32(dgvUsuarios.Rows[e.RowIndex].Cells["Id"].Value);

                if (idUser == 1)
                {
                    Notifier.Show(this.FindForm(), "No se le pueden realizar acciones al super admin", NotificationType.Error);
                    return;
                }

                var user = dgvUsuarios.Rows[e.RowIndex].DataBoundItem as UserResponseWithRoleDTO;
                if (user == null) return;

                if (user.UserState == UserState.Bloqueado)
                {
                    DialogResult result1 = MessageBox.Show(
                        "¿Esta seguro que quiere desbloquear al usuario?",
                        "Confirmar",
                        MessageBoxButtons.YesNo);

                    if (result1 == DialogResult.Yes)
                    {
                        await _userService.UnBlockUserByIdAsync(idUser);
                        await LoadUsersAsync();
                        Notifier.Show(this.FindForm(), "Usuario desbloqueado correctamente", NotificationType.Success);
                    }

                    return;
                }

                DialogResult result = MessageBox.Show(
                    "¿Esta seguro que quiere bloquear al usuario?",
                    "Confirmar",
                    MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    await _userService.BlockUserByIdAsync(idUser);
                    await LoadUsersAsync();
                    Notifier.Show(this.FindForm(), "Usuario bloqueado correctamente", NotificationType.Success);
                }
            }
        }

        private void dgvUsuarios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var user = dgvUsuarios.Rows[e.RowIndex].DataBoundItem as UserResponseWithRoleDTO;
            if (user == null) return;

            if (user.UserState == UserState.Bloqueado)
            {
                dgvUsuarios.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                    Color.FromArgb(255, 235, 238);

                dgvUsuarios.Rows[e.RowIndex].DefaultCellStyle.ForeColor =
                    Color.DarkRed;
            }

            if (dgvUsuarios.Columns[e.ColumnIndex].Name == "ColumnAcciones")
            {
                if (user.UserState == UserState.Bloqueado)
                {
                    e.Value = Properties.Resources.icono_usuario_bloqueado;
                }
                else
                {
                    e.Value = Properties.Resources.icono_bloquear_usuario;
                }
            }
        }
    }
}

