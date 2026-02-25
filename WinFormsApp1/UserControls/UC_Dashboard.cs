
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

            _dashboardService = new DashboardService();
            this.Load += UC_Dashboard_Load;

            this.Resize += UC_Dashboard_Resize;
            flpCards.Resize += UC_Dashboard_Resize;


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


        }


        private Control CreateFakeComplexItem(string name, string state)
        {
            var panel = new Panel
            {
                Height = 50,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 8),
                Padding = new Padding(12, 8, 12, 8),
                Width = flpUltimosComplejos.ClientSize.Width - 25 
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

        private Control CreateFakeUserItem(string userName, string role)
        {
            var panel = new Panel
            {
                Height = 50,
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

            var lblComplex = new Label
            {
                Text = complexName,
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                AutoEllipsis = true,
                Height = 20
            };

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

        private void UC_Dashboard_Resize(object sender, EventArgs e)
        {
            AdjustDashboardCardsWidth();
        }

        private void AdjustDashboardCardsWidth()
        {
            int cardsCount = flpCards.Controls.Count;
            if (cardsCount == 0) return;

            int totalHorizontalPadding =
                flpCards.Padding.Left +
                flpCards.Padding.Right;

            int totalHorizontalMargin = 0;

            foreach (Control card in flpCards.Controls)
            {
                totalHorizontalMargin += card.Margin.Left + card.Margin.Right;
            }

            int availableWidth =
                flpCards.ClientSize.Width
                - totalHorizontalPadding
                - totalHorizontalMargin;

            int cardWidth = availableWidth / cardsCount;

            foreach (Control card in flpCards.Controls)
            {
                card.Width = cardWidth;
                card.Height = flpCards.ClientSize.Height - 20;
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
                DialogService.ShowError(Form.ActiveForm ?? this.TopLevelControl as Form, ex.Message);
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
                flpUltimosComplejos.Controls.Add(CreateFakeComplexItem(c.Name, c.ComplexState.ToString()));
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
                        $"{r.Name} {r.LastName}",
                        r.Score
                    ));
            }
        }



    }
}

