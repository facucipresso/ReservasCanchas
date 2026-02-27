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
    public partial class UC_DasboardComplexCard : UserControl
    {
        public UC_DasboardComplexCard()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        [Category("Dashboard")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ComplexName
        {
            get => lblComplexName.Text;
            set => lblComplexName.Text = value;
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
        public string Rating
        {
            get => lblRating.Text;
            set => lblRating.Text = value;
        }
    }
}
