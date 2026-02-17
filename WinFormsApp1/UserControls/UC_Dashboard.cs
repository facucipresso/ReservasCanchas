using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Services;

namespace WinFormsApp1.UserControls
{
    public partial class UC_Dashboard : UserControl
    {
        private readonly DashboardService _dashboardService; 
        public UC_Dashboard()
        {
            InitializeComponent();

            //para datos reales
            _dashboardService = new DashboardService();
            //para datos reales
            this.Load += UC_Dashboard_Load;


            var cardNotificaciones = new UC_DashboardCard
            {
                Title = "Notificaciones pendientes",
                DisplayValue = "5",
                Icon = Properties.Resources.apuntando_a_la_derecha__1_
            };

            var cardUsuarios = new UC_DashboardCard
            {
                Title = "Usuarios registrados",
                DisplayValue = "120",
                Icon = Properties.Resources.apuntando_a_la_derecha__1_
            };

            var cardComplejos = new UC_DashboardCard
            {
                Title = "Complejos habilitados",
                DisplayValue = "8",
                Icon = Properties.Resources.apuntando_a_la_derecha__1_
            };

            var cardReviews = new UC_DashboardCard
            {
                Title = "Reseñas hechas",
                DisplayValue = "200",
                Icon = Properties.Resources.apuntando_a_la_derecha__1_
            };

            flpCards.Controls.Add(cardNotificaciones);
            flpCards.Controls.Add(cardUsuarios);
            flpCards.Controls.Add(cardComplejos);
            flpCards.Controls.Add(cardReviews);

            AdjustDashboardCardsWidth();

            //LoadFakeComplexes();
            //LoadFakeUsers();
            //LoadFakeReviews();

        }


        private Control CreateFakeComplexItem(string name, string state)
        {
            var panel = new Panel
            {
                Height = 50,
                //Dock = DockStyle.Top,
                //BackColor = Color.FromArgb(245, 245, 245),
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 8),
                Padding = new Padding(12, 8, 12, 8),
                Width = flpUltimosComplejos.ClientSize.Width - 25 // ocupa casi todo
            };

            var lblName = new Label
            {
                Text = name,
                Dock = DockStyle.Left,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                AutoSize = true
            };

            var lblState = new Label
            {
                Text = state,
                Dock = DockStyle.Right,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                AutoSize = true
            };

            panel.Controls.Add(lblName);
            panel.Controls.Add(lblState);

            return panel;
        }

        private void LoadFakeComplexes()
        {
            flpUltimosComplejos.Controls.Clear();

            flpUltimosComplejos.Controls.Add(
                CreateFakeComplexItem("Complejo Norte", "Habilitado"));

            flpUltimosComplejos.Controls.Add(
                CreateFakeComplexItem("Arena Fútbol", "Pendiente"));

            flpUltimosComplejos.Controls.Add(
                CreateFakeComplexItem("Club Central", "Bloqueado"));

            flpUltimosComplejos.Controls.Add(
                CreateFakeComplexItem("Santiago Bernabeu", "Pendiente"));

            flpUltimosComplejos.Controls.Add(
                CreateFakeComplexItem("Club Barracas", "Pendiente"));
        }

        private Control CreateFakeUserItem(string userName, string role)
        {
            var panel = new Panel
            {
                Height = 50,
                //Dock = DockStyle.Top,
                //BackColor = Color.FromArgb(245, 245, 245),
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 8),
                Padding = new Padding(12, 8, 12, 8),
                Width = flpUltimosUsuarios.ClientSize.Width - 25
            };

            var lblUser = new Label
            {
                Text = userName,
                Dock = DockStyle.Left,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                AutoSize = true
            };

            var lblRole = new Label
            {
                Text = role,
                Dock = DockStyle.Right,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                AutoSize = true
            };

            panel.Controls.Add(lblUser);
            panel.Controls.Add(lblRole);

            return panel;
        }



        private void LoadFakeUsers()
        {
            flpUltimosUsuarios.Controls.Clear();

            flpUltimosUsuarios.Controls.Add(
                CreateFakeUserItem("juanperez", "Admin Complejo"));

            flpUltimosUsuarios.Controls.Add(
                CreateFakeUserItem("maria.gomez", "Usuario"));

            flpUltimosUsuarios.Controls.Add(
                CreateFakeUserItem("superadmin", "SuperAdmin"));

            flpUltimosUsuarios.Controls.Add(
                CreateFakeUserItem("nahucipre", "Usuario"));

            flpUltimosUsuarios.Controls.Add(
                CreateFakeUserItem("lauti.gomez", "Usuario"));

            flpUltimosUsuarios.Controls.Add(
                CreateFakeUserItem("marceBustos", "Usuario"));
        }

        private Control CreateComplexReview(
            string complexName,
            string userName,
            int rating)
        {
            rating = Math.Max(0, Math.Min(5, rating));
            var panel = new Panel
            {
                Height = 60,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 8),
                Padding = new Padding(12, 8, 12, 8),
                Width = flpUltimasReseñas.ClientSize.Width - 25
            };

            // Nombre del complejo (arriba, solo)
            var lblComplex = new Label
            {
                Text = complexName,
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                AutoEllipsis = true,
                Height = 20
            };

            // Usuario + estrellas (abajo)
            var lblUserAndRating = new Label
            {
                Text = $"{userName}   {new string('★', rating)}{new string('☆', 5 - rating)}",
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                Height = 20
            };

            panel.Controls.Add(lblUserAndRating);
            panel.Controls.Add(lblComplex);

            return panel;
        }


        private void LoadFakeReviews()
        {
            flpUltimasReseñas.Controls.Clear();

            flpUltimasReseñas.Controls.Add(
                CreateComplexReview("Complejo Norte", "juanperez", 4));

            flpUltimasReseñas.Controls.Add(
                CreateComplexReview("Arena Fútbol 5", "maria.gomez", 5));

            flpUltimasReseñas.Controls.Add(
                CreateComplexReview("Club Central Deportivo", "nahucipre", 3));

            flpUltimasReseñas.Controls.Add(
                CreateComplexReview("Barracas Indoor", "lauti.gomez", 4));
        }


        private void AdjustDashboardCardsWidth()
        {
            int cardsCount = flpCards.Controls.Count;
            if (cardsCount == 0) return;

            int totalPadding =
                flpCards.Padding.Left +
                flpCards.Padding.Right +
                (flpCards.Margin.Left + flpCards.Margin.Right);

            int availableWidth = flpCards.ClientSize.Width - totalPadding;

            int cardWidth = (availableWidth / cardsCount) - 10; // margen entre cards

            foreach (Control card in flpCards.Controls)
            {
                card.Width = cardWidth;
                card.Height = flpCards.ClientSize.Height - 10;
            }
        }

        private async void UC_Dashboard_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadRealDataAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task LoadRealDataAsync()
        {
            await LoadRealSummaryAsync();
            await LoadRealComplexesAsync();
            await LoadRealUsersAsync();
            await LoadRealReviewsAsync();
        }

        private async Task LoadRealSummaryAsync()
        {
            var summary = await _dashboardService.GetDashboardDataAsync();

            ((UC_DashboardCard)flpCards.Controls[0]).DisplayValue = summary.PendingNotifications.ToString();
            ((UC_DashboardCard)flpCards.Controls[1]).DisplayValue = summary.TotalUsers.ToString();
            ((UC_DashboardCard)flpCards.Controls[2]).DisplayValue = summary.EnabledComplexes.ToString();
            ((UC_DashboardCard)flpCards.Controls[3]).DisplayValue = summary.TotalReviews.ToString();
        }

        private async Task LoadRealComplexesAsync()
        {
            var complexes = await _dashboardService.GetLastFiveComplexesBySuperAdminAsync();

            flpUltimosComplejos.Controls.Clear();

            foreach (var c in complexes)
            {
                flpUltimosComplejos.Controls.Add(
                    CreateFakeComplexItem(c.Name, c.State.ToString()));
            }
        }

        private async Task LoadRealUsersAsync()
        {
            var users = await _dashboardService.GetLastSixUsersWithRoleAsync();

            flpUltimosUsuarios.Controls.Clear();

            foreach (var u in users)
            {
                flpUltimosUsuarios.Controls.Add(
                    CreateFakeUserItem(u.UserName, u.Role));
            }
        }

        private async Task LoadRealReviewsAsync()
{
    var reviews = await _dashboardService.GetLastFourReviewsAsync();

    flpUltimasReseñas.Controls.Clear();

    foreach (var r in reviews)
    {
        flpUltimasReseñas.Controls.Add(
            CreateComplexReview(
                r.ComplexName,
                $"{r.Name} {r.LastName}", //ver si esto esta bien concatenado
                r.Score
            ));
    }
}



    }
}
