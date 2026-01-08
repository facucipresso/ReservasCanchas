using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.Dtos.Complex;
using ReservasCanchas.BusinessLogic.Dtos.Notification;
using ReservasCanchas.BusinessLogic.JWTService;
using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;

namespace ReservasCanchas.Controller.Controllers
{
    [Route("api/complexes")]
    [ApiController]
    public class ComplexController : ControllerBase
    {
        private readonly ComplexBusinessLogic _complexBusinessLogic;
        private readonly TokenService _tokenService;
        private readonly AuthService _authService;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env; // para WebRootPath
        public ComplexController(ComplexBusinessLogic complexBusinessLogic, IWebHostEnvironment env,
            TokenService tokenService, UserManager<User> userManager, AuthService authService)
        {
            _complexBusinessLogic = complexBusinessLogic;
            _env = env;
            _tokenService = tokenService;
            _userManager = userManager;
            _authService = authService;
        }

        [HttpGet("my")]
        //PASO 17 USO DE JWT, pongo en endpoint en Authorize para que solo me permita acceso si mando un token
        [Authorize]
        public async Task<ActionResult<List<ComplexCardResponseDTO>>> GetMyComplexes()
        {//Admin de Complejo obtiene sus complejos

            var complexes = await _complexBusinessLogic.GetComplexesForAdminComplexIdAsync();
            return Ok(complexes);
        }

        [HttpGet("filters")] // este va sin autenticacion
        public async Task<ActionResult<List<ComplexCardResponseDTO>>> GetComplexesWithFilters([FromQuery] ComplexFiltersRequestDTO filters)
        {//Busqueda principal de complejos disponibles para usuarios comunes usando filtros
            var complexes = await _complexBusinessLogic.SearchAvailableComplexes(filters);
            return Ok(complexes);
        }

        [HttpGet("super-admin")]
        public async Task<ActionResult<List<ComplexSuperAdminResponseDTO>>> GetAllComplexesForSuperAdmin()
        {//Super Admin obtiene todos los complejos
            var complexes = await _complexBusinessLogic.GetAllComplexesBySuperAdminAsync();
            return Ok(complexes);
        }

        [HttpGet("{id}")] // esta va sin autenticacion
        public async Task<ActionResult<ComplexDetailResponseDTO>> GetComplexById([FromRoute] int id)
        {//Obtener detalle de complejo por id
            var complex = await _complexBusinessLogic.GetComplexByIdAsync(id);
            return Ok(complex);
        }

        [HttpPost]
        public async Task<ActionResult> CreateComplex([FromForm] CreateComplexRequestDTO requestCreateDTO)
        {//Creacion de complejo
            var uploadPath = Path.Combine(_env.WebRootPath, "uploads", "complexes");
            var created = await _complexBusinessLogic.CreateComplexAsync(requestCreateDTO, uploadPath);
            
            var userId = _authService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var newToken = await _tokenService.CreateToken(user);

            return CreatedAtAction(nameof(GetComplexById), new { id = created.Id }, new{complex = created,token = newToken});      
        }

        [HttpPatch("{id}/basic-info")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> UpdateComplexBasicInfo([FromRoute] int id,[FromBody] UpdateComplexBasicInfoRequestDTO requestUpdateDTO)
        {//Edicion de info basica del complejo (Solo admin del complejo)
            var updatedComplexDTO = await _complexBusinessLogic.UpdateComplexAsync(id, requestUpdateDTO);
            return Ok(updatedComplexDTO);
        }

        [HttpPut("{id}/time-slots")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> UpdateComplexTimeSlots([FromRoute] int id, [FromBody] UpdateTimeSlotComplexRequestDTO requestUpdateDTO)
        {//Edicion de timeslots
            var updated = await _complexBusinessLogic.UpdateTimeSlotsAsync(id, requestUpdateDTO);
            return Ok(updated);
        }

        [HttpPut("{id}/services")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> UpdateComplexServices([FromRoute] int id, [FromBody] UpdateComplexServiceRequestDTO requestUpdateDTO)
        {//Edicion de servicios del complejo (Solo admin del complejo)
            var updated = await _complexBusinessLogic.UpdateServicesAsync(id, requestUpdateDTO.ServicesIds);
            return Ok(updated);
        }

        [HttpPatch("{id}/state")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> UpdateComplexState([FromRoute] int id, [FromBody] UpdateComplexStateDTO requestUpdateDTO)
        {//Cambio de estado del complejo (Super Admin o Admin del complejo, dependiendo de la transición)
            var updated = await _complexBusinessLogic.ChangeStateComplexAsync(id, requestUpdateDTO.State);

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComplex([FromRoute] int id)
        {//Eliminacion de complejo (Solo Admin del complejo)
            await _complexBusinessLogic.DeleteComplexAsync(id);
            return NoContent();
        }

        //notifico la aprobacion del complejo
        [HttpPatch("approveComplex")]
        public async Task<IActionResult> ApproveComplex([FromBody] AproveComplexRequestDTO request)
        {
            await _complexBusinessLogic.ApproveComplexAsync(request);
            return NoContent();
        }

        //notifico el rechazo del complejo
        [HttpPatch("rejectComplex")]
        public async Task<IActionResult> RejectComplex([FromBody] RejectComplexRequestDTO request)
        {
            await _complexBusinessLogic.RejectComplexAsync(request); 
            return NoContent();
        }

    }
}
