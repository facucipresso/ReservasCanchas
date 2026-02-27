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
    public partial class UC_DashboardLastComplexCard : UserControl
    {
        public UC_DashboardLastComplexCard()
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
        public string ComplexState
        {
            get => lblComplexState.Text;
            set => lblComplexState.Text = value;
        }

    }
}
