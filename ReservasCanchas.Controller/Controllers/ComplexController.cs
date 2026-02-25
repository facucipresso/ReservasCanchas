using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.Dtos.Complex;
using ReservasCanchas.BusinessLogic.Dtos.Notification;
using ReservasCanchas.BusinessLogic.JWTService;
using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;
using System.Runtime.CompilerServices;

namespace ReservasCanchas.Controller.Controllers
{
    [Route("api/complexes")]
    [ApiController]
    public class ComplexController : ControllerBase
    {
        private readonly ComplexBusinessLogic _complexBusinessLogic;
        private readonly StatisticsBusinessLogic _statisticsBusinessLogic;
        private readonly TokenService _tokenService;
        private readonly AuthService _authService;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env; // para WebRootPath
        public ComplexController(ComplexBusinessLogic complexBusinessLogic, StatisticsBusinessLogic statisticsBusinessLogic, IWebHostEnvironment env,
            TokenService tokenService, UserManager<User> userManager, AuthService authService)
        {
            _complexBusinessLogic = complexBusinessLogic;
            _statisticsBusinessLogic = statisticsBusinessLogic;
            _env = env;
            _tokenService = tokenService;
            _userManager = userManager;
            _authService = authService;
        }

        [HttpGet("my")]
        //PASO 17 USO DE JWT, pongo en endpoint en Authorize para que solo me permita acceso si mando un token
        [Authorize]
        public async Task<ActionResult<List<ComplexCardResponseDTO>>> GetMyComplexes()
        {

            var complexes = await _complexBusinessLogic.GetComplexesForAdminComplexIdAsync();
            return Ok(complexes);
        }

        [HttpGet("filters")] 
        public async Task<ActionResult<List<ComplexCardResponseDTO>>> GetComplexesWithFilters([FromQuery] ComplexFiltersRequestDTO filters)
        {
            var complexes = await _complexBusinessLogic.SearchAvailableComplexes(filters);
            return Ok(complexes);
        }

        [HttpGet("super-admin")]
        public async Task<ActionResult<List<ComplexSuperAdminResponseDTO>>> GetAllComplexesForSuperAdmin()
        {
            var complexes = await _complexBusinessLogic.GetAllComplexesBySuperAdminAsync();
            return Ok(complexes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> GetComplexById([FromRoute] int id)
        {
            var complex = await _complexBusinessLogic.GetComplexByIdAsync(id);
            return Ok(complex);
        }

        [HttpGet("stats/{id}")]
        public async Task<ActionResult<ComplexStatsDTO>> GetComplexStats([FromRoute] int id, [FromQuery] DateOnly date, [FromQuery] int? fieldId)
        {
            var stats = await _statisticsBusinessLogic.GetComplexStats(id, date, fieldId);
            return Ok(stats);
        }

        [HttpPost]
        public async Task<ActionResult> CreateComplex([FromForm] CreateComplexRequestDTO requestCreateDTO)
        {
            var uploadPath = Path.Combine(_env.WebRootPath, "uploads", "complexes");
            var created = await _complexBusinessLogic.CreateComplexAsync(requestCreateDTO, uploadPath);

            var userId = _authService.GetUserId();
            var newToken = await _tokenService.CreateTokenByUserId(userId);

            return CreatedAtAction(nameof(GetComplexById), new { id = created.Id }, new { complex = created, token = newToken });
        }

        [HttpPatch("{id}/basic-info")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> UpdateComplexBasicInfo([FromRoute] int id, [FromBody] UpdateComplexBasicInfoRequestDTO requestUpdateDTO)
        {
            var updatedComplexDTO = await _complexBusinessLogic.UpdateComplexAsync(id, requestUpdateDTO);
            return Ok(updatedComplexDTO);
        }

        [HttpPut("{id}/time-slots")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> UpdateComplexTimeSlots([FromRoute] int id, [FromBody] UpdateTimeSlotComplexRequestDTO requestUpdateDTO)
        {
            var updated = await _complexBusinessLogic.UpdateTimeSlotsAsync(id, requestUpdateDTO);
            return Ok(updated);
        }

        [HttpPut("{id}/services")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> UpdateComplexServices([FromRoute] int id, [FromBody] UpdateComplexServiceRequestDTO requestUpdateDTO)
        {
            var updated = await _complexBusinessLogic.UpdateServicesAsync(id, requestUpdateDTO.ServicesIds);
            return Ok(updated);
        }

        [HttpPatch("{id}/image")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> UpdateComplexImage([FromRoute] int id, [FromForm] IFormFile image)
        {
            var uploadPath = Path.Combine(_env.WebRootPath, "uploads", "complexes");
            var updated = await _complexBusinessLogic.UpdateImageAsync(id, image, uploadPath);
            return Ok(updated);
        }

        [HttpPatch("{id}/state")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> UpdateComplexState([FromRoute] int id, [FromBody] UpdateComplexStateDTO requestUpdateDTO)
        {//Cambio de estado del complejo (Super Admin o Admin del complejo, dependiendo de la transición)
            //var updated = await _complexBusinessLogic.ChangeStateComplexAsync(id, requestUpdateDTO.ComplexState); reemplazo pero no elimine metodo
            var updated = await _complexBusinessLogic.ChangeStateCompIexAsync(id, requestUpdateDTO);

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComplex([FromRoute] int id)
        {//Eliminacion de complejo (Solo Admin del complejo)
            await _complexBusinessLogic.DeleteComplexAsync(id);
            return NoContent();
        }

        [HttpGet("getComplexOwner/{id}")]
        public async Task<ActionResult<ComplexOwnerDTO>> GetComplexOwner([FromRoute] int id)
        {
            var datos = await _complexBusinessLogic.GetComplexOwnerAsync(id);
            return Ok(datos);
        }

    }
}
