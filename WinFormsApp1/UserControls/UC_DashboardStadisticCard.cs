using Guna.UI2.WinForms;
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
    public partial class UC_DashboardStadisticCard : UserControl
    {
        [Browsable(true)]
        [Category("Dashboard")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image Icon
        {
            get => pictureBoxIcon.Image;
            set => pictureBoxIcon.Image = value;
        }
        [Browsable(true)]
        [Category("Dashboard")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Value
        {
            get => labelValue.Text;
            set => labelValue.Text = value;
        }
        [Browsable(true)]
        [Category("Dashboard")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Title
        {
            get => labelTitle.Text;
            set => labelTitle.Text = value;
        }
        public UC_DashboardStadisticCard()
        {
            InitializeComponent();
        }
    }
}
