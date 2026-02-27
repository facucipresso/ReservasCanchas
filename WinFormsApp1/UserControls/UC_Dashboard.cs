
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

            flpUltimosComplejos.Resize += (s, e) => AdjustFlowLayoutChildWidths(flpUltimosComplejos);

            flpUltimosUsuarios.Resize += (s, e) => AdjustFlowLayoutChildWidths(flpUltimosUsuarios);

            flpUltimasReseñas.Resize += (s, e) => AdjustFlowLayoutChildWidths(flpUltimasReseñas);

            //this.Resize += UC_Dashboard_Resize;
            //flpCards.Resize += UC_Dashboard_Resize;

            //LoadStadisticCars();

            /*
            var cardNotificaciones = new UC_DashboardCard
            {
                Title = "Notificaciones pendientes",
                DisplayValue = "5",
                Icon = Properties.Resources.icono_notificaciones,
            };

            var cardUsuarios = new UC_DashboardCard
            {
                Title = "Usuarios registrados",
                DisplayValue = "120",
                Icon = Properties.Resources.icono_usuarios,
            };

            var cardComplejos = new UC_DashboardCard
            {
                Title = "Complejos habilitados",
                DisplayValue = "8",
                Icon = Properties.Resources.icono_complejos,
            };

            var cardReviews = new UC_DashboardCard
            {
                Title = "Reseñas hechas",
                DisplayValue = "200",
                Icon = Properties.Resources.icono_resenas,
            };

            flpCards.Controls.Add(cardNotificaciones);
            flpCards.Controls.Add(cardUsuarios);
            flpCards.Controls.Add(cardComplejos);
            flpCards.Controls.Add(cardReviews);

            AdjustDashboardCardsWidth();
            */

        }

        private async Task LoadStadisticCars()
        {
            var summary = await _dashboardService.GetDashboardDataAsync();
            var card1 = new UC_DashboardStadisticCard();
            card1.Dock = DockStyle.Fill;
            card1.Title = "Usuarios registrados";
            card1.Value = summary.TotalUsers.ToString();
            card1.Icon = Properties.Resources.icono_usuarios;
            //card1.Dock = DockStyle.Fill;

            var card2 = new UC_DashboardStadisticCard();
            card2.Dock = DockStyle.Fill;
            card2.Title = "Notificaciones pendientes";
            card2.Value = summary.PendingNotifications.ToString();
            card2.Icon = Properties.Resources.icono_notificaciones;
            //card2.Dock = DockStyle.Fill;

            var card3 = new UC_DashboardStadisticCard();
            card3.Dock = DockStyle.Fill;
            card3.Title = "Complejos habilitados";
            card3.Value = summary.EnabledComplexes.ToString();
            card3.Icon = Properties.Resources.icono_complejos;
            //card3.Dock = DockStyle.Fill;

            var card4 = new UC_DashboardStadisticCard();
            card4.Dock = DockStyle.Fill;
            card4.Title = "Reseñas hechas";
            card4.Value = summary.TotalReviews.ToString();
            card4.Icon = Properties.Resources.icono_resenas;
            //card4.Dock = DockStyle.Fill;

            tableLayoutPanelForCards.Controls.Add(card1, 0, 0);
            tableLayoutPanelForCards.Controls.Add(card2, 1, 0);
            tableLayoutPanelForCards.Controls.Add(card3, 2, 0);
            tableLayoutPanelForCards.Controls.Add(card4, 3, 0);

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

        private Control CreateComplexReview(string complexName, string userName, int rating)
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

        private Control CreateComplexReview2(string complexName, string userName, int rating)
        {
            var card = new UC_DasboardComplexCard();
            card.ComplexName = complexName;
            card.UserName = userName;
            card.Rating = $"{new string('★', rating)}{new string('☆', 5 - rating)}";

            card.Width = flpUltimasReseñas.ClientSize.Width - 25;
            card.Height = 85;
            card.Margin = new Padding(0, 0, 0, 8);

            return card;
        }

        private Control CreateUserItem2(string userName, string role)
        {
            var card = new UC_DashboardUserCard();
            card.UserName = userName;
            card.UserState = role;

            card.Width = flpUltimosUsuarios.ClientSize.Width - 25;
            card.Height = 85;
            card.Margin = new Padding(0, 0, 0, 8);

            return card;
        }

        private Control CreateComplexItem(string name, string state)
        {
            var card = new UC_DashboardLastComplexCard();
            card.ComplexName = name;
            card.ComplexState = state;

            card.Width = flpUltimosComplejos.ClientSize.Width - 25;
            card.Height = 85;
            card.Margin = new Padding(0, 0, 0, 8);

            return card;
        }

        /*
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
        */

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
            //await LoadRealSummaryAsync();
            await LoadStadisticCars();
            await LoadRealComplexesAsync();
            await LoadRealUsersAsync();
            await LoadRealReviewsAsync();
        }

        /*
        private async Task LoadRealSummaryAsync()
        {
            var summary = await _dashboardService.GetDashboardDataAsync();

            ((UC_DashboardCard)flpCards.Controls[0]).DisplayValue = summary.PendingNotifications.ToString();
            ((UC_DashboardCard)flpCards.Controls[1]).DisplayValue = summary.TotalUsers.ToString();
            ((UC_DashboardCard)flpCards.Controls[2]).DisplayValue = summary.EnabledComplexes.ToString();
            ((UC_DashboardCard)flpCards.Controls[3]).DisplayValue = summary.TotalReviews.ToString();
        }
        */

        private async Task LoadRealComplexesAsync()
        {
            var complexes = await _dashboardService.GetLastFiveComplexesBySuperAdminAsync();

            flpUltimosComplejos.Controls.Clear();

            foreach (var c in complexes)
            {
                flpUltimosComplejos.Controls.Add(
                    //CreateFakeComplexItem(c.Name, c.ComplexState.ToString())
                    CreateComplexItem(c.Name, c.ComplexState.ToString())
                );
            }
        }

        private async Task LoadRealUsersAsync()
        {
            var users = await _dashboardService.GetLastSixUsersWithRoleAsync();

            flpUltimosUsuarios.Controls.Clear();

            foreach (var u in users)
            {
                flpUltimosUsuarios.Controls.Add(
                    //CreateFakeUserItem(u.UserName, u.Role)
                    CreateUserItem2(u.UserName, u.Role)
                );
            }
        }

        private async Task LoadRealReviewsAsync()
        {
            var reviews = await _dashboardService.GetLastFourReviewsAsync();

            flpUltimasReseñas.Controls.Clear();

            foreach (var r in reviews)
            {
                flpUltimasReseñas.Controls.Add(

                    
                    CreateComplexReview2(
                        r.ComplexName,
                        $"{r.Name} {r.LastName}",
                        r.Score
                    )
                    
                 );
            }
        }

        private void AdjustFlowLayoutChildWidths(FlowLayoutPanel flp)
        {
            foreach (Control ctrl in flp.Controls)
            {
                int newWidth = flp.ClientSize.Width - 25;
                ctrl.Width = newWidth;
                ctrl.Height = 85;
            }

            flp.AutoScroll = false;
            flp.AutoScroll = true;
        }



    }
}

