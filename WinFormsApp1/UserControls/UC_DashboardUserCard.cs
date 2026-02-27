using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1.UserControls
{
    public partial class UC_DashboardUserCard : UserControl
    {
        public UC_DashboardUserCard()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        [Category("Dashboard")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string UserName
        {
            get => lblUserName.Text;
            set => lblUserName.Text = value;
        }
        [Browsable(true)]
        [Category("Dashboard")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string UserState
        {
            get => lblUserRol.Text;
            set => lblUserRol.Text = value;
        }

    }
}
