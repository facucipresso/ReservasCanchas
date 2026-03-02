using ReservasCanchas.BusinessLogic.Dtos.Complex;
using ReservasCanchas.BusinessLogic.Dtos.Dashboard;
using ReservasCanchas.BusinessLogic.Dtos.Review;
using ReservasCanchas.BusinessLogic.Dtos.User;
using ReservasCanchas.BusinessLogic.Exceptions;

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

        public async Task<List<UserResponseWithRoleDTO>> GetLastSixUsersWithRoleAsync()
        {
            var usuarios = await _userBusinessLogic.GetLastSixUsersWithRoleAsync();
            if(usuarios == null) throw new NotFoundException($"No hay usuarios");
            
            return usuarios;
        }
        public async Task<List<ComplexSuperAdminResponseDTO>> GetLastFiveComplexesBySuperAdminAsync()
        {
            var complejos = await _complexBusinessLogic.GetLastFiveComplexesBySuperAdminAsync();
            if (complejos == null) throw new NotFoundException($"No hay complejos");

            return complejos;
        }

        public async Task<List<ReviewResponseDTO>> GetLastFourReviewsAsync()
        {
            var reviews = await _reviewBusinessLogic.GetLastFourReviewsAsync();
            if (reviews == null) throw new NotFoundException($"No hay reviews");

            return reviews;
        }
    }
}
