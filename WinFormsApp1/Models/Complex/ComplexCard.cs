using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace WinFormsApp1.Models.Complex
{
    public partial class ComplexCard : UserControl
    {


        private ComplexSuperAdminResponseDTO _data;

        // esto no se para que es
        // public event EventHandler<int?> EnterClicked; pasa el Id del complejo
        public event Action<int?, string?, string?> EnterClicked; // pasa el Id del complejo

        public ComplexCard()
        {
            InitializeComponent();
            SetupDefaultStyles();

        }

        private void SetupDefaultStyles()
        {
            // Estilo general del control,  esto seria el estilo general de mi complex card?
            this.BackColor = Color.White;

            this.BorderStyle = BorderStyle.None;

            //this.BorderStyle = BorderStyle.FixedSingle;
            this.Padding = new Padding(8);
            this.Margin = new Padding(10);
            //aca es donde le doy el ultimo set a mi card que voy a mostrar
            this.Width = 350;
            //this.Height = 210;
            this.Height = 300;
            this.TabStop = false;

            // Ajustes de labels que requieren lógica fuera del diseñador
            labelAddress.AutoSize = false;
            labelAddress.MaximumSize = new Size(280, 0); // permite varias líneas si hace falta
            labelAddress.Size = new Size(280, 60);

            labelPhone.AutoSize = false;
            labelPhone.MaximumSize = new Size(280, 0);
            labelPhone.Size = new Size(280, 60);


            ApplyRoundedCorners(12);


        }

        // Método público para asignar datos 
        public void SetData(ComplexSuperAdminResponseDTO dto)
        {
            _data = dto;
            /*
            MessageBox.Show(
                $"ComplexId: {dto.Id}\nOwner: {dto.NameUser} {dto.LastNameUser}",
                "DEBUG CARD"
            );
            */

            labelName.Text = dto.Name;
            labelAddress.Text = $"{dto.Province} - {dto.Locality}";
            labelPhone.Text = $"{dto.Street} {dto.Number} · {dto.Phone}";

            //aca hacer un if para setear el color del estado, rojo/naranja/verde
            labelState.Text = $"Estado: {dto.State}";
            if (dto.State.Equals("Pendiente"))
            {
                labelState.ForeColor = Color.Orange;
            }else if (dto.State.Equals("Habilitado"))
            {
                labelState.ForeColor = Color.Green;
            }
            else
            {
                labelState.ForeColor= Color.Red;
            }

        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            /*
                La card NO abre formularios
                La card SOLO avisa “hicieron click con este ID”
                El UserControl decide qué hacer con eso
             */
            EnterClicked?.Invoke(_data?.Id, _data?.NameUser, _data?.LastNameUser); //aca agregar nombre y apellido

        }

        private void ApplyRoundedCorners(int radius)
        {
            var path = new GraphicsPath();
            int diameter = radius * 2;

            path.StartFigure();
            path.AddArc(0, 0, diameter, diameter, 180, 90);
            path.AddArc(Width - diameter, 0, diameter, diameter, 270, 90);
            path.AddArc(Width - diameter, Height - diameter, diameter, diameter, 0, 90);
            path.AddArc(0, Height - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            this.Region = new Region(path);
        }


    }
}
