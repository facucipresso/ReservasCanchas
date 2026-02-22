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
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms.Complex
{
    public partial class FormReasonModal : Form
    {
        public string ReasonText => textBoxReasonModal.Text.Trim();

        public FormReasonModal(string actionName)
        {
            InitializeComponent();

            if (string.IsNullOrWhiteSpace(actionName))
                actionName = "realizar esta acción";

            lblReasonModal.Text = $"Ingrese la razón para {actionName}:";
        }

        private void buttonConfirmar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxReasonModal.Text))
            {
                Notifier.Show(this.FindForm(), "Debe ingresar una razon", NotificationType.Warning);
                //MessageBox.Show("Debe ingresar una razón.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
