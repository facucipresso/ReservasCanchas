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
    public partial class UC_DashboardCard : UserControl
    {
        public UC_DashboardCard()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        [Category("Dashboard")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Title
        {
            get => lblTitle.Text;
            set => lblTitle.Text = value;
        }

        [Browsable(true)]
        [Category("Dashboard")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string DisplayValue
        {
            get => lbValue.Text;
            set => lbValue.Text = value;
        }

        [Browsable(true)]
        [Category("Dashboard")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image Icon
        {
            get => pictureIcon.Image;
            set => pictureIcon.Image = value;
        }
    }
}
