using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.Dtos.Complex;
using ReservasCanchas.BusinessLogic.Dtos.Dashboard;
using ReservasCanchas.BusinessLogic.Dtos.Review;
using ReservasCanchas.BusinessLogic.Dtos.User;

namespace ReservasCanchas.Controller.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardBusinessLogic _dasboardBusinessLogic;
        public DashboardController(DashboardBusinessLogic businessLogic)
        {
            _dasboardBusinessLogic = businessLogic; 
        }




        [HttpGet("dashboard-summary")]
        public async Task<ActionResult<SumaryDashboardDTO>> GetDashboardData()
        {
            var response = await _dasboardBusinessLogic.GetDashboardDataAsync();
            return Ok(response);
        }

        [HttpGet("dashboard-complex")]
        public async Task<ActionResult<List<ComplexSuperAdminResponseDTO>>> GetLastFiveComplexesBySuperAdminAsync()
        {
            var response = await _dasboardBusinessLogic.GetLastFiveComplexesBySuperAdminAsync();
            return Ok(response);
        }

        [HttpGet("dashboard-users")]
        public async Task<ActionResult<List<UserResponseWithRoleDTO>>> GetLastSixUsersWithRoleAsync()
        {
            var response = await _dasboardBusinessLogic.GetLastSixUsersWithRoleAsync();
            return Ok(response);
        }

        [HttpGet("dashboard-review")]
        public async Task<ActionResult<List<ReviewResponseDTO>>> GetLastFourReviewsAsync()
        {
            var response = await _dasboardBusinessLogic.GetLastFourReviewsAsync();
            return Ok(response);
        }
    }
}
