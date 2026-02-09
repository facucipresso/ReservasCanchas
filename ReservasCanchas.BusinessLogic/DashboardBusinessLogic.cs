using ReservasCanchas.BusinessLogic.Dtos.Complex;
using ReservasCanchas.BusinessLogic.Dtos.Dashboard;
using ReservasCanchas.BusinessLogic.Dtos.Review;
using ReservasCanchas.BusinessLogic.Dtos.User;
using ReservasCanchas.BusinessLogic.Exceptions;
using ReservasCanchas.DataAccess.Repositories;
using ReservasCanchas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic
{
    public class DashboardBusinessLogic
    {
        private readonly NotificationBusinessLogic _notificationBusinessLogic;
        private readonly UserBusinessLogic _userBusinessLogic;
        private readonly ComplexBusinessLogic _complexBusinessLogic;
        private readonly ReviewBusinessLogic _reviewBusinessLogic;
        public DashboardBusinessLogic(NotificationBusinessLogic notificationBusinessLogic, UserBusinessLogic userBusinessLogic, ComplexBusinessLogic complexBusinessLogic, ReviewBusinessLogic reviewBusinessLogic)
        {
            _notificationBusinessLogic = notificationBusinessLogic;
            _userBusinessLogic = userBusinessLogic;
            _complexBusinessLogic = complexBusinessLogic;
            _reviewBusinessLogic = reviewBusinessLogic;
        }
        public async Task<SumaryDashboardDTO> GetDashboardDataAsync()
        {
            var pendingNotifications = await _notificationBusinessLogic.GetNumberOfNotificationsNoReaded(); 
            var totalUsers = await _userBusinessLogic.GetTotalUsersAsync();
            var enabledComplexes = await _complexBusinessLogic.GetNumberOfComplexEnabled();
            var totalReviews = await _reviewBusinessLogic.GetNumberOfReviews();

            return new SumaryDashboardDTO
            {
                PendingNotifications = pendingNotifications,
                TotalUsers = totalUsers,
                EnabledComplexes = enabledComplexes,
                TotalReviews = totalReviews
            };
        }

        // PARA OBTENER LOS ULTMOS 6 USUARIO ACTIVOS USAR GetLastSixUsersWithRoleAsync DE USER BUSINESS LOGIC
        public async Task<List<UserResponseWithRoleDTO>> GetLastSixUsersWithRoleAsync()
        {
            var usuarios = await _userBusinessLogic.GetLastSixUsersWithRoleAsync();
            if(usuarios == null) throw new NotFoundException($"No hay usuarios");
            
            return usuarios;
        }
        // PARA OBTENER LOS ULTIMOS 5 COMPLEJOS HABILITADOS USAR GetLastFiveComplexesBySuperAdminAsync DE COMPLEX BUSINESS LOGIC
        public async Task<List<ComplexSuperAdminResponseDTO>> GetLastFiveComplexesBySuperAdminAsync()
        {
            var complejos = await _complexBusinessLogic.GetLastFiveComplexesBySuperAdminAsync();
            if (complejos == null) throw new NotFoundException($"No hay complejos");

            return complejos;
        }

        // PARA OBTENES LOS ULTIMOS 4 REVIEWS USAR GetLastFourReviewsAsync DE REVIEW BUSINESS LOGIC
        public async Task<List<ReviewResponseDTO>> GetLastFourReviewsAsync()
        {
            var reviews = await _reviewBusinessLogic.GetLastFourReviewsAsync();
            if (reviews == null) throw new NotFoundException($"No hay reviews");

            return reviews;
        }
    }
}
