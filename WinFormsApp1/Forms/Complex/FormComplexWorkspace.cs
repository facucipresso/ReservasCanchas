using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.UserControls;

namespace WinFormsApp1.Forms.Complex
{
    public partial class FormComplexWorkspace : Form
    {
        private readonly int _complexId;
        private readonly string _nameOwner;
        private readonly string _lastNameOwner;
        public FormComplexWorkspace(int complexId, string nameOwnwer, string lastNameOwnwer)
        {
            InitializeComponent();

            _complexId = complexId;
            _nameOwner = nameOwnwer;
            _lastNameOwner = lastNameOwnwer;

            //funcion que tengo que hacer yo, pero ya lo configure desde el diseñador
            //ConfigureForm();
            LoadInitialView();
        }

        private void LoadInitialView()
        {
            var ucDetail = new UC_ComplexDetail(_complexId, _nameOwner, _lastNameOwner);

            ucDetail.VerCanchasClicked += OnVerCanchas;
            
            ucDetail.VerReservasClicked += OnVerReservas;
            
            ucDetail.VerReseniasClicked += OnVerResenias;

            ucDetail.CerrarClicked += OnCerrarUC_Detail;
            

            // después vamos a enganchar eventos acá
            LoadUserControl(ucDetail);
        }

        private void LoadUserControl(UserControl control)
        {
            panelContentComplexWorkspace.Controls.Clear();

            control.Dock = DockStyle.Fill;

            panelContentComplexWorkspace.Controls.Add(control);
        }

        private void OnVerCanchas()
        {
            var ucCanchas = new UC_FieldList(_complexId);

            //ucCanchas.VerReservasCanchaClicked += OnVerReservasCancha;
            ucCanchas.VerReservasCanchaClicked += UcCanchas_VerReservasCanchaClicked;
            ucCanchas.CerrarClicked += UcCanchas_CerrarClicked;
            ucCanchas.VolverClicked += UcCanchas_VolverClicked;
            LoadUserControl(ucCanchas);
        }

        private void UcCanchas_VolverClicked()
        {
            // aca tengo que abrir el complex detail
            LoadInitialView();
        }

        private void UcCanchas_CerrarClicked()
        {
            this.Close();
        }

        private void UcCanchas_VerReservasCanchaClicked(object sender, int fieldId)
        {
            var ucReservas = new UC_FieldListReservations(fieldId);

            ucReservas.VolverClicked += () => OnVerCanchas();
            ucReservas.CerrarClicked += () => this.Close();
            LoadUserControl(ucReservas);
        }

        
        private void OnVerReservas()
        {
            var ucReservas = new UC_ComplexReservations(_complexId);

            ucReservas.VolverClicked += () => LoadInitialView();
            ucReservas.CerrarClicked += () => this.Close();
            LoadUserControl(ucReservas);
        }

        
        private void OnVerResenias()
        {
            var ucResenias = new UC_ComplexReviews(_complexId);

            ucResenias.VolverClicked += () => LoadInitialView();
            ucResenias.CerrarClicked += () => this.Close();

            LoadUserControl(ucResenias);
        }

        private void OnCerrarUC_Detail()
        {
            this.Close();
        }
        /*
        //esto para cuando tenga creado el uc de canchas
        private void OnVerReservasCancha(int canchaId)
        {
            var ucReservas = new UC_ReservasCancha(canchaId);
            LoadUserControl(ucReservas);
        }
        */
    }
}
