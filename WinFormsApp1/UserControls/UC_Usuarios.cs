using Microsoft.VisualBasic.Logging;
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
using WinFormsApp1.Models.User;
using WinFormsApp1.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WinFormsApp1.UserControls
{
    public partial class UC_Usuarios : UserControl
    {

        private readonly UserService _userService;
        private BindingList<UserResponseWithRoleDTO> _usuarios;
        public UC_Usuarios()
        {
            InitializeComponent();
            _userService = new UserService();

            this.Load += UC_Users_Load;

            dgvUsuarios.CellFormatting += dgvUsuarios_CellFormatting;
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
                _usuarios = new BindingList<UserResponseWithRoleDTO>(list);
                dgvUsuarios.DataSource = _usuarios;

                //que ocupe el espacio de id nomas
                dgvUsuarios.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                //cambio los nombres de las columnas autogeneradas
                dgvUsuarios.Columns["UserName"].HeaderText = "Usuario";
                dgvUsuarios.Columns["Phone"].HeaderText = "Telefono";
                dgvUsuarios.Columns["UserState"].HeaderText = "Estado";
                dgvUsuarios.Columns["Role"].HeaderText = "Rol";

                //ColumnAcciones
                var totalColumnas = dgvUsuarios.Columns.Count;
                dgvUsuarios.Columns["ColumnAcciones"].DisplayIndex = totalColumnas - 1;





            }
            catch (Exception ex)
            {
                DialogService.ShowError(Form.ActiveForm ?? this.TopLevelControl as Form, ex.Message);
                //MessageBox.Show("Error ocurrido cargando los usuarios por: " + ex.Message);
            }
        }

        private async void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Verificar que el clic no sea en el encabezado (fila -1)
            if (e.RowIndex < 0) return;

            // 2. Verificar si el clic fue en la columna de la imagen por su Nombre o Índice
            if (dgvUsuarios.Columns[e.ColumnIndex].Name == "ColumnAcciones")
            {
                var idUser = Convert.ToInt32(dgvUsuarios.Rows[e.RowIndex].Cells["Id"].Value);

                var user = dgvUsuarios.Rows[e.RowIndex].DataBoundItem as UserResponseWithRoleDTO;
                if (user == null) return;


                if (user.UserState == UserState.Bloqueado)
                {
                    // opcional: tooltip o mensaje suave
                    // MessageBox.Show("El usuario ya se encuentra bloqueado");

                    DialogResult result1 = MessageBox.Show($"¿Esta seguro que quiere desbloquear al usuario?",
                                    "Confirmar", MessageBoxButtons.YesNo);

                    if (result1 == DialogResult.Yes)
                    {
                        // Ejecuto bloqueo de user
                        await _userService.UnBlockUserByIdAsync(idUser);

                        // refresco el dgv
                        await LoadUsersAsync();
                        Notifier.Show(this.FindForm(), "Usuario desbloqueado correctamente", NotificationType.Success);
                    }

                    return;
                }

                DialogResult result = MessageBox.Show($"¿Esta seguro que quiere bloquear al usuario?",
                                                    "Confirmar", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Ejecuto bloqueo de user
                    await _userService.BlockUserByIdAsync(idUser);

                    // refresco el dgv
                    await LoadUsersAsync();
                    Notifier.Show(this.FindForm(), "Usuario bloqueado correctamente", NotificationType.Success);
                }
            }

            // VER COMO HACER PARA QUE UNA VEZ BLOQUEADO EL USUARIO NO PUEDA BLOQUEARLO DE NUEVO YA ESTANDO BLOQUEADO
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
